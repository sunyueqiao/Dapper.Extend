using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Dapper.Extend.Extension
{
    class MySqlConnectionStrategy : IDbConnectionStrategy
    {
        public IDbConnection InitializeConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }

    class SqlServerConnectionStrategy : IDbConnectionStrategy
    {
        public IDbConnection InitializeConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
