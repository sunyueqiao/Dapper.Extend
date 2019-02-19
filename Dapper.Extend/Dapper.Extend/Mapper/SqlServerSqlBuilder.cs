using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Extend.SqlObject.Mapper;

namespace Dapper.Extend.Mapper
{
    public class SqlServerSqlBuilder<T> : BaseSqlBuilder<T> where T : class
    {
        public override SqlObjectData BuildInsert(T t)
        {
            SqlObjectData sqlObjectData = base.BuildInsert(t);
            if (sqlObjectData.ExistIdentityColumn)
            {
                StringBuilder sql = new StringBuilder(sqlObjectData.Sql);
                sql.Append(";SELECT @@IDENTITY;");
                sqlObjectData.Sql = sql.ToString();
            }

            return sqlObjectData;
        }

        public static SqlServerSqlBuilder<T> Build()
        {
            return SqlBuilder.Build();
        }

        public static class SqlBuilder
        {
            public static SqlServerSqlBuilder<T> mysqlSqlBuilder = new SqlServerSqlBuilder<T>();
            public static SqlServerSqlBuilder<T> Build()
            {
                return mysqlSqlBuilder;
            }
        }
    }
}
