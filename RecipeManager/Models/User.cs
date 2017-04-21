using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections.Generic;

namespace RecipeManager.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PassHash { get; set; }
        public string Salt { get; set; }
        
        public static class UserDb
        {
            public static List<User> SelectByAllUsers()
            {

                List<User> output = new List<User>();
                MySqlConnection connection = MySqlProvider.Connection;
                MySqlCommand command = connection.CreateCommand();
                //command.CommandType = CommandType.Text;
                command.CommandText = "SELECT UserId,Username FROM Users order by UserId"; //"SELECT RecipeName FROM RecipeLists JOIN Recipes on RecipeLists.RecipeId = Recipes.RecipeId";

                MySqlDataReader Reader = null;
                try
                {
                    //connection.Open();
                    Reader = command.ExecuteReader();
                    if (Reader.Read())
                    {
                        do
                        {
                            var user = new User()
                            {
                                UserId = Convert.ToInt32(Reader["UserId"]),
                                Username = Convert.ToString(Reader["Username"]),
                                // PassHash = Reader["PassHash"].ToString()


                            };
                            output.Add(user);
                        } while (Reader.Read());
                    }

                }
                catch (MySqlException ex)
                {
                    output.Add(new User() { Username = ex.Message });
                }
                finally
                {
                    Reader?.Close();
                }

                return output;
            }


        }  

    }
}