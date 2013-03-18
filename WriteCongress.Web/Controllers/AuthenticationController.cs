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
        public JsonResult CreateAccount(string firstname, string lastname, string address1, string address2, string city, string state, string zip, string email, string password) {
            if (Db.Users.Any(u => u.Email == email)) {
                return Json(new JsonServiceResult<bool>(false, "Email already exsists"));
            }

            WriteCongress.Core.User newUser = new User();
            newUser.Identity = CryptoHelper.GenerateRandomString(64);
            newUser.Email = email;
            newUser.FirstName = firstname;
            newUser.LastName = lastname;
            newUser.AddressOne = address1;
            newUser.AddressTwo = address2;
            newUser.City = city;
            newUser.State = state;
            newUser.ZipCode = zip;

            var salt = CryptoHelper.GenerateRandomString();
            newUser.Salt = salt;
            newUser.Password = CryptoHelper.HashAndSalt(password, salt);

            Db.Users.Add(newUser);
            Db.SaveChanges();

            return Json(new JsonServiceResult<bool>(true));
        }
    }
}
