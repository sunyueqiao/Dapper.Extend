using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend.Attirbute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : System.Attribute
    {
        public string Name { get; set; }

        public ColumnNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
