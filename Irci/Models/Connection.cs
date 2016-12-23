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
            this.conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=postgres;Database=irci");
        }
        public NpgsqlConnection getConnection()
        {
            return this.conn;
        }
    }
}