using System;
using System.Collections.Generic;
using Npgsql;
using System.Data.Common;
using QR_Catalog.Models;
using System.Text;

namespace QR_Catalog.Services
{
    public class PostgreConnectionService : IConnectionService
    {
        public RemoteDB db;

        public string GetConnectionString()
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder
            {
                Host = db.Host,
                Port = Convert.ToInt32(db.Port),
                Database = db.Database.Substring(1),
                Username = db.User,
                Password = db.Password
            };

            return builder.ConnectionString;
        }

        public DbConnection Connection()
        {
            return new NpgsqlConnection(GetConnectionString());
        }
    }
}
