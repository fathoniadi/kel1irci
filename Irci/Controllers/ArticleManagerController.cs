using Irci.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irci.Models;

namespace Irci.Controllers
{
    public class ArticleManagerController : Controller
    {
        // GET: ArticleManager
        ArticleHandler ah = new ArticleHandler();
        List<Article> articles;
        public ActionResult Index()
        {
            return View(articles);
        }

        public ActionResult GenerateNewArticle()
        {
            articles = ah.getArticles();
            for (int i=0; i<articles.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(articles[i]);
            }

            System.Diagnostics.Debug.WriteLine("==================================================================================================");

            return View("Index", articles);
        }
    }
}