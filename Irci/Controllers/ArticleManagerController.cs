using Irci.Entity;
using System.Collections.Generic;
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
            if(articles!=null)
                articles.Clear();
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

        [HttpPost]
        public ActionResult GenerateNewArticle(Article listArticle)
        {
            articles = ah.getArticles();
            List<string> toBeDeleted = new List<string>();
            foreach (var article in articles)
            {
                List<string> profileId = new List<string>();
                string idArticle=ah.insertNewArticle(article);
                System.Diagnostics.Debug.WriteLine("id article: " + idArticle);
                foreach (var author in article.Author)
                {
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
                    System.Diagnostics.Debug.WriteLine("idProfile: " + idProfile);
                    profileId.Add(idProfile.ToString());
                    if(idArticle!="" && idProfile != "")
                    {
                        ph.insertAuthorship(profileId, idArticle.ToString());
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("tidak memasukkan ke authorhip karena id article or/and idprofile null");
                    }
                    System.Diagnostics.Debug.WriteLine("idProfile: " + idProfile);
                }
                toBeDeleted.Add(article.idrecord);
            }
            ah.deleteRecords(toBeDeleted);
            return RedirectToAction("Index");
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