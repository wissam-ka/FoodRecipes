using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using FoodRecipes.Models;

namespace FoodRecipes.ProjectClasses
{
    public class StaticInfo
    {
         private RecipeDataContext dbContext;


        public StaticInfo(RecipeDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public long HighestRate()
        {
            return dbContext.Recipes.OrderBy(s => s.FinalRate).First().Id;
        }

        public long HeighestCommentsNumber()
        {
            return dbContext.Recipes.OrderBy(s => s.Comments.Count).First().Id;
        }
        public List<Recipe> HeighestNCommentsNumber(int x, RecipeDataContext dbContextTemp)
        {
            return dbContextTemp.Recipes.OrderBy(s => s.Comments.Count).Take(x).ToList();
        }

        public IQueryable<Recipe> CookingTimeLess(int x,RecipeDataContext dbContextTemp)
        {
            var cookinglist = (from item in dbContextTemp.Recipes
                where item.CookingTime <= x
                select item);

            return cookinglist;
         }

        public IQueryable<Recipe> PeopleNumberBoL(int x, bool incOdec, RecipeDataContext dbContextTemp)
        {
           
            if (incOdec)
            {
               var data = (from item in dbContextTemp.Recipes
            
                           where item.PeopoleNumber <= x
                    select item);
                return data;
            }
            else
            {
                var data = (from item in dbContextTemp.Recipes
                            where item.PeopoleNumber >= x
                            select item);
                return data; 
            }
        }

        public IQueryable<Recipe> SortRecipes(string sortOrder, RecipeDataContext dbContextTemp)
        {
            var recipestemp = from s in dbContextTemp.Recipes
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
            return recipestemp;
        }

    }
}