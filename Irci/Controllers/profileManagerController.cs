using Irci.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irci.Models;

namespace Irci.Controllers
{
    public class ProfileManagerController : Controller
    {
        // GET: ProfileManager
        ProfileHandler ph = new ProfileHandler();
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
            return View("Profile", profiles);
        }

        public ActionResult ViewProfile(int idprofile)
        {
            var _profile = idprofile.ToString();
            profile = ph.GetOneProfile(_profile);

            return View();
        }
    }
}