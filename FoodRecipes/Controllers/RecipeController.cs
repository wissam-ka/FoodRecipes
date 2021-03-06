﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using DotNetOpenAuth.Messaging;
using FoodRecipes.Filters;
using FoodRecipes.Models;
using log4net;
using System.Diagnostics;
using FoodRecipes.ProjectClasses;
using WebMatrix.WebData;


namespace FoodRecipes.Controllers
{
    
    [AllowAnonymous]
     [LogginActionFilter.LoggingActionFilter]
    public class RecipeController : Controller
    {
        //
        // GET: /Recipe/
        // private static readonly ILog _log = log4net.LogManager.GetLogger(typeof(Controller));
        public ActionResult Index()
        {
            
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
         [Authorize]
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
            var context = new UsersContext();
            var username = User.Identity.Name;
            var user = context.UserProfiles.SingleOrDefault(u => u.UserName == username);

             tempRecipe.UserId =user.UserId ;
            tempRecipe.TimeStamp = DateTime.Now;
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
            var initgroupsize = 6;

            var groupnumberlist = new SelectList(new[] { "6","12", "24" });
            ViewBag.GroupNumberList = groupnumberlist;
            
            var searchlist = new SelectList(new[] { "Title","Food Rate", "people number" });
            ViewBag.SearchList = searchlist;
            var db = new RecipeDataContext();
            SearchCal searchCal=new SearchCal(db);
             var groupnumber=searchCal.SearchGropNumber(initgroupsize);
          //  IQueryable<Recipe> recipelist = db.Recipes;
            var recipelist1 = new List<Recipe>();
            if (db.Recipes.Count() != 0)
            {
   
                if (db.Recipes.Count()<=initgroupsize)
                {
                     recipelist1 = db.Recipes.ToList();
                }
                else
                {
                  //  var idtemp = searchCal.IdStartPoint(groupnumber,initgroupsize);
             
                   //recipelist = recipelist1.Where(a => a.Id > (idtemp));
                  recipelist1 = searchCal.RecipesResultList(1, initgroupsize);

                }
                
            }
            ViewData["resultlist"] = recipelist1;
            ViewData["groupnumber"] = groupnumber;
            return View();
        } 
        public ActionResult Search(string category, string searchPattern)
        {
            var groupnumberlist = new SelectList(new[] { "6", "12", "24" });
            ViewBag.GroupNumberList = groupnumberlist;
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
            
                //return View("Search", results);
                ViewData["resultlist"] = results;

            }
            return View();
        }

        public ActionResult RecipeDetails(long? id)
        {
          
            Debug.WriteLine(WebSecurity.CurrentUserId);
            var db = new RecipeDataContext();
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }


            
            var recipe = db.Recipes.Find(id);
           
                
            
