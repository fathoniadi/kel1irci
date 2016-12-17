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
            
            articles = ah.getArticles();
            var idarticle = ah.insertNewArticle(articles[0]);
            System.Diagnostics.Debug.WriteLine(idarticle);
            foreach (var article in articles)
            {

            }
            for (int i = 0; i < articles.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(articles[i]);
            }

            System.Diagnostics.Debug.WriteLine("==================================================================================================");

            return View("Index", articles);
        }

        public ActionResult GenerateNewArticle()
        {
            return View();
        }
    }
}