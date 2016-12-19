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
<<<<<<< Updated upstream
            this.conn = new NpgsqlConnection("Host=192.168.0.16;Username=postgres;Password=;Database=irci");
=======
            this.conn = new NpgsqlConnection("Host=10.151.36.5;Username=postgres;Password=;Database=irciDump");
>>>>>>> Stashed changes
        }
        public NpgsqlConnection getConnection()
        {
            return this.conn;
        }
    }
}