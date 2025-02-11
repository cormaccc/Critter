﻿using Microsoft.Data.Sqlite;
using System.Data;

namespace TwitterCloneApp.Contexts
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateSQLiteConnnection() => new SqliteConnection(_connectionString);
    }
}
