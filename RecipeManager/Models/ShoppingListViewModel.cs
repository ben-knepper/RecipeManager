using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeManager.Models
{
    public class ShoppingListViewModel
    {
        public List<Ingredient> ings { get; set; }

        public ShoppingListViewModel()
        {

            ings = RecipeDb.SelectUserShoppingList();

        }
    }

   
   

}