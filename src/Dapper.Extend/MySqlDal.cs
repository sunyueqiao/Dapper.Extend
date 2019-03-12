using Dapper.Extend.Extension;
using Dapper.Extend.Mapper;
using Dapper.Extend.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend
{
    public abstract class MySqlDal<TPrimary, TEntity> : SqlDal<TPrimary, TEntity>
        where TPrimary : struct
        where TEntity : class
    {
        protected MySqlDal(string connectionString) : base(connectionString, MysqlSqlBuilder<TEntity>.Build(), DapperExtension.UseMySql(connectionString))
        {
        }
    }
}
