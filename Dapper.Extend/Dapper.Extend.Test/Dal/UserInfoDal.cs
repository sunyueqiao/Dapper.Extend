using Dapper.Extend.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Extend.Test.Dal
{
    public class UserInfoDal : MySqlDal<long, UserInfo>
    {
        public void Test()
        {
        }
    }
}
