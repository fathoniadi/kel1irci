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
        public ActionResult Index()
        {
            var articles = from article in GetAllArticle() orderby article.ID select article;
            return View(articles);
        }

        public ActionResult GenerateNewArticle()
        {
            List<string> ID = ah.getArticles();
            if (ID == null)
            {
                System.Diagnostics.Debug.WriteLine("IDnya NULL");
                return View("GenerateNewArticle");
            }
            System.Diagnostics.Debug.Write(ID);

            return View("GenerateNewArticle", ID);
        }

        [NonAction]
        public List<Article> GetAllArticle()
        {
            return new List<Article>
            {
                new Article {
                    ID ="1",
                    Judul ="Test1",
                    Isi ="ini isi",
                    Author ="Syukron"
                },
                new Article {
                    ID ="2",
                    Judul ="Test2",
                    Isi ="ini isi",
                    Author ="Muhsin"
                },
                new Article {
                    ID ="3",
                    Judul ="Test2",
                    Isi ="ini isi",
                    Author ="Kunto"
                },
                new Article {
                    ID ="4",
                    Judul ="Test2",
                    Isi ="ini isi",
                    Author ="KurKur"
                },
            };
        }
    }
}