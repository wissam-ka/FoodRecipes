using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FoodRecipes.Models
{
    public class Comment
    {
        public long id { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string CommentTitle { get; set; }
        public string UserComment { get; set; }
    }
}