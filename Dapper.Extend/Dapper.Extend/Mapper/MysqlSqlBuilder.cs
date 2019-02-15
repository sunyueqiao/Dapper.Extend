using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Extend.SqlObject.Mapper;

namespace Dapper.Extend.Mapper
{
    public class MysqlSqlBuilder<T> : BaseSqlBuilder<T> where T : class
    {
        public override SqlObjectData BuildInsert(T t)
        {
            return base.BuildInsert(t);
        }

        public static MysqlSqlBuilder<T> Build()
        {
            return SqlBuilder.Build();
        }

        public static class SqlBuilder
        {
            public static MysqlSqlBuilder<T> mysqlSqlBuilder = new MysqlSqlBuilder<T>();
            public static MysqlSqlBuilder<T> Build()
            {
                return mysqlSqlBuilder;
            }
        }
    }
}
