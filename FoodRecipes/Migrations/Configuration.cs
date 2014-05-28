using FoodRecipes.Models;

namespace FoodRecipes.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodRecipes.Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FoodRecipes.Models.UsersContext context)
        {
            //RecipeDataContext
            //Date = {5/28/2014 12:00:00 AM}
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
         //   Ingredient temp = new Ingredient{ Id = 0, Name = " Zucker braun", Amount = "1", Unit = "kg" };
           //   context.Recipes.AddOrUpdate(
           //      p => p.Title,
             //    new Recipe { Id = 0, Title = "Spareribs NT im Bratschlauch", CookingTime = 60, PeopoleNumber = 4, ImgPath = "~/Images/siteimg/3d54d213-3670-48c2-bf19-b31770c9f88b.jpg", FoodPreparation = "Arbeitszeit: ca. 1 Std. Ruhezeit: ca. 1 Tag / Schwierigkeitsgrad: simpel / Kalorien p. P.: keine AngabeAlle Zutaten für die Gewürzmischung miteinander vermengen, die in Stücke geteilten Rippchen kräftig damit einmassieren und über Nacht im Kühlschrank durchziehen lassen. Alle Zutaten für die Soße vermengen und ebenfalls im Kühlschrank ziehen lassen. tschlauch, die haben auch gerade gut auf einem Backblech Platz. ",Ingredients={temp},FinalRate = 0,RatingPeople = 0}
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
             //  );
            //
        }
    }
}
