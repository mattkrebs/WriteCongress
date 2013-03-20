﻿using System;
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

        [HttpPost]
        public JsonResult GetLetterPrefill()
        {
            return Json(Db.Users.Where(u => u.SessionId == User.Identity.Name).Select(ui => new {
                ui.FirstName,
                ui.LastName,
                ui.AddressOne,
                ui.AddressTwo,
                ui.Email,
                ui.PhoneNumber,
                ui.ZipCode,
                ui.City,
                ui.State
            }).FirstOrDefault(), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult CreateAccount(string firstname, string lastname, string address1, string address2, string city, string state, string zipcode, string email, string password,string redirect) {

            WriteCongress.Core.User newUser = new User();
            newUser.Identity = CryptoHelper.GenerateRandomString(64);
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
