using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipeManager.Models;
using static RecipeManager.Models.User;

namespace RecipeManager.Models
{
    public class AllUsersViewModel
    {
        public List<User> Users { get; set; }
        public AllUsersViewModel()
        {
            Users = UserDb.SelectByAllUsers();
        }
    }
}