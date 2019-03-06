using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extend.Test.Dal
{
    public class MySqlDal<TPrimary, TEntity> : Extend.MySqlDal<TPrimary, TEntity> where TPrimary : struct where TEntity : class
    {
        protected static string ConnectionString
        {
            get
            {
                return "Database=dapper_extend;Data Source=127.0.0.1;Port=3306;User Id=root;Password=1qaz@WSX;Charset=utf8;TreatTinyAsBoolean=false;";
            }
        }

        public MySqlDal() : base(ConnectionString)
        {

        }
    }
}
