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

        public int IdStartPoint(int groupnumber, int groupsize)
        {
            return groupnumber * groupsize +1;
        }
        public List<Recipe> RecipesResultList (int startid,int groupsize)
         {
             List<Recipe> templist=new List<Recipe>();
            if (startid + groupsize > (dbContext.Recipes.Count()+1))
            {
                groupsize = dbContext.Recipes.Count() - startid+1;
            }
            templist = dbContext.Recipes.ToList().GetRange(startid-1, groupsize);
            return templist;

         }
    }
}