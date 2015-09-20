using System.Web;
using System.Web.Mvc;

namespace Weikeren.Utility.LogWeb
{
    public class FilterConfig
    {
        /// <summary>
        ///    
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}