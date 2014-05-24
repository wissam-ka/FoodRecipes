using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace FoodRecipes.Models
{
    public class RecipeDataContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        static RecipeDataContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<RecipeDataContext>());
            //   Database.SetInitializer(new DropCreateDatabaseAlways<RecipeDataContext>());
           
        }
    }
}