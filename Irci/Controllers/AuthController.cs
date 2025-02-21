﻿using System;
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
        ProfileHandler ph = new ProfileHandler();
        string url = "http://riset.ajk.if.its.ac.id/kel1irci/";
        public ActionResult login()
        {
            if (Session["uu"] != null)
                return  Redirect(url+"searchProfile/index");
            return View("login");
        }

        public ActionResult register()
        {
            if (Session["uu"] != null) return Redirect(url + "searchProfile/index");
            return View("register");
        }
        [HttpPost]
        public ActionResult doLogin(string email, string password)
        {
            password = Crypto.SHA256(password);
            //System.Diagnostics.Debug.Write(email + password);
            List<string> flagLogin = auh.login(email, password);
            Dictionary<string, string> userData = new Dictionary<string, string>();
            if (flagLogin.Count>0)
            {


                userData.Add("email",email);
                userData.Add("idaccount", flagLogin[0]);
                userData.Add("roleaccount", flagLogin[1]);
                Session["uu"] = userData;
                Session["idprofile"] = ph.GetProfileFromAccount(flagLogin[0]);
                Session["namaprofile"] = ph.GetNamaProfileFromProfile(Session["idprofile"].ToString());

                System.Diagnostics.Debug.WriteLine("Login Session IDProfile = " + Session["idprofile"]);
                System.Diagnostics.Debug.WriteLine("Login Session NamaProfile = " + Session["namaprofile"]);

                return Redirect(url + "searchProfile/index");
            }
            else
            {
                Session["mess"] = "Sukses Register";
                return Redirect(url + "searchProfile/index");
            }

            //return View("register");

        }

        public ActionResult logut()
        {
            Session.Contents.Remove("uu");
            return Redirect(url + "searchProfile/index");
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
                 return Redirect(url + "Auth/register");
                //Redirect
            }
            else {
                Session["mess"] = "Gagal Register";
                return Redirect(url + "Auth/register");
            }

            //return View("register");

        }

        public ActionResult logout()
        {
            Session.Contents.Remove("uu");
            return Redirect(url + "searchProfile/index");

        }

        public bool checkProfile(int id)
        {
            string flagProfile = auh.checkProfileMain(id.ToString());
            System.Diagnostics.Debug.WriteLine("CHECK PROFILE => "+flagProfile.Length);
            if (flagProfile.Length == 0) return true;
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