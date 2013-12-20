using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WriteCongress.Core;
using WriteCongress.Web.Models;

namespace WriteCongress.Web.Controllers
{
    public class AuthenticationController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FacebookLogin() {
            return View();
        }
        public ActionResult FacebookChannel() {
            return View();
        }
        
        [HttpPost]
        public ActionResult CheckEmailAddress(string email) {
            Logger.Info("check email: {0}", email);
            if (Db.Users.Any(u => u.Email == email)) {
                return Json(new JsonServiceResult<bool>(true) {Data = true, Message = "Email already exists."});
            }else {
                return Json(new JsonServiceResult<bool>(true) {Data = false});
            }
        }
      
        public ActionResult Signout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ValidateEmailFormat(string email) {
            try {
                System.Net.Mail.MailAddress ma = new System.Net.Mail.MailAddress(email);
                return Json(new JsonServiceResult<string>(true){Data = ma.Address});
            }
            catch {
                Logger.Trace("validate email: invalid {0}", email);
                return Json(new JsonServiceResult<bool>(false));
            }
        }
        [HttpGet]
        public ActionResult SignIn(string redirect, bool? failedLogin,string email) {
            ViewBag.FailedLogin = failedLogin.HasValue && failedLogin.Value;
            ViewBag.Email = email;
            ViewBag.Redirect = redirect;
            return View();
        }


        [HttpPost]
        public ActionResult SignIn(string email, string password, string redirect) {
            var user = Db.Users.SingleOrDefault(u => u.Email == email);
            if (String.IsNullOrWhiteSpace(redirect)) {
                if (Request.UrlReferrer != null) {
                    redirect = Request.UrlReferrer.AbsolutePath;
                }
                else {
                    redirect = "/";
                }
            }
            if (user != null) {
                var hashedPassword = CryptoHelper.HashAndSalt(password, user.Salt);
                if (hashedPassword == user.Password) {
                    user.SessionId = CryptoHelper.HMACObject(CryptoHelper.GenerateRandomString(64), "TacoBellFridays", StringEncodingFormat.Base64).Left(128);
                    FormsAuthentication.SetAuthCookie(user.SessionId, true);
                    Db.SaveChanges();
                    redirect = "/Account";
                    Logger.Info("login: successfull login for userid: {0}", user.Id);
                    return Redirect(redirect ?? Request.UrlReferrer.ToString());
                }
                else {
                    Logger.Info("login: password didn't match for userid: {0}", user.Id);
                    return RedirectToAction("Signin", "Authentication", new { FailedLogin = true, Redirect = redirect,Email=email});
                }
            }
            else {
                Logger.Warn("login: couldn't find user for email: {1}", email);
                return RedirectToAction("Signin", "Authentication", new { FailedLogin = true, Redirect = redirect});
            }
        
        }
        
