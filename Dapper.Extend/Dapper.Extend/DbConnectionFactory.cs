using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Site.Builder.Infrastructure.Data.Sql
{
    public static class DbConnectionFactory
    {
        private static IDbConnection GetConnection(DbEnum dbEnum)
        {
            if (dbEnum == DbEnum.MySql)
            {
                return new MySqlConnection();
            }
            return new SqlConnection();
        }

        public static IDbConnection GetMySqlConnection()
        {
            return GetConnection(DbEnum.MySql);
        }

        public static IDbConnection GetSqlConnection()
        {
            return GetConnection(DbEnum.SqlServer);
        }
    }
}
