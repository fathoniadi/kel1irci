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
        private List<string> ID;
        public ArticleHandler()
        {
            dbCon = new Connection().getConnection();
            dbCon.Open();
            dbCmd = new NpgsqlCommand();
        }

        public List<string> getArticles()
        {
            ID = new List<string>();
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = "SELECT title FROM irci.records";
            try
            {
                var result = dbCmd.ExecuteReader();

                var counter = 0;
                while (result.Read())
                {
                    counter++;
                    System.Diagnostics.Debug.Write("Result: " + result[0]);
                    ID.Add(result[0].ToString());
                    System.Diagnostics.Debug.Write("Counter:"+counter);
                }
                
            }catch(Exception e)
            {
                System.Diagnostics.Debug.Write(e);
            }
            return ID;
        }
    }
}