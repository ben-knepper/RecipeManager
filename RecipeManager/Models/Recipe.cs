using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RecipeManager.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string Instructions { get; set; } //may change later
        public Uri Image { get; set; }
        public int Servings { get; set; }
        public string SourceName { get; set; }
        public int MinutesToMake { get; set; }
    }

    public static class RecipeDb
    {
        public static List<Recipe> SelectUserRecipes()
        {


            List<Recipe> output = new List<Recipe>();
            MySqlConnection connection = MySqlProvider.Connection;

            MySqlCommand recipeListCommand = connection.CreateCommand();
            recipeListCommand.CommandText = "SELECT * FROM UserRecipeList JOIN Recipes on UserRecipeList.RecipeId = Recipes.RecipeId";

            MySqlDataReader reader = null;
            try
            {

                //connection.Open();
                reader = recipeListCommand.ExecuteReader();
                if (reader.Read())
                {
                    do
                    {
                        var recipe = new Recipe()
                        {
                            RecipeId = Convert.ToInt32(reader["RecipeId"]),
                            RecipeName = Convert.ToString(reader["RecipeName"]),
                            Instructions = Convert.ToString(reader["Instructions"]),
                            Image = new Uri(Convert.ToString(reader["Image"])),
                            Servings = Convert.ToInt16(reader["Servings"]),
                            SourceName = Convert.ToString(reader["SourceName"]),
                            MinutesToMake = Convert.ToInt16(reader["MinutesToMake"])

                        };
                        output.Add(recipe);
                    } while (reader.Read());


                }
            }
            catch (MySqlException ex)
            {
                output.Add(new Recipe() { RecipeName = ex.Message });
            }
            finally
            {
                reader?.Close();
            }
            return output;
        }
        public static List<Recipe> SelectAllRecipes()
        {
            List<Recipe> output = new List<Recipe>();
            MySqlConnection connection = MySqlProvider.Connection;

            MySqlCommand recipeListCommand = connection.CreateCommand();
            recipeListCommand.CommandText = "SELECT * FROM Recipes";

            MySqlDataReader reader = null;
            try
            {

                reader = recipeListCommand.ExecuteReader();
                if (reader.Read())
                {
                    do
                    {
                        var recipe = new Recipe()
                        {
                            RecipeId = Convert.ToInt32(reader["RecipeId"]),
                            RecipeName = Convert.ToString(reader["RecipeName"]),
                            Instructions = Convert.ToString(reader["Instructions"]),
                            Image = new Uri(Convert.ToString(reader["Image"])),
                            Servings = Convert.ToInt16(reader["Servings"]),
                            SourceName = Convert.ToString(reader["SourceName"]),
                            MinutesToMake = Convert.ToInt16(reader["MinutesToMake"])

                        };
                        Console.WriteLine(recipe.ToString());
                        output.Add(recipe);
                    } while (reader.Read());


                }
            }
            catch (MySqlException ex)
            {
                output.Add(new Recipe() { RecipeName = ex.Message });
            }
            finally
            {
                reader?.Close();
            }
            return output;
        }
        public static Recipe SelectRecipe(int RecipeId)
        {

            Recipe output = new Recipe();
            MySqlConnection connection = MySqlProvider.Connection;

            MySqlCommand Command = connection.CreateCommand();
            
            Command.Parameters.AddWithValue("@param1", RecipeId);
            Command.CommandText = "SELECT * FROM Recipes WHERE RecipeId = @param1";
            
            MySqlDataReader reader = null;
            try
            {

                reader = Command.ExecuteReader();
                if (reader.Read())
                {

                    var recipe = new Recipe()
                    {
                        RecipeId = Convert.ToInt32(reader["RecipeId"]),
                        RecipeName = Convert.ToString(reader["RecipeName"]),
                        Instructions = Convert.ToString(reader["Instructions"]),
                        Image = new Uri(Convert.ToString(reader["Image"])),
                        Servings = Convert.ToInt16(reader["Servings"]),
                        SourceName = Convert.ToString(reader["SourceName"]),
                        MinutesToMake = Convert.ToInt16(reader["MinutesToMake"])

                    };
                    output = recipe;



                }
            }
            catch (MySqlException ex)
            {
                // output.Add(new Recipe() { RecipeName = ex.Message });
            }
            finally
            {
                reader?.Close();
            }
            return output;

        }
        public static List<Recipe> SearchRecipes(string searchTerm)
        {
            MySqlConnection connection = MySqlProvider.Connection;

            MySqlCommand setSearchTermCommand = connection.CreateCommand();
            setSearchTermCommand.CommandText = $"SET @searchTerm = '{searchTerm}'";
            setSearchTermCommand.ExecuteNonQuery();

            MySqlCommand searchCommand = connection.CreateCommand();
            searchCommand.CommandText = "EXECUTE SearchRecipeNames USING @searchTerm";

            MySqlDataReader reader = null;
            var output = new List<Recipe>();
            try
            {
                reader = searchCommand.ExecuteReader();

                if (reader.Read())
                {
                    var recipe = new Recipe()
                    {
                        RecipeId = Convert.ToInt32(reader["RecipeId"]),
                        RecipeName = Convert.ToString(reader["RecipeName"]),
                        Instructions = Convert.ToString(reader["Instructions"]),
                        Image = new Uri(Convert.ToString(reader["Image"])),
                        Servings = Convert.ToInt16(reader["Servings"]),
                        SourceName = Convert.ToString(reader["SourceName"]),
                        MinutesToMake = Convert.ToInt16(reader["MinutesToMake"])
                    };

                    output.Add(recipe);
                }
            }
            finally
            {
                reader?.Read();
            }

            return output;
        }
    }

}
