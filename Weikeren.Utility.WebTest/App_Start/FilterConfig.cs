using System.Web;
using System.Web.Mvc;
using Weikeren.Utility.WebTest.Filters;

namespace Weikeren.Utility.WebTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionFilter());
        }
    }
}