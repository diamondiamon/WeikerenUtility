using System.Web;
using System.Web.Mvc;

namespace Weikeren.Utility.WebTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}