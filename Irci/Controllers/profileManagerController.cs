using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irci.Entity;
using Irci.Models;


namespace Irci.Controllers
{
    public class profileManagerController : Controller
    {
        // GET: profileManager
        public ActionResult Index()
        {
            return View();
        }


        public bool profileCreator(List<Article> articles)
        {
            return true;
        }
    }
}