﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BusinessInsights
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Redirect home controller
            routes.MapRoute(
                name: "HomeRedirect",
                url: "Home",
                defaults: new { controller = "Facebook", action = "Search" }
             );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Facebook", action = "Search", id = UrlParameter.Optional }
            );
        }
    }
}
