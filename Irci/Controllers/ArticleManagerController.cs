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
        ProfileHandler ph = new ProfileHandler();
        List<Article> articles;
        public ActionResult Index()
        {
            getArticles();
            return View("Index", articles);
        }

        public ActionResult GenerateNewArticle()
        {
            getArticles();
            foreach(var article in articles)
            {
                List<string> profileId = new List<string>();
                int idArticle=ah.insertNewArticle(article);
                foreach(var author in article.Author)
                {
                    System.Diagnostics.Debug.WriteLine(author.Split(';')[0]);
                    // System.Diagnostics
                    var _Nama = ""; var _Deskripsi = "";
                    if (author.IndexOf(';')>=0)
                    {
                        _Nama = author.Split(';')[0];
                        _Deskripsi = author.Split(';')[1];
                    }
                    else
                    {
                        _Nama = author.Split(';')[0];
                        _Deskripsi = "";
                    }
                    var idProfile = ph.insertProfile(new Profile() { Nama=_Nama,Deskripsi=_Deskripsi} );
                    profileId.Add(idProfile.ToString());
                    ph.insertAuthorship(profileId, idArticle.ToString());
                    System.Diagnostics.Debug.WriteLine("idProfile: " + idProfile);
                }
            }
            return View("Index",articles);
        }

        public void getArticles()
        {
            articles = ah.getArticles();
            for (int i = 0; i < articles.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(articles[i]);
            }

            System.Diagnostics.Debug.WriteLine("==================================================================================================");
        }
    }
}