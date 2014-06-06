using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodRecipes.Areas.cocktails.Controllers
{


    [Authorize(Roles = "Administrator")]
    public class CocktailController : Controller
    {
        //
        // GET: /cocktails/Cocktail/
       
        public ActionResult Index()
        {
            return View();
        }

    }
}
