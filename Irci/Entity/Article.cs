using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Irci.Entity
{
    public class Article
    {
        public string ID { get; set; }
        public string IDJurnal { get; set; }
        public string Judul { get; set; }
        public string Submission { get; set; }
        public string Bahasa { get; set; }
        public string Deskripsi { get; set; }
        public string Publisher { get; set; }
        public List<string> Author { get; set; }
        public string URL { get; set; }
    }
}