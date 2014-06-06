using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using FoodRecipes.Models;
using FoodRecipes.ProjectClasses;
using PagedList;

namespace FoodRecipes.Controllers
{
    public class ServicesController : Controller
    {
        private RecipeDataContext db = new RecipeDataContext();

        //
        // GET: /Services/

        public ViewResult SortView(string sortOrder, int? page, int? pagesize, string PNumberState,bool ? coTimeActive,string coTime,string Pnum)
        {
            StaticInfo saticinfo = new StaticInfo(db);
            var groupnumberlist = new SelectList(new[] { "6", "12", "24" });
            ViewBag.GroupNumberList = groupnumberlist;
            var sortOrderlist = new SelectList(new[] {  "Rate","Name", "Date", "CookingTime" });
            ViewBag.sortOrderlist = sortOrderlist;
            
          
            if (sortOrder.IsEmpty())
            {
                sortOrder = "FinalRate";
            }
           
            var recipestemp = from s in db.Recipes
                           select s;

            switch (sortOrder)
            {
                case "Name":
                    recipestemp = recipestemp.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    recipestemp = recipestemp.OrderBy(s => s.TimeStamp);
                    break;
                case "CookingTime":
                    recipestemp = recipestemp.OrderByDescending(s => s.CookingTime);
                    break;
                default:  // Name ascending 
                    recipestemp = recipestemp.OrderBy(s => s.FinalRate);
                    break;
            }
            
            if (coTimeActive==true)
            {
                var intcotime = int.Parse(coTime);
                recipestemp = from item in recipestemp
                              where item.CookingTime <= intcotime
                    select item;
            }

            if (!PNumberState.IsEmpty())
            {
                var intPnum = int.Parse(Pnum);
                if (PNumberState.Equals("less"))
                {
                    recipestemp = from item in recipestemp
                        where item.PeopoleNumber <= intPnum
                        select item;
                }
                else
                {
                    recipestemp = from item in recipestemp
                        where item.PeopoleNumber >= intPnum
                        select item;
                }
            }



            var pageNumber = (page ?? 1);
            var pageSize = (pagesize ?? 6);
           
            ViewBag.currentsize = pageSize;
            ViewBag.currentorder = sortOrder;
            return View(recipestemp.ToPagedList(pageNumber, pageSize));
        }

        





        // GET: /Services/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        //
        // POST: /Services/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("SortView");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}