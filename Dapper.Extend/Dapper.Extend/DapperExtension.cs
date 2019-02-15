using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dapper;

namespace Site.Builder.Infrastructure.Data.Sql
{
    public class DapperExtension
    {
        /// <summary>
        /// 
        /// </summary>
        private IDbConnection DbConnection { get; set; }
        private DapperExtension() { }

        public static DapperExtension UseMySql(string connectionString)
        {
            DapperExtension dapperHelper = DapperHelperBuilder.build();
            dapperHelper.DbConnection = DbConnectionFactory.GetMySqlConnection();
            return dapperHelper;
        }

        public static DapperExtension UseSqlServer(string connectionString)
        {
            DapperExtension dapperHelper = DapperHelperBuilder.build();
            dapperHelper.DbConnection = DbConnectionFactory.GetSqlConnection();
            return dapperHelper;
        }

        public int Insert(string sql, DynamicParameters parameters)
        {
            return SqlMapper.Execute(this.DbConnection, sql, parameters);
        }

        public int Update(string sql, DynamicParameters parameters)
        {
            return SqlMapper.Execute(this.DbConnection, sql, parameters);
        }

        public int Delete(string sql, DynamicParameters parameters)
        {
            return SqlMapper.Execute(this.DbConnection, sql, parameters);
        }

        public IEnumerable<T> Select<T>(string sql, DynamicParameters parameters) where T : class
        {
            return SqlMapper.Query<T>(this.DbConnection, sql, parameters);
        }

        public SqlMapper.GridReader SelectMutiple(string sql, DynamicParameters parameters)
        {
            return SqlMapper.QueryMultiple(this.DbConnection, sql, parameters);
        }

        public T SelectOne<T>(string sql, DynamicParameters parameters)
        {
            return SqlMapper.QuerySingle<T>(this.DbConnection, sql, parameters);
        }

        private static class DapperHelperBuilder
        {
            private static DapperExtension dapperHelper = new DapperExtension();

            internal static DapperExtension build()
            {
                return dapperHelper;
            }
        }
    }
}
