using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Irci.Controllers
{
    public class searchProfileController : Controller
    {
        // GET: ProfileManager
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult search_profile(string keywordSearch)
        {
            TempData["searchProfile"] = keywordSearch;
            return View();
        }
    }
}