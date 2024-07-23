using System.Web.Mvc;

namespace App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AllowCorsAttribute()); // Add this line to register the AllowCorsAttribute
        }
    }
}