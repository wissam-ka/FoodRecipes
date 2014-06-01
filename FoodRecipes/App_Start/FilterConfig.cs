using System.Web;
using System.Web.Mvc;
using FoodRecipes.Filters;

namespace FoodRecipes
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogginActionFilter.ExceptionLoggingActionFiter());
            
        }
    }
}