using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeManager.Models
{
    public class AddRecipeViewModel
    {
        Recipe recipe { get; set; }
        List<Ingredient> ingNames {get; set;}
        int PartAmount { get; set; }
        string measurename { get; set; }
        string partText { get; set; }

        public AddRecipeViewModel(){
            
            
            }

    }
}