using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeManager.Models
{
    public class AddRecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public List<Ingredient> ingNames {get; set;}
        //public List< PartAmount { get; set; }
        public string measurements{ get; set; }
        //public 

        public AddRecipeViewModel(){
            
            
            }

    }
}