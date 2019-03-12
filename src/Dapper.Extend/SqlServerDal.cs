using Dapper.Extend.Extension;
using Dapper.Extend.Mapper;
using Dapper.Extend.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend
{
    public abstract class SqlServerDal<TPrimary, TEntity> : SqlDal<TPrimary, TEntity>
        where TPrimary : struct
        where TEntity : class
    {
        private readonly DapperExtension _dapperExtension;
        protected SqlServerDal(string connectionString) : base(connectionString, SqlServerSqlBuilder<TEntity>.Build(), DapperExtension.UseSqlServer(connectionString))
        {
        }
    }
}
