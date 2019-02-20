using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend.Attirbute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : System.Attribute
    {
        private string Name { get; set; }
        public TableNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