        [HttpGet]
        public ActionResult PasswordReset() {
            return View();
        }
        [HttpPost]
        public ActionResult PasswordReset(string email) {
            email = email.Trim();
            var user = Db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) {
                Logger.Error("password reset: couldn't find a user with email: {0}", email);
                return Json(false, JsonRequestBehavior.DenyGet);
            }
            else {
                PasswordReset r = new PasswordReset();
                r.Guid = Guid.NewGuid();
                r.UserId = user.Id;
                r.DateRequestedUtc = DateTime.UtcNow;
                r.UserHostAddress = Request.UserHostAddress;
                r.DateUsed = null;
                Db.PasswordResets.Add(r);
                Db.SaveChanges();

                try {
                    EmailManager em = new EmailManager();
                    em.SendMessage(user.Email, EmailManager.Support, "Password Reset - WriteCongress.us", this.RenderPartialViewToString("~/Views/Email/PasswordReset.cshtml", r));
                }
                catch (System.Exception ex) {
                    Logger.FatalException(String.Format("password reset: couldn't send an email. user:{0}", user.Email), ex);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpGet]
        public ActionResult BeginPasswordReset(Guid token) {
            var dayAgo = DateTime.UtcNow.AddHours(-24);
            var passwordReset = Db.PasswordResets.FirstOrDefault(r => r.Guid == token && r.UserHostAddress == Request.UserHostAddress && r.DateUsed==null && r.DateRequestedUtc>=dayAgo);
            if (passwordReset == null) {
                Logger.Warn("password reset: request of {0} was not found or was expired/used previously", token);
                return View("InvalidPasswordResetToken");
            }
            else {
                return View(token);
            }
        }
        [HttpPost]
        public ActionResult FinishPasswordReset(Guid token, string email, string password)
        {
            var dayAgo = DateTime.UtcNow.AddHours(-24);
            var passwordReset = Db.PasswordResets.FirstOrDefault(r => r.Guid == token && r.UserHostAddress == Request.UserHostAddress && r.DateUsed == null && r.DateRequestedUtc >= dayAgo);
            if (passwordReset == null) {
                return View("InvalidPasswordResetToken");
            }
            else {
                if (!passwordReset.User.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase)) {
                    Logger.Error("password reset: valid token but email entered didn't match account. user:{0}, entered:{1}",passwordReset.User.Email,email);
                    //TODO: should probably give them a message, but its low priority and i don't want to handle it at the moment
                    return View("InvalidPasswordResetToken");
                }
                else {
                    var user = passwordReset.User;
                    user.Salt = CryptoHelper.GenerateRandomString();
                    user.Password = CryptoHelper.HashAndSalt(password, user.Salt);
                    passwordReset.DateUsed = DateTime.UtcNow;
                    
                    //log them in as well
                    user.SessionId = CryptoHelper.HMACObject(CryptoHelper.GenerateRandomString(64), "TacoBellFridays").Left(128);
                    Db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(user.SessionId, true);
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    

        [HttpPost]
        public ActionResult CreateAccount(string firstname, string lastname, string address1, string address2, string city, string state, string zipcode, string email, string password,string congressionalDistrict,string redirect) {
            if (Db.Users.Any(u => u.Email == email)) {
                return Redirect(redirect ?? Request.UrlReferrer.ToString());
            }
        
            var newUser = new User();
            newUser.Identity = CryptoHelper.GenerateRandomString(64);
            newUser.CreatedDateUtc = DateTime.UtcNow;
            newUser.Email = email.Trim();
            newUser.FirstName = firstname.Trim();
            newUser.LastName = lastname.Trim();
            newUser.AddressOne = address1.Trim();
            newUser.AddressTwo = address2.Trim();
            newUser.City = city.Trim();
            newUser.State = state;
            newUser.ZipCode = zipcode.Trim();
            newUser.Ip = Request.UserHostAddress.Left(15);
            newUser.UserAgent = Request.UserAgent.Left(500);

            newUser.CongressionalDistrict = -1;
            int districtNumber = -1;
            if(int.TryParse(congressionalDistrict,out districtNumber))
            {
                newUser.CongressionalDistrict = districtNumber;
            }

            var salt = CryptoHelper.GenerateRandomString();
            newUser.Salt = salt;
            newUser.Password = CryptoHelper.HashAndSalt(password, salt);
            newUser.SessionId = CryptoHelper.HMACObject(CryptoHelper.GenerateRandomString(64), "TacoBellFridays", StringEncodingFormat.Base64).Left(128);

            Db.Users.Add(newUser);
            try {
                Db.SaveChanges();
            }
            catch (System.Exception ex) {
                var errors = Db.GetValidationErrors();
                errors.ToString();

            }

            try {
                EmailManager em = new EmailManager();
                em.SendMessage(newUser.Email, EmailManager.Support, "Get involved - WriteCongress.us", this.RenderPartialViewToString("~/Views/Email/SignupConfirm.cshtml", newUser));
                Logger.Trace("create user: created {0}", newUser.Email);
            }
            catch (System.Exception ex) {
                Logger.Error("create user: unable to send a welcome email to {0}", newUser.Email);
            }
            

            FormsAuthentication.SetAuthCookie(newUser.SessionId, true);
            return Redirect(redirect ?? Request.UrlReferrer.ToString());
        }
    }
}
