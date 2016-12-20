using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Irci.Models;
namespace Irci.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        AuthHandler auh = new AuthHandler();
        public ActionResult login()
        {

            return View("login");
        }

        public ActionResult register()
        {

            return View("register");
        }
        [HttpPost]
        public ActionResult doLogin(string email, string password)
        {
            password = Crypto.SHA256(password);
            System.Diagnostics.Debug.Write(email + password);
            var flagLogin = auh.login(email, password);
            if (flagLogin != "")
            {
                Session["uu"] = flagLogin;
                return RedirectToAction("Index", "searchProfile");
            }
            else
            {
                Session["mess"] = "Sukses Register";
                return RedirectToAction("Login", "Auth");
            }

            return View("register");

        }
        [HttpPost]
        public ActionResult doRegister(string email, string password)
        {
            password = Crypto.SHA256(password);
            System.Diagnostics.Debug.Write(email + password);
            var flagRegister = auh.register(email, password);

            if (flagRegister != "")
            {
                Session["mess"] = "Sukses Register";
                return RedirectToAction("Register", "Auth");
                //Redirect
            }
            else {
                Session["mess"] = "Gagal Register";
                return RedirectToAction("Register", "Auth");
            }

            return View("register");

        }
    }
}