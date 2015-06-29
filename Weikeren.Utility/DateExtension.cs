using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility
{
    /// <summary>
    /// 日期扩展
    /// </summary>
    public static class DateExtension
    {
        #region 日期转字符串
        /// <summary>
        /// 长日期
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <returns></returns>
        public static string ToFullDateTimeString(this DateTime? dateTime)
        {
            return ToDateTimeString(dateTime);
        }

        /// <summary>
        /// 短日期
        /// yyyy-MM-dd
        /// </summary>
        /// <returns></returns>
        public static string ToDateString(this DateTime? dateTime)
        {
            return ToDateTimeString(dateTime, "yyyy-MM-dd");
        }

        /// <summary>
        /// 日期转字符串，默认yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="format">格式</param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime? dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (dateTime == null)
                return string.Empty;

            return dateTime.Value.ToString(format);
        }

        /// <summary>
        /// 一天的结束时间
        /// yyyy-MM-dd 23:59:59
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateEndString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;

            return dateTime.Value.ToString("yyyy-MM-dd 23:59:59");
        }
        /// <summary>
        /// 一天的开始时间
        /// yyyy-MM-dd 00:00:00
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateStartString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;

            return dateTime.Value.ToString("yyyy-MM-dd 00:00:00");
        }
        #endregion
    }
}
