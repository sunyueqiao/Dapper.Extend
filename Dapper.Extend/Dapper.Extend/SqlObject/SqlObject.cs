using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend.Data.Sql.Mapper
{
    public class SqlObject
    {
        public string Sql { get; set; }
        public DynamicParameters Parameters { get; set; }
    }
}