            if (recipe == null)
            {
                return View(db.Recipes.FirstOrDefault());
              
            }
               var context = new UsersContext();
            var user = context.UserProfiles.SingleOrDefault(u => u.UserId==recipe.UserId);
            ViewBag.userName = user.NickName;
            return View(recipe);
            
        }
         [CustomAuthorizeAttribute]
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            var db = new RecipeDataContext();
            var tempRecipe = db.Recipes.Find(id);
           // var tempRecipe = db.Recipes.FirstOrDefault();
            if (tempRecipe == null)
            {
                return RedirectToAction("Index");
               
            }
             var context = new UsersContext();
            var username = User.Identity.Name;
            var user = context.UserProfiles.SingleOrDefault(u => u.UserName == username);


            if (user.UserId == tempRecipe.UserId)
            {
                return RedirectToAction("RecipeDetails", new {id = id});
            }

           return View(tempRecipe);
        }
        [CustomAuthorizeAttribute]
        [HttpPost]
        public ActionResult Edit(Models.Recipe tempRecipe, HttpPostedFileBase file)
        {
            var db = new RecipeDataContext();
            var tempRecipeold = db.Recipes.Find(tempRecipe.Id);
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
            
           
            tempRecipeold.TimeStamp = DateTime.Now;
            for (int i=0;i<tempRecipe.Ingredients.Count;i++  )
            {
                tempRecipeold.Ingredients[i].Name = tempRecipeold.Ingredients[i].Name;
                tempRecipeold.Ingredients[i].Amount = tempRecipeold.Ingredients[i].Amount;
                tempRecipeold.Ingredients[i].Unit = tempRecipeold.Ingredients[i].Unit;
            }
            tempRecipeold.Ingredients = tempRecipe.Ingredients;
            
            tempRecipe.RatingPeople = tempRecipeold.RatingPeople;
            if (ModelState.IsValid)
            {


               
                db.SaveChanges();
               
            }
            return RedirectToAction("RecipeDetails", new {id=tempRecipeold.Id});
        }

        [HttpPost]
        public ActionResult Rate(Rate rate)
        {

            var db = new RecipeDataContext();
            var i = rate.ReId;
            var tempRecipe = db.Recipes.Find(rate.ReId);
            if (rate.RateValue > 10 || rate.RateValue < 0)
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
                tempRecipe.FinalRate =(double)Math.Round(((tempRecipe.FinalRate*(tempRecipe.RatingPeople - 1) + rate.RateValue)/(tempRecipe.RatingPeople)), 2);

                db.SaveChanges();
            }
            if (!Request.IsAjaxRequest())
                return RedirectToAction("RecipeDetails", new {id = rate.ReId});


            return Json(new
            {
                FRate1 = tempRecipe.FinalRate,
                NNum = tempRecipe.RatingPeople

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculatePeopleNumber(double newPeopleNumber, int newId,string viewtime)
        {
            var tempbool = false;
            var db = new RecipeDataContext();
            var tempRecipe = db.Recipes.Find(newId);
            List<double> newAmount =new List<double>();
             if (tempRecipe == null)
            {
                ModelState.AddModelError("newId", "not found");
            }
            else
             {

                 var correctionRatio = newPeopleNumber/(double)tempRecipe.PeopoleNumber;
                 if (tempRecipe.TimeStamp.ToString().Contains(viewtime))
                 {
                       tempbool = true;
                 }
                 else
                 {
                    tempbool = false;  
                 }

                 foreach (var item in tempRecipe.Ingredients)
                {
                    if (tempbool)
                    {
                        newAmount.Add((double) Math.Round(double.Parse(item.Amount)*correctionRatio, 2));
                    }
                    else
                    {
                        item.Amount =""+ Math.Round(double.Parse(item.Amount)*correctionRatio, 2);
                    }
                
                }
 
            }
             if (!Request.IsAjaxRequest())
                 return RedirectToAction("RecipeDetails", new { id = newId });
            if (!tempbool)
            { var Pview=PartialView("_RecipePart", tempRecipe);
                return Pview;
            }
            else
            {


                return Json(new
                {
                    newamountlist = newAmount,
                    //  tempRecipe,
                }, JsonRequestBehavior.AllowGet);
                
            }
        }
        [HttpPost]
        public ActionResult AddComment(Comment comment, long newcommentId)
        {
            var db = new RecipeDataContext();
            
            var tempRecipe = db.Recipes.Find(newcommentId);
            if (tempRecipe == null)
            {
                ModelState.AddModelError("newcommentId", "not found");
            }
            else
            {
                tempRecipe.Comments.Add(comment);
                db.SaveChanges();
            }
            return PartialView("CommentPart",comment);
               
                
        }

        [HttpPost]
        public ActionResult UploadnewPage(string groupnumber, int groupsize)
        {
              var db = new RecipeDataContext();
            SearchCal searchCal=new SearchCal(db);
         
            var idtemp = searchCal.IdStartPoint(int.Parse(groupnumber), groupsize);
            var recipelist1 = searchCal.RecipesResultList(idtemp, groupsize);

            return  PartialView("_SearchView",recipelist1);
        }
        
    }
    
}