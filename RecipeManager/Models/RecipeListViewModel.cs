using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeManager.Models
{
    public class RecipeListViewModel
    {
        public List<Recipe> Recipes { get; set; }
        public RecipeListViewModel()
        {
             Recipes = RecipeDb.SelectUserRecipes();
        }
    }
    public class AllRecipesViewModel
    {
        public List<Recipe> Recipes { get; set; }
        public AllRecipesViewModel()
        {
           Recipes = RecipeDb.SelectAllRecipes();
        }
    }
    public class SearchViewModel
    {
        public List<Recipe> Recipes { get; set; }
        public string SearchTerm { get; set; }
        public SearchViewModel(string searchTerm)
        {
            SearchTerm = searchTerm;
            Recipes = RecipeDb.SearchRecipes(searchTerm);
        }
    }
}