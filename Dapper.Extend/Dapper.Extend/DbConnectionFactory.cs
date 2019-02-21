using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Dapper.Extend
{
    public static class DbConnectionFactory
    {
        private static IDbConnection GetConnection(DbEnum dbEnum, string connectionString)
        {
            if (dbEnum == DbEnum.MySql)
            {
                return new MySqlConnection(connectionString);
            }
            else if (dbEnum == DbEnum.SqlServer)
            {
                return new SqlConnection(connectionString);
            }

            throw new Exception("未知的数据库类型");

        }

        public static IDbConnection GetMySqlConnection(string connectionString)
        {
            return GetConnection(DbEnum.MySql, connectionString);
        }

        public static IDbConnection GetSqlConnection(string connectionString)
        {
            return GetConnection(DbEnum.SqlServer, connectionString);
        }
    }
}
