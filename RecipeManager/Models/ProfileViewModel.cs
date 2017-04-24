using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeManager.Models
{
    public class ProfileViewModel
    {
        public User user { get; set; }
        public List<Recipe> Recipes { get; set; }
        public int RecipeContributions { get; set;}

        public ProfileViewModel(int UserId, string UserName)
        {
            user = UserDb.GetUserInfo(UserId);
            // user.UserId = UserId;
            // user.Username = UserName;
            // Recipes = RecipeDb.SelectUserRecipesById(UserId,UserName);
            Recipes = RecipeDb.SelectAllUserMadeRecipes();
            RecipeContributions = RecipeDb.GetContributions(UserName);
        }
    }
}