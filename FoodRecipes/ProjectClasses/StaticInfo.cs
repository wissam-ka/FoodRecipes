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

        public List<Recipe> CookingTimeLess(int x,RecipeDataContext dbContextTemp)
        {
            var cookinglist = (from item in dbContextTemp.Recipes
                where item.CookingTime <= x
                select item).ToList();

            return cookinglist;
         }

        public List<Recipe> peopleNumberBoL(int x, bool incOdec, RecipeDataContext dbContextTemp)
        {
           
            if (incOdec)
            {
               var data = (from item in dbContextTemp.Recipes
            
                           where item.PeopoleNumber <= x
                    select item).ToList();
                return data;
            }
            else
            {
                var data = (from item in dbContextTemp.Recipes
                            where item.PeopoleNumber >= x
                            select item).ToList();
                return data; 
            }
        }

    }
}