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
        string url = "http://riset.ajk.if.its.ac.id/kel1irci/";
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
            Dictionary<string, string> userData = (Dictionary<string,string>)Session["uu"];
            var idaccount = userData["idaccount"];
            ph.MergeProfile(profile,idaccount);
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
            //Session["uu"] = 3;
            if (Session["uu"] != null)
            {

                Dictionary<string, string> userData = (Dictionary<string, string>)Session["uu"];
                var id = int.Parse(userData["idaccount"]);
                //var id = int.Parse(Session["uu"].ToString());
                var flagProfile = auh.checkProfile(id);
                if (flagProfile == false) return RedirectToAction("index", "searchProfile");
                else return View();
            }
            else return Redirect(url + "Auth/login");
        }

        [HttpPost]
        public ActionResult doCreateProfile(string nama, string instansi)
        {
            
            if (Session["uu"] != null)
            {
                System.Diagnostics.Debug.Write(nama + instansi);
                Dictionary<string, string> userData = (Dictionary<string, string>)Session["uu"];
                var id = userData["idaccount"];
                var ResultInsert = ph.createProfile(nama, instansi,Convert.ToInt32(id));
                Session["idprofile"] = ResultInsert;
                Session["namaprofile"] = ph.GetNamaProfileFromProfile(ResultInsert.ToString());

                System.Diagnostics.Debug.WriteLine("Session IDProfile = " + Session["idprofile"]);
                System.Diagnostics.Debug.WriteLine("Session NamaProfile = " + Session["namaprofile"]);

                var ResultUpdate = auh.updateAccountSetProfileMain(ResultInsert, int.Parse(id));
                return RedirectToAction("index", "searchProfile");
            }
            else return Redirect(url + "Auth/login");
        }
    }
}