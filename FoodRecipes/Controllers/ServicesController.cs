using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
        //[HttpGet]
        //public ViewResult SortView()
        //{
        //    StaticInfo saticinfo = new StaticInfo(db);
        //    var groupnumberlist = new SelectList(new[] { "6", "12", "24" });
        //    ViewBag.GroupNumberList = groupnumberlist;
        //    var sortOrderlist = new SelectList(new[] { "Rate", "Name", "Date", "CookingTime" });
        //    ViewBag.sortOrderlist = sortOrderlist;
        //    var sortOrder = "FinalRate";
        //    var recipestemp = saticinfo.SortRecipes(sortOrder, db);
        //    var pageNumber = 1;
        //    var pageSize =  6;

        //    return View(recipestemp.ToPagedList(pageNumber, pageSize));
        //}
        //[HttpPost]
        public ViewResult SortView(string sortOrder, int? page, int? pagesize,bool ? cookingTimeActive,string cookingTime)
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
            var recipestemp = saticinfo.SortRecipes(sortOrder, db);
            
            if (cookingTimeActive==true)
            {
                var intcotime = int.Parse(cookingTime);
                //recipestemp = saticinfo.CookingTimeLess(intcotime, recipestemp);
                recipestemp = from item in recipestemp
                              where item.CookingTime <= intcotime
                              select item;
            }

            //if (!pNumberState.IsEmpty())
            //{
            //    var intPnum = int.Parse(peopleNum);
            //    if (pNumberState.Equals("less"))
            //    {
            //        recipestemp = from item in recipestemp
            //            where item.PeopoleNumber <= intPnum
            //            select item;
            //    }
            //    else
            //    {
            //        recipestemp = from item in recipestemp
            //            where item.PeopoleNumber >= intPnum
            //            select item;
            //    }
            //}

            var pageNumber = (page ?? 1);
            var pageSize = (pagesize ?? 6);
           
            ViewBag.currentsize = pageSize;
            ViewBag.currentorder = sortOrder;
            //return HttpUtility.HtmlEncode("pageSize" + pageSize+ ",pageNimber" + pageNumber);
            return  View(recipestemp.ToPagedList(pageNumber, pageSize));
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