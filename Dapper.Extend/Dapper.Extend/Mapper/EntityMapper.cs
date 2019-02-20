using Dapper.Extend.Data.Sql.Attirbute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dapper.Extend.Mapper
{
    public class EntityMapper<T> where T : class
    {
        public string ClassFullName
        {
            get; set;
        }
        public string ClassName
        {
            get; set;
        }
        public string TableName
        {
            get; set;
        }

        /// <summary>
        /// 自增列
        /// </summary>
        public RelationMapper RelationIdentity
        {
            get; set;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public List<RelationMapper> RelationPrimarys
        {
            get; set;
        }

        public List<RelationMapper> RelationColumns
        {
            get; set;
        }

        public List<string> PrimaryNames
        {
            get
            {
                return this.RelationPrimarys.Select(p => p.ColumnName).ToList();
            }
        }

        public string IdentityColumnName
        {
            get
            {
                return this.RelationIdentity.ColumnName;
            }
        }

        public List<string> ColumnNames
        {
            get
            {
                return this.RelationColumns.Select(p => p.ColumnName).ToList();
            }
        }

        public List<string> PropertyNames
        {
            get
            {
                return this.RelationColumns.Select(p => p.PropertyName).ToList();
            }
        }


        /// <summary>
        /// 过滤掉自增列
        /// </summary>
        /// <returns></returns>
        public EntityMapper<T> FilterIdentity()
        {
            this.RelationColumns.RemoveAll((item) =>
            {
                return item != null && this.RelationIdentity != null && string.Equals(this.RelationIdentity.ColumnName, item.ColumnName);
            });

            return this;
        }

        /// <summary>
        /// 过滤掉主键
        /// </summary>
        /// <returns></returns>
        public EntityMapper<T> FilterPrimaryKey()
        {
            this.RelationColumns.RemoveAll((item) =>
            {
                if (this.RelationPrimarys == null || !this.RelationPrimarys.Any())
                {
                    return false;
                }
                foreach (RelationMapper mapper in this.RelationPrimarys)
                {
                    if (string.Equals(mapper.ColumnName, item.ColumnName))
                    {
                        return true;
                    }
                    continue;
                }

                return false;
            });

            return this;
        }
    }
}