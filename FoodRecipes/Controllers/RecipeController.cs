using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using FoodRecipes.Models;


namespace FoodRecipes.Controllers
{
    public class RecipeController : Controller
    {
        //
        // GET: /Recipe/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Recipe tempRecipe, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && file.ContentType.StartsWith("image/"))
            {
                try
                {

                    string temppath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images/siteimg"), temppath);
                    int i;
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                    tempRecipe.ImgPath = Path.Combine("~/Images/siteimg", temppath);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                ModelState.AddModelError("file", "You have not specified a file // wrong file type");
               
            }


            tempRecipe.FinalRate = 0;
            tempRecipe.RatingPeople = 0;
            if (ModelState.IsValid)
            {
                var db = new RecipeDataContext();

                db.Recipes.Add(tempRecipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tempRecipe);
        }
        [HttpGet]
        public ActionResult Search()
        {
            var searchlist = new SelectList(new[] { "Title","Food Rate", "people number" });
            ViewBag.SearchList = searchlist;
            var db = new RecipeDataContext();
            IQueryable<Recipe> recipelist = db.Recipes;
            if (db.Recipes.Count() != 0)
            {
                
                var recipelist1 = db.Recipes;
                
                if (db.Recipes.Count()<=9)
                {
                     recipelist = recipelist1;

                }
                else
                {
                    recipelist = recipelist1.Where(a => a.Id < db.Recipes.Count());
                 
                }
            }

            return View(recipelist);
        }
        public ActionResult Search(string category, string searchPattern)
        {
            var searchlist = new SelectList(new[] { "Title", "Food Rate", "people number" });
            ViewBag.SearchList = searchlist;
            if (searchPattern.IsEmpty())
            {
                ModelState.AddModelError("searchPattern", "Shouldn't be empty");
            }
            if (ModelState.IsValid)
            {
                var db = new RecipeDataContext();
                IQueryable<Recipe> results = db.Recipes;
                
                if (category == "Food Rate")
                {
                    results = results.Where(a => a.FinalRate >= double.Parse(searchPattern));
                }
                else  if(category=="People Number")
                {
                    int i = int.Parse(searchPattern);
                    results = results.Where(recipe => recipe.PeopoleNumber == i);
                }
                else
                {
                    results = results.Where(recipe => recipe.Title.Contains(searchPattern));
                }
            
                return View("Search", results);

            }
            return View();
        }

        public ActionResult RecipeDetails(long? id)
        {
            var db = new RecipeDataContext();
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }


            
            var resp = db.Recipes.Find(id);
            if (resp == null)
            {
                return View(db.Recipes.FirstOrDefault());
                //ViewData["Resipe"] = resp;
            }
            return View(resp);
            
        }

        [HttpPost]
        public ActionResult Rate(Rate rate)
        {

            var db = new RecipeDataContext();
            var i = rate.ReId;
            var tempRecipe = db.Recipes.Find(rate.ReId);
            if (rate.RateValue > 10 || rate.RateValue < 0 )
            {
                ModelState.AddModelError("comm", "out of range");
            }
            else if (tempRecipe == null)
            {
                ModelState.AddModelError("ReId", "not found");
            }
            else
            {
                tempRecipe.Rates.Add(rate);
                tempRecipe.RatingPeople = tempRecipe.RatingPeople + 1;
                tempRecipe.FinalRate = (double)Math.Round(((tempRecipe.FinalRate * (tempRecipe.RatingPeople - 1) + rate.RateValue) / (tempRecipe.RatingPeople)), 2);
               
                db.SaveChanges();
            }
            if (!Request.IsAjaxRequest())
                return RedirectToAction("RecipeDetails", new { id = rate.ReId });
         
            return Json(new
            {
                FRate1 = tempRecipe.FinalRate,
                NNum = tempRecipe.RatingPeople

            });
        }
    }
}
