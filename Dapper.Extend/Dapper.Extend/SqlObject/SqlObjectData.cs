using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend.SqlObject.Mapper
{
    public class SqlObjectData
    {
        public string Sql { get; set; }
        public DynamicParameters Parameters { get; set; }
    }
}
