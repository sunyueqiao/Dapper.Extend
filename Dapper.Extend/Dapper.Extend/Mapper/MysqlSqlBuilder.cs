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
            SqlObjectData sqlObjectData = base.BuildInsert(t);
            if (sqlObjectData.ExistIdentityColumn)
            {
                StringBuilder sql = new StringBuilder(sqlObjectData.Sql);
                sql.Append(";select @@IDENTITY;");
            }

            return sqlObjectData;
        }

        public override SqlObjectData BuildDelete(T t)
        {
            return base.BuildDelete(t);
        }

        public override SqlObjectData BuildSelect(T t)
        {
            return base.BuildSelect(t);
        }

        public override SqlObjectData BuildUpdate(T t)
        {
            return base.BuildUpdate(t);
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
