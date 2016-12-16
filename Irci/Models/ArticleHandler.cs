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
        }

        public List<Article> getArticles()
        {
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = "SELECT title, date_submission, bahasa, description, publisher, resource_identifier FROM irci.records LIMIT 5";
            try
            {
                var result = dbCmd.ExecuteReader();

                var counter = 0;
                while (result.Read())
                {
                    articles.Add(new Article() { Judul = result[0].ToString(), Submission = result[1].ToString(), Bahasa = result[2].ToString(), Deskripsi = result[3].ToString(), Publisher = result[4].ToString(), URL = result[5].ToString() });
                }
                
            }catch(Exception e)
            {
                System.Diagnostics.Debug.Write(e);
            }
            return articles;
        }
    }
}