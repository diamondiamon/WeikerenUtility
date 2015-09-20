using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.Payment
{
    public static class Extensions
    {
        public static string QueryString(this HttpRequestBase request,string param)
        {
            try
            {
                string value = request.QueryString[param];
                return value;
            }
            catch (Exception ex)
            {
                //参数不正确
                //Do it
                return string.Empty;
            }
        }

        public static int? QueryInt(this HttpRequestBase request, string param)
        {
            try
            {
                string value = request.QueryString[param];
                return int.Parse(value);
            }
            catch (Exception ex)
            {
                //参数不正确
                //Do it
                return null;
            }
        }

        public static decimal? QueryDecimal(this HttpRequestBase request, string param)
        {
            try
            {
                string value = request.QueryString[param];
                return decimal.Parse(value);
            }
            catch (Exception ex)
            {
                //参数不正确
                //Do it
                return null;
            }
        }

        public static DateTime? QueryDateTime(this HttpRequestBase request, string param)
        {
            try
            {
                string value = request.QueryString[param];
                return DateTime.Parse(value);
            }
            catch (Exception ex)
            {
                //参数不正确
                //Do it
                return null;
            }
        }
    }
}