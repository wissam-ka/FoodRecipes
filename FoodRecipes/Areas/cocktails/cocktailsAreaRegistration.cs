using System.Web.Mvc;

namespace FoodRecipes.Areas.cocktails
{
    public class cocktailsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "cocktails";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "cocktails_default",
                "cocktails/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Foodrecipes.Areas.cocktails.Controllers" }

            );
        }
    }
}
