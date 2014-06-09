using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections;
using FoodRecipes.Filters;

namespace FoodRecipes.Models
{
    public class Recipe
    {
         [Required] 
        public long Id { get; set; }
        [Required] 
        public string Title { get; set; }
        public DateTime TimeStamp {get; set; }
        public int CookingTime { get; set; }
        [DisplayName("Number of people")]
        public int PeopoleNumber { get; set; }
        public string ImgPath { get; set; }
        public string FoodPreparation { get; set; }
        public virtual List<Rate> Rates { get; private set; }
        public virtual List<Ingredient> Ingredients { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public double FinalRate { get; set; }
        public int RatingPeople { get; set; }
        public int UserId { get; set; }
        public Recipe()
        {
             Rates = new List<Rate>();
            Comments=new List<Comment>();
            Ingredients=new List<Ingredient>();

        }
        
    }
}