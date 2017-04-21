using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace RecipeManager
{
    public class MySqlProvider : IDisposable
    {
        private static MySqlProvider _instance;
        private MySqlConnection _connection;

        private MySqlProvider()
        {
            OpenNewConnection();
        }

        public static MySqlProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MySqlProvider();
                return _instance;
            }
        }

        public static MySqlConnection Connection
        {
            get
            {
                if (Instance._connection.State == ConnectionState.Closed
                    || Instance._connection.State == ConnectionState.Broken)
                    Instance.OpenNewConnection();
                return Instance._connection;
            }
        }

        public void OpenNewConnection()
        {
            _connection?.Close();

            _connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnStr"].ConnectionString);
            _connection.Open();

            //// SearchRecipeNames
            //// Prepared statement for searching recipes by name
            //MySqlCommand searchRecipeNamesCommand = _connection.CreateCommand();
            //searchRecipeNamesCommand.CommandText = @"PREPARE SearchRecipeNames FROM 'SELECT * FROM Recipes WHERE RecipeName LIKE CONCAT(\'%\', ?, \'%\')'";
            //searchRecipeNamesCommand.ExecuteNonQuery();

            //// GetRecipeIngredients
            //// Prepared statement for getting recipe ingredients
            //MySqlCommand getRecipeIngredientsCommand = _connection.CreateCommand();
            //getRecipeIngredientsCommand.CommandText = @"PREPARE GetRecipeIngredients FROM 'SELECT PartText FROM RecipeParts WHERE RecipeId = ? ORDER BY PartNo';";
            //getRecipeIngredientsCommand.ExecuteNonQuery();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        private MySqlCommand _searchCommand;
        public MySqlCommand GetSearchCommand(string searchTerm)
        {
            if (_searchCommand == null)
            {
                _searchCommand = _connection.CreateCommand();
                _searchCommand.CommandText = @"SELECT * FROM Recipes WHERE RecipeName LIKE CONCAT('%', @searchTerm, '%')";
                _searchCommand.Prepare();
                _searchCommand.Parameters.AddWithValue("@searchTerm", "searchTerm");
            }

            _searchCommand.Parameters["@searchTerm"].Value = searchTerm;

            return _searchCommand;
        }

        private MySqlCommand _recipeIngredientsCommand;
        public MySqlCommand GetRecipeIngredientsCommand(int recipeId)
        {
            if (_recipeIngredientsCommand == null)
            {
                _recipeIngredientsCommand = _connection.CreateCommand();
                _recipeIngredientsCommand.CommandText = @"PREPARE GetRecipeIngredients FROM 'SELECT PartText FROM RecipeParts WHERE RecipeId = ?recipeId ORDER BY PartNo';";
                _recipeIngredientsCommand.Prepare();
                _recipeIngredientsCommand.Parameters.AddWithValue("?recipeId", 0);
            }

            _recipeIngredientsCommand.Parameters["?recipeId"].Value = recipeId;

            return _searchCommand;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection.Close();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MySqlProvider() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}