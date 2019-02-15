using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dapper.Extend.Data.Sql.Attirbute
{
    public static class EntityAttributeUtil<T> where T : class
    {
        private static string PrimaryAttrFullName
        {
            get
            {
                return typeof(PrimaryKeyAttribute).FullName;
            }
        }

        private static string IdentityAttrFullName
        {
            get
            {
                return typeof(IdentityAttribute).FullName;
            }
        }

        public static bool IsPrimaryKey(string propertyName)
        {
            return AssertCustomerAttribute(propertyName, PrimaryAttrFullName);
        }

        public static bool IsIdentity(string propertyName)
        {
            return AssertCustomerAttribute(propertyName, IdentityAttrFullName);
        }

        private static bool AssertCustomerAttribute(string propertyName, string targetAttrName)
        {
            Type type = typeof(T);
            object[] attributes = type.GetCustomAttributes(true);
            PropertyInfo[] propertyInfos = type.GetProperties();
            bool result = false;
            foreach (PropertyInfo p in propertyInfos)
            {
                IList<CustomAttributeData> customAttributeDatas = p.GetCustomAttributesData();
                foreach (CustomAttributeData customAttributeData in customAttributeDatas)
                {
                    Type attrType = customAttributeData.AttributeType;
                    if (string.Equals(targetAttrName, attrType.FullName, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(propertyName, p.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return result;
        }
    }
}
