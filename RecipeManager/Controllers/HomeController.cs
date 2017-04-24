using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool? LogOut = false)
        {
            if (LogOut.HasValue && LogOut.Value)
            {
                var connection = MySqlProvider.Connection;
                var command = connection.CreateCommand();
                command.CommandText = "CALL LogoutUser()";

                try
                {
                    command.ExecuteNonQuery();
                }
                catch { }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}