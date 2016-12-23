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
            if (Session["uu"] != null) return RedirectToAction("index", "searchProfile");
            return View("login");
        }

        public ActionResult register()
        {
            if (Session["uu"] != null) return RedirectToAction("index", "searchProfile");
            return View("register");
        }
        [HttpPost]
        public ActionResult doLogin(string email, string password)
        {
            password = Crypto.SHA256(password);
            //System.Diagnostics.Debug.Write(email + password);
            var flagLogin = auh.login(email, password);
            Dictionary<string, string> userData = new Dictionary<string, string>();
            if (flagLogin != null)
            {


                userData.Add("email",email);
                userData.Add("idaccount", flagLogin[0]);
                userData.Add("roleaccount", flagLogin[1]);
                Session["uu"] = userData;
                return RedirectToAction("Index", "searchProfile");
            }
            else
            {
                Session["mess"] = "Sukses Register";
                return RedirectToAction("Login", "Auth");
            }

            //return View("register");

        }

        public ActionResult logut()
        {
            Session.Contents.Remove("uu");
            return RedirectToAction("index", "searchProfile");
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

            //return View("register");

        }

        public ActionResult logout()
        {
            Session.Contents.Remove("uu");
            return RedirectToAction("index", "searchProfile");
            
        }

        public bool checkProfile(int id)
        {
            var flagProfile = auh.checkProfileMain(id.ToString());
            if (flagProfile != "") return true;
            else return false;
        }


        public bool updateAccountSetProfileMain(int idprofile, int idaccount)
        {
            //var idaccount = Convert.ToInt32(Session["uu"].ToString());
            System.Diagnostics.Debug.WriteLine("Idprofile di auth "+idprofile);
            var result = auh.updateAccountProfile(idprofile, idaccount);
            return true;
        }

    }


}