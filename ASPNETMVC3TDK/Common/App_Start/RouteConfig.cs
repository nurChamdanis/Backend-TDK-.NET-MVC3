﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Toyota.Common.App_Start
{
    public class RouteConfig

    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "PersonalInformation", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}