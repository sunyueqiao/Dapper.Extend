using Dapper.Extend.Attirbute;
using Dapper.Extend.Data.Sql.Attirbute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extend.Test.Model
{
    [TableName("user_info")]
    public class UserInfo
    {
        [PrimaryKey]
        [ColumnName("user_id")]
        public long UserId { get; set; }

        [ColumnName("user_name")]
        public string UserName { get; set; }
    }
}
