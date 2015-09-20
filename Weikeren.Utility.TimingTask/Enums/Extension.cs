using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Weikeren.Utility.TimingTask.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extension
    {
        #region Enum
        /// <summary>
        /// 将Enum转为显示值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDisplayValue(this Enum value)
        {
            if (value == null)
            {
                return null;
            }
            var map = EnumToAttributeMap(value.GetType());
            return map[value].DisplayValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private static IDictionary<object, ETextAttribute> EnumToAttributeMap(Type enumType)
        {
            IDictionary<object, ETextAttribute> retval = new Dictionary<object, ETextAttribute>();

            foreach (FieldInfo fi in enumType.GetFields())
            {
                if (fi.FieldType.BaseType == typeof(Enum))
                {
                    ETextAttribute[] attrs =
                        (ETextAttribute[])fi.GetCustomAttributes(
                        typeof(ETextAttribute), false);
                    if (attrs.Length > 0)
                    {
                        retval.Add(Enum.Parse(enumType, fi.Name), attrs[0]);
                    }
                }
            }
            return retval;
        } 
        #endregion
    }
}
