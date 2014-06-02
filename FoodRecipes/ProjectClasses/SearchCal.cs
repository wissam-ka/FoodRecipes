using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using FoodRecipes.Models;

namespace FoodRecipes.ProjectClasses
{
    public class SearchCal
    {
        private RecipeDataContext dbContext;


        public SearchCal(RecipeDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int SearchGropNumber(int groupsize)
        {
            int count = dbContext.Recipes.Count();
            
            if(count%groupsize==0)
            {
               
                return count/groupsize;
            }
            else
            {
                return ((count / groupsize)+1);
                
            }
          
        }

        public long IdStartPoint(int groupnumber, int groupsize)
        {
            return (dbContext.Recipes.Count() - ((long)groupnumber - 1) * (long)groupsize + 1);
        }
        public List<Recipe> RecipesResultList (int startid,int groupsize)
         {
             List<Recipe> templist=new List<Recipe>();
            templist = dbContext.Recipes.ToList().GetRange(startid, groupsize);
            return templist;

         }
    }
}