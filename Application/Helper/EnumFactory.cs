using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public static class EnumFactory
    {
        public static string GetDescriptions(Type type, string fieldName)
        {
            var desc = String.Empty;

            var field = type.GetField(fieldName);
            var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
            foreach (DescriptionAttribute fd in fds)
            {
                desc = fd.Description;
            }
            return desc;
        }

        public static string GetEnumDescriptionByValue(this System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public static int GetEnumFromDescription(string description, Type enumType)
        {
            foreach (var field in enumType.GetFields())
            {
                DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute == null)
                    continue;
                if (attribute.Description == description)
                {
                    return (int)field.GetValue(null);
                }
            }
            return 0;
        }
    }
}