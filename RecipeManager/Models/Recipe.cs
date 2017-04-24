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
        public List<RecipePart> RecipeParts { get; set; }
    }

    public static class RecipeDb
    {


        public static int GetContributions(string UserName)
        {

            int output = 0;
            MySqlConnection connection = MySqlProvider.Connection;
            MySqlCommand Command = connection.CreateCommand();
            Command.CommandText = "SELECT Count(RecipeId) as contrib FROM Recipes WHERE Recipes.SourceName = @UseName";
            Command.Parameters.AddWithValue("@UserName", UserName);

            MySqlDataReader reader = null;

            try
            {
                reader = Command.ExecuteReader();
                int contribution = Convert.ToInt32(reader["contrib"]);
                output = contribution;
            }
           
            catch (MySqlException ex)
            {

            }


            finally
            {
                reader?.Close();
            }


            return output;

        }


        public static List<Recipe> SelectUserRecipesById(int UserId)
        {


            List<Recipe> output = new List<Recipe>();
            MySqlConnection connection = MySqlProvider.Connection;
            MySqlCommand Command = connection.CreateCommand();
            Command.CommandText = "SELECT * FROM RecipeLists JOIN Recipes WHERE RecipeLists.UserId = @UserId";
            Command.Parameters.AddWithValue("@UserId", UserId);
            MySqlDataReader recipeReader = null;
            try
            {

                recipeReader = Command.ExecuteReader();
                if (recipeReader.Read())
                {

                    var recipe = new Recipe()
                    {
                        RecipeId = Convert.ToInt32(recipeReader["RecipeId"]),
                        RecipeName = Convert.ToString(recipeReader["RecipeName"]),
                        Instructions = Convert.ToString(recipeReader["Instructions"]),
                        Image = new Uri(Convert.ToString(recipeReader["Image"])),
                        Servings = Convert.ToInt16(recipeReader["Servings"]),
                        SourceName = Convert.ToString(recipeReader["SourceName"]),
                        MinutesToMake = Convert.ToInt16(recipeReader["MinutesToMake"])

                    };
                    output.Add(recipe);

                }


            }
            catch (MySqlException ex)
            {
                output.Add(new Recipe() { RecipeName = ex.Message });
            }
        
            finally
            {
                recipeReader?.Close();
            }
            return output;
        }
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
                            Servings = Convert.ToInt32(reader["Servings"]),
                            SourceName = Convert.ToString(reader["SourceName"]),
                            MinutesToMake = Convert.ToInt32(reader["MinutesToMake"])

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

        public static void AddToMyShoppingList(string IngName)
        {

            MySqlConnection connection = MySqlProvider.Connection;
            MySqlCommand command = connection.CreateCommand();
            //command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "CALL AddToMyShoppingList(@i_name)";
           
            command.Parameters.AddWithValue("@i_name", IngName);
           

            try
            {

                //connection.Open();

                command.ExecuteNonQuery();

            }

            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }

        public static void Insert(AddRecipeViewModel model)
        {
            MySqlConnection connection = MySqlProvider.Connection;
            MySqlCommand command = connection.CreateCommand();
            //command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText ="Call CreateRecipe(@r_name,@r_instructions,@r_image,@r_servings,@r_minutesToMake)";

            string image = "https://spoonacular.com/recipeImages.jpg";

           

            
           

            command.Parameters.AddWithValue("@r_name", model.Recipe.RecipeName);
            command.Parameters.AddWithValue("@r_instructions", model.Recipe.Instructions);
            command.Parameters.AddWithValue("@r_image", image);
            command.Parameters.AddWithValue("@r_servings", model.Recipe.Servings);
            command.Parameters.AddWithValue("@r_minutesToMake", model.Recipe.MinutesToMake);



            try
            {

                //connection.Open();

                command.ExecuteNonQuery();

            }

            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


        }

    
        public static List<Recipe> SelectAllUserMadeRecipes()
        {
            List<Recipe> output = new List<Recipe>();
            MySqlConnection connection = MySqlProvider.Connection;

            MySqlCommand recipeListCommand = connection.CreateCommand();
            recipeListCommand.CommandText = "SELECT RecipeId, RecipeName FROM Recipes WHERE SourceName IN(SELECT Username FROM CurrentUser)";

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
                            Servings = Convert.ToInt32(reader["Servings"]),
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

            MySqlCommand recipeCommand = connection.CreateCommand();
            
            recipeCommand.Parameters.AddWithValue("@param1", RecipeId);
            recipeCommand.CommandText = "SELECT * FROM Recipes WHERE RecipeId = @param1";
            
            MySqlDataReader recipeReader = null;
            MySqlDataReader recipePartsReader = null;
            try
            {

                recipeReader = recipeCommand.ExecuteReader();
                if (recipeReader.Read())
                {

                    var recipe = new Recipe()
                    {
                        RecipeId = Convert.ToInt32(recipeReader["RecipeId"]),
                        RecipeName = Convert.ToString(recipeReader["RecipeName"]),
                        Instructions = Convert.ToString(recipeReader["Instructions"]),
                        Image = new Uri(Convert.ToString(recipeReader["Image"])),
                        Servings = Convert.ToInt16(recipeReader["Servings"]),
                        SourceName = Convert.ToString(recipeReader["SourceName"]),
                        MinutesToMake = Convert.ToInt16(recipeReader["MinutesToMake"])

                    };
                    output = recipe;

                    recipeReader.Close();

                    MySqlCommand recipePartsCommand = MySqlProvider.Instance.
                        GetRecipeIngredientsCommand(recipe.RecipeId);
                    recipePartsReader = recipePartsCommand.ExecuteReader();

                    recipe.RecipeParts = new List<RecipePart>();
                    while (recipePartsReader.Read())
                    {
                        RecipePart recipePart = new RecipePart()
                        {
                            PartText = Convert.ToString(recipePartsReader["PartText"]),
                            IngName = Convert.ToString(recipePartsReader["IngName"]),
                            PartAmount = Convert.ToDouble(recipePartsReader["PartAmount"]),
                            MeasureName = Convert.ToString(recipePartsReader["MeasureName"])
                        };
                        recipe.RecipeParts.Add(recipePart);
                    }
                }
            }
            catch (MySqlException ex)
            {
                // output.Add(new Recipe() { RecipeName = ex.Message });
            }
            finally
            {
                recipeReader?.Close();
                recipePartsReader?.Close();
            }
            return output;

        }
        public static List<Recipe> SearchRecipes(string searchTerm)
        {
            if (searchTerm == null)
                searchTerm = "";

            //MySqlConnection connection = MySqlProvider.Connection;

            //MySqlCommand setSearchTermCommand = connection.CreateCommand();
            //setSearchTermCommand.CommandText = $"SET @searchTerm = '{searchTerm}'";
            //setSearchTermCommand.ExecuteNonQuery();

            //MySqlCommand searchCommand = connection.CreateCommand();
            //searchCommand.CommandText = "EXECUTE SearchRecipeNames USING @searchTerm";

            MySqlCommand searchCommand = MySqlProvider.Instance.GetSearchCommand(searchTerm);

            MySqlDataReader reader = null;
            var output = new List<Recipe>();
            try
            {
                reader = searchCommand.ExecuteReader();

                while (reader.Read())
                {
                    var recipe = new Recipe()
                    {
                        RecipeId = Convert.ToInt32(reader["RecipeId"]),
                        RecipeName = Convert.ToString(reader["RecipeName"]),
                        Instructions = Convert.ToString(reader["Instructions"]),
                        Image = new Uri(Convert.ToString(reader["Image"])),
                        Servings = Convert.ToInt32(reader["Servings"]),
                        SourceName = Convert.ToString(reader["SourceName"]),
                        MinutesToMake = Convert.ToInt32(reader["MinutesToMake"])
                    };

                    output.Add(recipe);
                }
            }
            finally
            {
                reader?.Close();
            }

            return output;
        }
        public static void AddToMyRecipes(int RecipeId)
        {
           
            MySqlConnection connection = MySqlProvider.Connection;
            MySqlCommand command = connection.CreateCommand();
            //command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "CALL AddToUserRecipeList(@r_id)";

            command.Parameters.AddWithValue("@r_id", RecipeId);
           


            try
            {

                //connection.Open();

                command.ExecuteNonQuery();

            }

            catch (MySqlException ex)
            {

            }


        }
        public static void RemoveFromMyRecipes(int RecipeId)
        {
            MySqlConnection connection = MySqlProvider.Connection;
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "CALL DeleteFromUserRecipeList(@r_id)";
            command.Parameters.AddWithValue("@r_id", RecipeId);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {

            }
        }


        public static List<Ingredient> SelectUserShoppingList()
        {


            List<Ingredient> output = new List<Ingredient>();
            MySqlConnection connection = MySqlProvider.Connection;

            MySqlCommand recipeListCommand = connection.CreateCommand();
            recipeListCommand.CommandText = "CALL SelectUserShoopingList()";

            MySqlDataReader reader = null;
            try
            {

                //connection.Open();
                reader = recipeListCommand.ExecuteReader();
                if (reader.Read())
                {
                    do
                    {
                        var ing = new Ingredient()
                        {
                           
                            IngName = Convert.ToString(reader["IngName"]),
                            
                        };
                        output.Add(ing);
                    } while (reader.Read());


                }
            }
            catch (MySqlException ex)
            {
                output.Add(new Ingredient() { IngName = ex.Message });
            }
            finally
            {
                reader?.Close();
            }
            return output;
        }

    }



}