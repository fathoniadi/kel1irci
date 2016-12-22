using Irci.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irci.Models;
using Irci.Controllers;
namespace Irci.Controllers
{
    public class ProfileManagerController : Controller
    {
        // GET: ProfileManager
        ProfileHandler ph = new ProfileHandler();
        AuthController auh = new AuthController();
        List<Profile> profiles;
        Profile profile;
        public ActionResult Index()
        {
            profiles = ph.getProfiles();
            return View("Index", profiles);
        }

        [HttpPost]
        public ActionResult SearchProfile(String keyword)
        {
            profiles = ph.GetProfiles(keyword);
            return View("SearchProfile", profiles);
        }

        [HttpPost]
        public ActionResult MergeProfile(String[] profile)
        {
            ph.MergeProfile(profile);

            return View("MergeSuccess");
        }

        [HttpGet]
        public ActionResult ViewProfile(String idprofile)
        {

            profile = ph.GetOneProfile(idprofile);
            ViewData["profile"] = profile;
            return View();
        }


        public ActionResult CreateProfile()
        {
            Session["uu"] = 3;
            if (Session["uu"] != null) return View();
            else return RedirectToAction("login", "Auth");
        }

        [HttpPost]
        public ActionResult doCreateProfile(string nama, string instansi)
        {
            
            if (Session["uu"] != null)
            {
                System.Diagnostics.Debug.Write(nama + instansi);
                var id = Session["uu"].ToString();
                var ResultInsert = ph.createProfile(nama, instansi,Convert.ToInt32(id));
                var ResultUpdate = auh.updateAccountSetProfileMain(ResultInsert, int.Parse(id));
                return RedirectToAction("index", "searchProfile");
            }
            else return RedirectToAction("login", "Auth");
        }
    }
}