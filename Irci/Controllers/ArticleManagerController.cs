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
            foreach(var article in articles)
            {
                List<string> newAuthor = new List<string>();
                foreach(var author in article.Author)
                {
                    newAuthor.Add(author.Split(';')[0].Trim('*'));
                }
                article.Author = newAuthor;
            }
            return View("Index", articles);
        }

        public ActionResult GenerateNewArticle()
        {
            getArticles();
            List<string> toBeDeleted = new List<string>();
            foreach (var article in articles)
            {
                List<string> profileId = new List<string>();
                int idArticle=ah.insertNewArticle(article);
                foreach (var author in article.Author)
                {
                    System.Diagnostics.Debug.WriteLine(author.Split(';')[0]);
                    // System.Diagnostics
                    var _Nama = ""; var _Deskripsi = "";
                    if (author.IndexOf(';')>=0)
                    {
                        _Nama = author.Split(';')[0].Trim('*');
                        _Deskripsi = author.Split(';')[1];
                    }
                    else
                    {
                        _Nama = author.Split(';')[0].Trim('*');
                        _Deskripsi = "";
                    }
                    var idProfile = ph.insertProfile(new Profile() { Nama=_Nama,Deskripsi=_Deskripsi} );
                    profileId.Add(idProfile.ToString());
                    ph.insertAuthorship(profileId, idArticle.ToString());
                    System.Diagnostics.Debug.WriteLine("idProfile: " + idProfile);
                }
                toBeDeleted.Add(article.idrecord);
            }
            ah.deleteRecords(toBeDeleted);
            getArticles();
            foreach (var article in articles)
            {
                List<string> newAuthor = new List<string>();
                foreach (var author in article.Author)
                {
                    newAuthor.Add(author.Split(';')[0].Trim('*'));
                }
                article.Author = newAuthor;
            }
            return View("Index", articles);
        }

        [HttpPost]
        public ActionResult generateArticles(List<Article> listArticle)
        {
            System.Diagnostics.Debug.WriteLine("genereateArticles Called");
            foreach (var article in listArticle)
            {
                List<string> profileId = new List<string>();
                int idArticle = ah.insertNewArticle(article);
                foreach (var author in article.Author)
                {
                    System.Diagnostics.Debug.WriteLine(author.Split(';')[0]);
                    // System.Diagnostics
                    var _Nama = ""; var _Deskripsi = "";
                    if (author.IndexOf(';') >= 0)
                    {
                        _Nama = author.Split(';')[0].Trim('*');
                        _Deskripsi = author.Split(';')[1];
                    }
                    else
                    {
                        _Nama = author.Split(';')[0].Trim('*');
                        _Deskripsi = "";
                    }
                    var idProfile = ph.insertProfile(new Profile() { Nama = _Nama, Deskripsi = _Deskripsi });
                    profileId.Add(idProfile.ToString());
                    ph.insertAuthorship(profileId, idArticle.ToString());
                    System.Diagnostics.Debug.WriteLine("idProfile: " + idProfile);
                }
            }
            return View("Index", listArticle);
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