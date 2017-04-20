using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using System;

namespace RecipeManager.Models
{
    public class ViewAllRecipes
    {
       
        public static List<Recipe> SelectAllRecipes()
        {
            List<Recipe> output = new List<Recipe>();
            MySqlConnection connection = MySqlProvider.Connection;

            MySqlCommand recipeListCommand = connection.CreateCommand();
            recipeListCommand.CommandText = "SELECT * FROM Recipes";

            try
            {

                //connection.Open();
                MySqlDataReader Reader = recipeListCommand.ExecuteReader();
                if (Reader.Read())
                {
                    do
                    {
                        var recipe = new Recipe()
                        {
                            RecipeId = Convert.ToInt32(Reader["RecipeId"]),
                            RecipeName = Convert.ToString(Reader["RecipeName"]),
                            Instructions = Convert.ToString(Reader["Instructions"]),
                            Image = new Uri(Convert.ToString(Reader["Image"])),
                            Servings = Convert.ToInt16(Reader["Servings"]),
                            SourceName = Convert.ToString(Reader["SourceName"]),
                            MinutesToMake = Convert.ToInt16(Reader["MinutesToMake"])

                        };
                        Console.WriteLine(recipe.ToString());
                        output.Add(recipe);
                    } while (Reader.Read());


                }
            }
            catch (MySqlException ex)
            {
                output.Add(new Recipe() { RecipeName = ex.Message });
            }
            //finally
            //{
            //    connection.Close();
            //}
            return output;
        }
    }
}
