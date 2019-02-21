using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend.Mapper
{
    public class RelationMapper
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName
        {
            get; set;
        }

        /// <summary>
        /// 属性值
        /// </summary>
        public object PropertyValue
        {
            get;set;
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            get; set;
        }
    }
}
