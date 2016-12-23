using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Irci.Models
{
    public class Connection
    {
        private NpgsqlConnection conn;
        public Connection()
        {
            this.conn = new NpgsqlConnection("Host=10.151.36.139;Username=postgres;Password=sandi;Database=irci");
        }
        public NpgsqlConnection getConnection()
        {
            return this.conn;
        }
    }
}