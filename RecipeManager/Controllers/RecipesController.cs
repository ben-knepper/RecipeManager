using RecipeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeManager.Controllers
{
    public class RecipesController : Controller
    {
        // GET: Recipes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Recipe (int RecipeId)
        {
            Recipe model = RecipeDb.SelectRecipe(RecipeId);

            return View(model);
        }

        public ActionResult Search(string SearchTerm)
        {
            SearchViewModel model = new SearchViewModel(SearchTerm);
            return View(model);
        }
        public ActionResult AddRecipe()
        {
            AddRecipeViewModel model = new AddRecipeViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddRecipe(AddRecipeViewModel model)

        {

            if (ModelState.IsValid)
            {
                RecipeDb.Insert(model);
                return RedirectToAction("AllRecipes");
            }

            return View();
        }

        public ActionResult AllRecipes()
        {
            AllRecipesViewModel model =new AllRecipesViewModel();
            return View(model);
        }
        public ActionResult Viewerprofile()
        {
            return View();
        }
        public ActionResult ShoppingList()
        {
            return View();
        }
        public ActionResult UserRecipes()
        {
            RecipeListViewModel model;
            model = new RecipeListViewModel();
            return View(model);
        }

        public ActionResult ViewUsers()
        {
            AllUsersViewModel users = new AllUsersViewModel();
            return View(users);

        }
        public ActionResult AccountInfo()
        {
            return View();
        }
        public ActionResult AddToMyRecipes(int RecipeId)

        {
            
            return RedirectToAction("UserRecipes");
        }
    }
}