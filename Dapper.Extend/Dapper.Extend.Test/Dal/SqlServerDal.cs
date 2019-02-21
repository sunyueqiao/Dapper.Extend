using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extend.Test.Dal
{
    public class SqlServerDal<TPrimary, TEntity> : BaseDal<TPrimary, TEntity>
        where TPrimary : struct where TEntity : class
    {
        private static string ConnectionString
        {
            get
            {
                return "";
            }
        }

        public SqlServerDal() : base(DbEnum.SqlServer, ConnectionString)
        {
        }
    }
}
