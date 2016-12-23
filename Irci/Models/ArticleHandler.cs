using System;
using System.Collections.Generic;
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
            dbCmd.CommandText = "SELECT title, date_submission, bahasa, description, publisher, resource_identifier, author_creator, id_record FROM irci.records LIMIT 5 OFFSET 0";
            try
            {
                if (dbCmd.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbCmd.Connection.Open();
                }

                dbCmd.CommandTimeout = 0;
                var result = dbCmd.ExecuteReader();

                while (result.Read())
                {
                    var artikelbaru = new Article() {Judul = result[0].ToString(), Submission = result[1].ToString(), Bahasa = result[2].ToString(), Deskripsi = result[3].ToString(), Publisher = result[4].ToString(), URL = result[5].ToString(), idrecord=result[7].ToString()};
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
            return articles;
        }

        public void deleteRecords(List<string> idRecords)
        {
            string strCommand="delete from irci.records where id_record in (";
            foreach(var idRecord in idRecords)
            {
                strCommand += "'"+idRecord+"',";
            }
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = strCommand.Trim(',')+")";
            dbCmd.CommandType = System.Data.CommandType.Text;
            try
            {
                dbCmd.Connection.Open();
            }
            catch (InvalidOperationException e)
            {

            }

            try
            {
                dbCmd.ExecuteReader();
                dbCmd.Connection.Close();
            }
            catch (Exception e)
            {

            }
        }
       
        public string insertNewArticle(Article article)
        {
            dbCmd.Connection = dbCon;

            var idArticle = "";

            dbCmd.CommandText = "insert into new_irci.article (judularticle,submissiondatearticle,bahasaarticle,deskripsiarticle,publisherarticle,urlarticle) values('"+article.Judul+"', to_date('"+article.Submission+"','MM/DD/YYYY'),'"+article.Bahasa+"','"+article.Deskripsi+"','"+article.Publisher+"','"+article.URL+"') RETURNING idarticle";
            dbCmd.CommandType = System.Data.CommandType.Text;
            try
            {
                dbCmd.Connection.Open();
            }
            catch (InvalidOperationException e)
            {
                System.Diagnostics.Debug.WriteLine("masalah open koneksi database");
            }
            try
            {
                var result = dbCmd.ExecuteReader();
                
                while (result.Read())
                {
                    idArticle = result[0].ToString();
                }
                dbCmd.Connection.Close();
                System.Diagnostics.Debug.WriteLine("berhasil input");
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return idArticle;
        }
    }
}