using Dapper.Extend.Data.Sql.Attirbute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend.SqlObject.Mapper
{
    public class TableObject<T> where T : class
    {
        public string TableName
        {
            get; set;
        }

        public string IdentityColumn
        {
            get; set;
        }

        public List<string> PrimaryColumns
        {
            get; set;
        }

        public List<string> Columns
        {
            get; set;
        }

        public TableObject<T> FilterIdentity()
        {
            this.Columns.RemoveAll((item) =>
            {
                return EntityAttributeUtil<T>.IsIdentity(item);
            });

            return this;
        }

        public TableObject<T> FilterPrimaryKey()
        {
            this.Columns.RemoveAll((item) =>
            {
                return EntityAttributeUtil<T>.IsPrimaryKey(item);
            });

            return this;
        }
    }
}
