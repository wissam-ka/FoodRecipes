using System.Collections.Generic;
using System.IO;
using FoodRecipes.Models;

namespace FoodRecipes.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodRecipes.Models.RecipeDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FoodRecipes.Models.RecipeDataContext context)
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
            var Rates1 = new List<Rate>();
            var Comments1 = new List<Comment>();
            var Ingredients1 = new List<Ingredient>
            {
                new Ingredient{Name = "Kartoffel(n), festkochende",Amount ="1",Unit = "kg"},
                new Ingredient{Name = "salt",Amount ="1",Unit = "EL"},
                new Ingredient{Name = "Wasser",Amount ="750",Unit = "ml"},

            };
            var Ingredients2 = new List<Ingredient>
            {
                new Ingredient{Name = "Hackfleisch (Faschiertes), gemischt",Amount ="600",Unit = "g"},
                new Ingredient{Name = "Semmel(n), eingeweicht",Amount ="2",Unit = ""},
                new Ingredient{Name = "Ei(er)",Amount ="1",Unit = ""},
                new Ingredient{Name ="Petersilie, fein gehackt",Amount ="2",Unit = "EL"},
                 new Ingredient{Name = "Salz, fein gehackt",Amount ="1",Unit = ""},
                new Ingredient{Name ="Pfeffer, fein gehackt",Amount ="1",Unit = "EL"},

            };
            var Ingredients3 = new List<Ingredient>
            {
                new Ingredient{Name = "Paprikaschote(n), rot",Amount ="3",Unit = ""},
                new Ingredient{Name = "Chilischote(n), getrocknete",Amount ="2",Unit = "EL"},
                new Ingredient{Name = "Knoblauchzehe(n)",Amount ="3",Unit = ""},
                new Ingredient{Name = "Pinienkerne",Amount ="50",Unit = "g"},
                new Ingredient{Name = "Olivenöl",Amount ="5",Unit = "EL"},

            };
            var Ingredients4 = new List<Ingredient>
            {
                new Ingredient{Name = "Putenunterkeule(n)",Amount ="2",Unit = "p"},
                new Ingredient{Name = "salt",Amount ="1",Unit = "EL"},
                new Ingredient{Name = "Wasser",Amount ="750",Unit = "ml"},
                 new Ingredient{Name = "Suppengrün Möhren, (Sellerie, Lauch etc.)",Amount ="1",Unit = "Handvoll"},
                new Ingredient{Name = "Paprikapulver, süß",Amount ="1",Unit = "EL"},
                new Ingredient{Name = "Öl",Amount ="6",Unit = "EL"},
            };
            var Ingredients5 = new List<Ingredient>
            {
                new Ingredient{Name = "Fleischtomate(n)",Amount ="1",Unit = "kg"},
                new Ingredient{Name = "salt",Amount ="1",Unit = "EL"},
                new Ingredient{Name = "Lasagneplatte(n)",Amount ="300",Unit = "g"},
                 new Ingredient{Name = "Mozzarella",Amount ="300",Unit = "g"},
                new Ingredient{Name = "Paprikapulver, süß",Amount ="1",Unit = "EL"},
                new Ingredient{Name = "Parmesan, frisch gerieben",Amount ="100",Unit = "g"},
            };
            var Ingredients6 = new List<Ingredient>
            {
                new Ingredient{Name = "Quark",Amount ="250",Unit = "g"},
                new Ingredient{Name = "Sirup, (Karamellsirup nach meinem Rezept)",Amount ="100",Unit = "ml"},
                new Ingredient{Name = "Sahne",Amount ="250",Unit =" ml"},
                 new Ingredient{Name = "Puderzucker, ( ca. 75 g )",Amount ="9",Unit = "TL, gehäuft"},  
            };
            var Recipes = new List<Recipe>
            {
                new Recipe{Id =2,TimeStamp =DateTime.Now,Title = "Großmutter's Rahmkartoffeln",CookingTime = 60,FinalRate = 0,Ingredients =Ingredients1,Comments = Comments1,PeopoleNumber = 4,RatingPeople = 0,FoodPreparation = "Arbeitszeit: ca. 20 Min. / Schwierigkeitsgrad: simpel / Kalorien p. P.: keine Angabe Kartoffeln schälen und in knapp 1/2 cm dicke Scheiben schneiden, im Salzwasser einmal aufkochen und 5 Min. ziehen lassen! Abgießen und warm stellen. Die Butter zerlassen, das Mehl darüber stäuben und unter Rühren eine helle Mehlschwitze herstellen. Ist das Mehl etwas angeröstet, erst mit der heißen Fleischbrühe, dann mit der Milch unter ständigem Rühren ablöschen und mit Salz abschmecken. Aufkochen lassen, dabei rühren, etwas einkochen und dann die Kartoffelscheiben hinein geben. Leise köcheln, bis die Kartoffeln gar sind.",ImgPath =Path.Combine("~/Images/siteimg/main1.jpg","")},
                new Recipe{Id =6,TimeStamp =DateTime.Now,Title = "Faschierte Laibchen",CookingTime = 50,FinalRate = 0,Ingredients =Ingredients2,Comments = Comments1,PeopoleNumber = 4,RatingPeople = 0,FoodPreparation = "Zwiebel in Fett goldgelb anlaufen lassen, Petersilie dazugeben, anlaufen lassen, Semmeln gut ausdrücken, darunter mischen. Fleisch mit sämtlichen Zutaten sehr gut durcharbeiten. Kleine Laibchen formen, in Brösel wälzen und in Butter braten.",ImgPath =Path.Combine("~/Images/siteimg/seed3.jpg","")},
                 new Recipe{Id =7,TimeStamp =DateTime.Now,Title = "Gnocchisalat",CookingTime = 50,FinalRate = 0,Ingredients =Ingredients3,Comments = Comments1,PeopoleNumber = 4,RatingPeople = 0,FoodPreparation = "Den Ofen auf 250 Grad vorheizen. Die Paprikaschoten waschen, halbieren, Kerne und Trennwände heraus zupfen. Die Schoten mit der Haut nach oben auf ein Backblech legen und für ca. 20 Minuten in den Ofen schieben, bis die Haut dunkle Blasen hat. Aus dem Ofen holen und abkühlen lassen und die Haut abziehen. Eine Schote klein würfeln und beiseite legen.",ImgPath =Path.Combine("~/Images/siteimg/seed4.jpg","")},
                 new Recipe{Id =3,TimeStamp =DateTime.Now,Title = "Putenunterkeulen",CookingTime = 40,FinalRate = 0,Ingredients =Ingredients4,Comments = Comments1,PeopoleNumber = 4,RatingPeople = 0,FoodPreparation = "Den Ofen auf 250 Grad vorheizen. Die Paprikaschoten waschen, halbieren, Kerne und Trennwände heraus zupfen. Die Schoten mit der Haut nach oben auf ein Backblech legen und für ca. 20 Minuten in den Ofen schieben, bis die Haut dunkle Blasen hat. Aus dem Ofen holen und abkühlen lassen und die Haut abziehen. Eine Schote klein würfeln und beiseite legen.",ImgPath =Path.Combine("~/Images/siteimg/seed5.jpg","")},
                   new Recipe{Id =4,TimeStamp =DateTime.Now,Title = "Tomatenlasagne",CookingTime = 66,FinalRate = 0,Ingredients =Ingredients5,Comments = Comments1,PeopoleNumber =3,RatingPeople = 0,FoodPreparation = "Tomaten in ca. 1 cm dicke Scheiben schneiden (wer mag, vorher kurz überbrühen und häuten).Butter zerlassen, Mehl darin leicht anrösten, unter Rühren Milch und Wein dazugeben. Sauce ca. 3 min köcheln lassen, den Parmesan einrühren und mit Salz, Pfeffer, Muskat abschmecken.",ImgPath =Path.Combine("~/Images/siteimg/seed6.jpg","")},
                   new Recipe{Id =5,TimeStamp =DateTime.Now,Title = "Andis Karamell-Quark",CookingTime = 6,FinalRate = 0,Ingredients =Ingredients6,Comments = Comments1,PeopoleNumber =5,RatingPeople = 0,FoodPreparation = "Arbeitszeit: ca. 5 Min. / Schwierigkeitsgrad: simpel / Kalorien p. P.: keine AngabeDie Sahne mit dem Puderzucker steif schlagen.Quark mit Karamellsirup cremig rühren, die geschlagene Sahne unterheben.In Dessertgläser füllen und kühl stellen.",ImgPath =Path.Combine("~/Images/siteimg/seed7.jpg","")},
      
            };
            Recipes.ForEach(s => context.Recipes.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

        }
    }
}
