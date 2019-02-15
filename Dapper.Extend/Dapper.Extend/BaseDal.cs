using Dapper.Extend;
using Dapper.Extend.Data.Sql.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend
{
    public abstract class BaseDal<TPrimary, TEntity>
        where TPrimary : struct
        where TEntity : class
    {
        private DapperExtension _dapperExtension;
        protected BaseDal(DbEnum dbEnum, string connectionString)
        {
            if (dbEnum == DbEnum.MySql)
            {
                this._dapperExtension = DapperExtension.UseMySql(connectionString);
            }
            else if (dbEnum == DbEnum.SqlServer)
            {
                this._dapperExtension = DapperExtension.UseSqlServer(connectionString);
            }
            else
            {
                throw new Exception("未知的数据库类型");
            }
        }

        protected TPrimary Insert(TEntity entity)
        {
            SqlObject sqlObject = SqlBuilder<TEntity>.BuildInsert(entity);
            return this._dapperExtension.Insert<TPrimary>(sqlObject.Sql, sqlObject.Parameters);
        }

        protected int Update(TEntity entity)
        {
            SqlObject sqlObject = SqlBuilder<TEntity>.BuildUpdate(entity);
            return this._dapperExtension.Update(sqlObject.Sql, sqlObject.Parameters);
        }

        protected IEnumerable<TEntity> Select(TEntity entity)
        {
            return this._dapperExtension.Select<TEntity>(string.Empty, null);
        }

    }
}
