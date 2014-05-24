using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodRecipes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                
                name: "RecipeDetails",
                url: "{controller}/{id}",
                defaults: new { controller = "Recipe", action = "RecipeDetails" },
                constraints: new { id = @"^\d+$" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Recipe", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}