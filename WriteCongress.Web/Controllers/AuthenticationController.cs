using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WriteCongress.Core;

namespace WriteCongress.Web.Controllers
{
    public class JsonServiceResult<T> {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }


        public JsonServiceResult(bool success, string message = null) {
            Success = success;
        }

        public JsonServiceResult(T data, bool success, string message = null) {
            Data = data;
            Success = success;
        }
    }



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
            if (Db.Users.Any(u => u.Email == email)) {
                return Json(new JsonServiceResult<bool>(false, "Email already exsists"));
            }else {
                return Json(new JsonServiceResult<bool>(true));
            }
        }
      
        public ActionResult Signout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SignIn(string email, string password, string redirect) {
            var user = Db.Users.SingleOrDefault(u => u.Email == email);
            if (user != null) {
                var hashedPassword = CryptoHelper.HashAndSalt(password, user.Salt);
                if (hashedPassword == user.Password) {
                    user.SessionId = CryptoHelper.HMACObject(CryptoHelper.GenerateRandomString(64), "TacoBellFridays", StringEncodingFormat.Base64).Left(128);
                    FormsAuthentication.SetAuthCookie(user.SessionId, true);
                    Db.SaveChanges();
                    return Redirect(redirect ?? Request.UrlReferrer.ToString());
                }
                else {
                    return RedirectToAction("Signin", "Authentication", new { FailedLogin = true, Redirect = redirect ?? Request.UrlReferrer.ToString() });
                }
            }
            else {
                return RedirectToAction("Signin", "Authentication", new { FailedLogin = true, Redirect = redirect ?? Request.UrlReferrer.ToString() });
            }

        }

        [HttpPost]
        public ActionResult CreateAccount(string firstname, string lastname, string address1, string address2, string city, string state, string zipcode, string email, string password,string redirect) {
            if (Db.Users.Any(u => u.Email == email)) {
                return Redirect(redirect ?? Request.UrlReferrer.ToString());
            }
        
            WriteCongress.Core.User newUser = new User();
            newUser.Identity = CryptoHelper.GenerateRandomString(64);
            newUser.CreatedDateUtc = DateTime.UtcNow;
            newUser.Email = email;
            newUser.FirstName = firstname;
            newUser.LastName = lastname;
            newUser.AddressOne = address1;
            newUser.AddressTwo = address2;
            newUser.City = city;
            newUser.State = state;
            newUser.ZipCode = zipcode;

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

            FormsAuthentication.SetAuthCookie(newUser.SessionId, true);
            return Redirect(redirect ?? Request.UrlReferrer.ToString());
        }
    }
}
