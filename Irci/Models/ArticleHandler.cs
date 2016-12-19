using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irci.Entity;
using Npgsql;

namespace Irci.Models
{
    public class ArticleHandler
    {
        private NpgsqlConnection dbCon;
        private NpgsqlCommand dbCmd;
        private List<Article> articles;
        public ArticleHandler()
        {
            dbCon = new Connection().getConnection();
            dbCon.Open();
            dbCmd = new NpgsqlCommand();
            articles = new List<Article>();
            dbCmd.Connection = dbCon;
        }

        public List<Article> getArticles()
        {
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = "SELECT title, date_submission, bahasa, description, publisher, resource_identifier, author_creator FROM irci.records LIMIT 5";
            try
            {
                var result = dbCmd.ExecuteReader();

                var counter = 0;
                while (result.Read())
                {
                    var artikelbaru = new Article() {Judul = result[0].ToString(), Submission = result[1].ToString(), Bahasa = result[2].ToString(), Deskripsi = result[3].ToString(), Publisher = result[4].ToString(), URL = result[5].ToString()};
                    List<string> temp = new List<string>();
                    foreach(var value in result[6] as String[])
                    {
                        temp.Add(value);
                    }
                    artikelbaru.Author = temp;
                    articles.Add(artikelbaru);
                }
            }catch(Exception e)
            {
                System.Diagnostics.Debug.Write(e);
            }
            dbCmd.Connection.Close();
        // insertNewArticle(articles[0]);
            return articles;
        }
       
        public int insertNewArticle(Article article)
        {
            dbCmd.Connection = dbCon;

            var idArticle = 0;
            System.Diagnostics.Debug.Write( article.Submission.Split(' ')[0]);

            dbCmd.CommandText = "insert into new_irci.article (judularticle,submissiondatearticle,bahasaarticle,deskripsiarticle,publisherarticle,urlarticle) values('"+article.Judul+"', to_date('"+article.Submission+"','MM/DD/YYYY'),'"+article.Bahasa+"','"+article.Deskripsi+"','"+article.Publisher+"','"+article.URL+"')";
            dbCmd.CommandType = System.Data.CommandType.Text;
            try
            {
                dbCmd.Connection.Open();
                dbCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("berhasil input");
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            System.Diagnostics.Debug.WriteLine(article.Judul + "inserted");
            dbCmd.Connection.Close();
            dbCmd.Connection = dbCon;

            dbCmd.CommandText = "select idarticle from new_irci.article where judularticle ='"+article.Judul+"' and submissiondatearticle = to_date('"+article.Submission+"','MM/DD/YYYY')";
            

            try
            {
                var result = dbCmd.ExecuteReader();

                while(result.Read())
                {
                    idArticle = int.Parse(result[0].ToString());
                    //return idArticle;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            System.Diagnostics.Debug.WriteLine("inserted");

            dbCmd.Connection.Close();

            return idArticle;
        }
    }
}