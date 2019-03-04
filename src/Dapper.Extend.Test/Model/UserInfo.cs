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
        [Identity]
        [ColumnName("user_id")]
        public long? UserId { get; set; }

        [ColumnName("user_name")]
        public string UserName { get; set; }

        public List<UserContact> UserContact { get; set; }
    }
}
