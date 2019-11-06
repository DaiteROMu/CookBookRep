using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using CookBook.Common.Models;
using CookBook.Common.Log;

namespace CookBook.DAL.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ILogger _logger;
        private string _connectionString = ConfigurationManager.ConnectionStrings["CookBookDB"].ConnectionString;
        private delegate void _executerVoidCmd(SqlConnection sqlConnection, SqlCommand cmd);
        private delegate Recipe _executerRecipeCmd(SqlConnection sqlConnection, SqlCommand cmd);
        private delegate List<RecipeView> _executerRecipeViewCmd(SqlConnection sqlConnection, SqlCommand cmd);
        private delegate RecipeDetails _executerRecipeDetailsCmd(SqlConnection sqlConnection, SqlCommand cmd);
        private delegate List<RecipeIngridientView> _executerRecipeIngridientCmd(SqlConnection sqlConnection, SqlCommand cmd);

        public RecipeRepository(ILogger logger)
        {
            if (logger != null)
            {
                _logger = logger;
            }
            else
            {
                LoggerFactory loggerFactory = new LoggerFactory();
                _logger = loggerFactory.GetLogger();
            }
        }

        private void ExecuteVoidSqlCmd(SqlConnection sqlConnection, SqlCommand cmd)
        {
            try
            {
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _logger.ErrorMessage(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private Recipe ExecuteRecipeSqlCmd(SqlConnection sqlConnection, SqlCommand cmd)
        {
            Recipe recipe = new Recipe();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        recipe.RecipeId = reader.GetFieldValue<int>(0);
                        recipe.Name = reader.GetFieldValue<string>(1);
                        if (!reader.IsDBNull(2))
                        {
                            recipe.ImageUrl = reader.GetFieldValue<string>(2);
                        }
                        else
                        {
                            recipe.ImageUrl = "";
                        }
                        recipe.CategoryId = reader.GetFieldValue<int>(3);
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.ErrorMessage(ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return recipe;
        }

        private List<RecipeView> ExecuteRecipeViewSqlCmd(SqlConnection sqlConnection, SqlCommand cmd)
        {
            List<RecipeView> returnedList = new List<RecipeView>();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RecipeView recipeView = new RecipeView();
                        recipeView.RecipeId = reader.GetFieldValue<int>(0);
                        recipeView.Name = reader.GetFieldValue<string>(1);
                        if (!reader.IsDBNull(2))
                        {
                            recipeView.ImageUrl = reader.GetFieldValue<string>(2);
                        }
                        else
                        {
                            recipeView.ImageUrl = "";
                        }
                        recipeView.CategoryId = reader.GetFieldValue<int>(3);
                        recipeView.CategoryName = reader.GetFieldValue<string>(4);
                        returnedList.Add(recipeView);
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.ErrorMessage(ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return returnedList;
        }

        private RecipeDetails ExecuteRecipeDetailsSqlCmd(SqlConnection sqlConnection, SqlCommand cmd)
        {
            RecipeDetails recipeDetails = new RecipeDetails();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        recipeDetails.RecipeId = reader.GetFieldValue<int>(0);
                        if (!reader.IsDBNull(1))
                        {
                            recipeDetails.CookingTime = reader.GetFieldValue<string>(1);
                        }
                        else
                        {
                            recipeDetails.CookingTime = "";
                        }
                        if (!reader.IsDBNull(2))
                        {
                            recipeDetails.CookingTemperature = reader.GetFieldValue<string>(2);
                        }
                        else
                        {
                            recipeDetails.CookingTemperature = "";
                        }
                        if (!reader.IsDBNull(3))
                        {
                            recipeDetails.Description = reader.GetFieldValue<string>(3);
                        }
                        else
                        {
                            recipeDetails.Description = "";
                        }
                        recipeDetails.Sequencing = reader.GetFieldValue<string>(4);
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.ErrorMessage(ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return recipeDetails;
        }

        private List<RecipeIngridientView> ExecuteRecipeIngridientSqlCmd(SqlConnection sqlConnection, SqlCommand cmd)
        {
            List<RecipeIngridientView> recipeIngridientsList = new List<RecipeIngridientView>();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RecipeIngridientView recipeIngridient = new RecipeIngridientView()
                        {
                            RecipeId = reader.GetFieldValue<int>(0),
                            IngridientId = reader.GetFieldValue<int>(1),
                            Name = reader.GetFieldValue<string>(2),
                            Weight = reader.GetFieldValue<string>(3)
                        };
                        recipeIngridientsList.Add(recipeIngridient);
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.ErrorMessage(ex);
            }
            finally
            {
                sqlConnection.Close();
            }

            return recipeIngridientsList;
        }

        public int InsertRecipe(Recipe recipe, RecipeDetails recipeDetails)
        {
            int recipeId = 0;

            if (recipe != null && recipeDetails != null)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmdInsertRecipe = new SqlCommand("InsertRecipe", sqlConnection);
                    cmdInsertRecipe.CommandType = CommandType.StoredProcedure;
                    cmdInsertRecipe.Parameters.AddWithValue("@Name", recipe.Name);
                    if (recipe.ImageUrl == null)
                    {
                        cmdInsertRecipe.Parameters.AddWithValue("@ImageUrl", DBNull.Value);
                    }
                    else
                    {
                        cmdInsertRecipe.Parameters.AddWithValue("@ImageUrl", recipe.ImageUrl);
                    }
                    cmdInsertRecipe.Parameters.AddWithValue("@CategoryId", recipe.CategoryId);

                    SqlCommand cmdInsertRecipeDetails = new SqlCommand("InsertRecipeDetails", sqlConnection);
                    cmdInsertRecipeDetails.CommandType = CommandType.StoredProcedure;
                    if (recipeDetails.CookingTime == null)
                    {
                        cmdInsertRecipeDetails.Parameters.AddWithValue("@CookingTime", DBNull.Value);
                    }
                    else
                    {
                        cmdInsertRecipeDetails.Parameters.AddWithValue("@CookingTime", recipeDetails.CookingTime);
                    }
                    if (recipeDetails.CookingTemperature == null)
                    {
                        cmdInsertRecipeDetails.Parameters.AddWithValue("@CookingTemperature", DBNull.Value);
                    }
                    else
                    {
                        cmdInsertRecipeDetails.Parameters.AddWithValue("@CookingTemperature", recipeDetails.CookingTemperature);
                    }
                    if (recipeDetails.Description == null)
                    {
                        cmdInsertRecipeDetails.Parameters.AddWithValue("@Description", DBNull.Value);
                    }
                    else
                    {
                        cmdInsertRecipeDetails.Parameters.AddWithValue("@Description", recipeDetails.Description);
                    }
                    if (recipeDetails.Sequencing == null)
                    {
                        cmdInsertRecipeDetails.Parameters.AddWithValue("@Sequencing", DBNull.Value);
                    }
                    else
                    {
                        cmdInsertRecipeDetails.Parameters.AddWithValue("@Sequencing", recipeDetails.Sequencing);
                    }                    

                    SqlCommand cmdLastRecipeId = new SqlCommand("GetLastInsertedRecipeId", sqlConnection);
                    cmdLastRecipeId.CommandType = CommandType.StoredProcedure;

                    SqlTransaction transaction = null;

                    try
                    {
                        sqlConnection.Open();
                        transaction = sqlConnection.BeginTransaction();
                        cmdInsertRecipe.Transaction = transaction;
                        cmdInsertRecipeDetails.Transaction = transaction;
                        cmdLastRecipeId.Transaction = transaction;
                        cmdInsertRecipe.ExecuteNonQuery();
                        cmdInsertRecipeDetails.ExecuteNonQuery();
                        object recipeIdObj = cmdLastRecipeId.ExecuteScalar();
                        recipeId = int.Parse(recipeIdObj.ToString());
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _logger.ErrorMessage(ex);
                        transaction.Rollback();
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }

            return recipeId;
        }

        public void UpdateRecipe(Recipe recipe, RecipeDetails recipeDetails)
        {
            if (recipe != null && recipeDetails != null)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmdUpdateRecipe = new SqlCommand("UpdateRecipe", sqlConnection);
                    cmdUpdateRecipe.CommandType = CommandType.StoredProcedure;
                    cmdUpdateRecipe.Parameters.AddWithValue("@RecipeId", recipe.RecipeId);
                    cmdUpdateRecipe.Parameters.AddWithValue("@Name", recipe.Name);
                    if (recipe.ImageUrl == null)
                    {
                        cmdUpdateRecipe.Parameters.AddWithValue("@ImageUrl", DBNull.Value);
                    }
                    else
                    {
                        cmdUpdateRecipe.Parameters.AddWithValue("@ImageUrl", recipe.ImageUrl);
                    }
                    cmdUpdateRecipe.Parameters.AddWithValue("@CategoryId", recipe.CategoryId);

                    SqlCommand cmdUpdateRecipeDetails = new SqlCommand("UpdateRecipeDetails", sqlConnection);
                    cmdUpdateRecipeDetails.CommandType = CommandType.StoredProcedure;
                    cmdUpdateRecipeDetails.Parameters.AddWithValue("@RecipeId", recipe.RecipeId);
                    if (recipeDetails.CookingTime == null)
                    {
                        cmdUpdateRecipeDetails.Parameters.AddWithValue("@CookingTime", DBNull.Value);
                    }
                    else
                    {
                        cmdUpdateRecipeDetails.Parameters.AddWithValue("@CookingTime", recipeDetails.CookingTime);
                    }
                    if (recipeDetails.CookingTemperature == null)
                    {
                        cmdUpdateRecipeDetails.Parameters.AddWithValue("@CookingTemperature", DBNull.Value);
                    }
                    else
                    {
                        cmdUpdateRecipeDetails.Parameters.AddWithValue("@CookingTemperature", recipeDetails.CookingTemperature);
                    }
                    if (recipeDetails.Description == null)
                    {
                        cmdUpdateRecipeDetails.Parameters.AddWithValue("@Description", DBNull.Value);
                    }
                    else
                    {
                        cmdUpdateRecipeDetails.Parameters.AddWithValue("@Description", recipeDetails.Description);
                    }
                    if (recipeDetails.Sequencing == null)
                    {
                        cmdUpdateRecipeDetails.Parameters.AddWithValue("@Sequencing", DBNull.Value);
                    }
                    else
                    {
                        cmdUpdateRecipeDetails.Parameters.AddWithValue("@Sequencing", recipeDetails.Sequencing);
                    }

                    SqlTransaction transaction = null;

                    try
                    {
                        sqlConnection.Open();
                        transaction = sqlConnection.BeginTransaction();
                        cmdUpdateRecipe.Transaction = transaction;
                        cmdUpdateRecipeDetails.Transaction = transaction;
                        cmdUpdateRecipe.ExecuteNonQuery();
                        cmdUpdateRecipeDetails.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _logger.ErrorMessage(ex);
                        transaction.Rollback();
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        public void UpdateRecipeDetailsSequencing(int recipeId, string sequencing)
        {
            if (recipeId > 0)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateRecipeDetailsSequencing", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                        if (sequencing == null)
                        {
                            cmd.Parameters.AddWithValue("@Sequencing", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Sequencing", sequencing);
                        }
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public void DeleteRecipe(int recipeId)
        {
            if (recipeId > 0)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteRecipe", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public void InsertRecipeIngridient(RecipeIngridient recipeIngridient)
        {
            if (recipeIngridient != null)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertRecipeIngridient", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecipeId", recipeIngridient.RecipeId);
                        cmd.Parameters.AddWithValue("@IngridientId", recipeIngridient.IngridientId);
                        cmd.Parameters.AddWithValue("@Weight", recipeIngridient.Weight);
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public void UpdateRecipeIngridient(RecipeIngridient recipeIngridient)
        {
            if (recipeIngridient != null)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateRecipeIngridient", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecipeId", recipeIngridient.RecipeId);
                        cmd.Parameters.AddWithValue("@IngridientId", recipeIngridient.IngridientId);
                        cmd.Parameters.AddWithValue("@Weight", recipeIngridient.Weight);
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public void DeleteRecipeIngridient(int recipeId, int ingridientId)
        {
            if (recipeId > 0 && ingridientId > 0)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteRecipeIngridient", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                        cmd.Parameters.AddWithValue("@IngridientId", ingridientId);
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public Recipe GetRecipeById(int recipeId)
        {
            Recipe recipe = new Recipe();

            if (recipeId > 0)
            {
                _executerRecipeCmd executer = new _executerRecipeCmd(ExecuteRecipeSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetRecipeById", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                        recipe = executer(sqlConnection, cmd);
                    }
                }
            }

            return recipe;
        }

        public RecipeDetails GetRecipeDetailsByRecipeId(int recipeId)
        {
            RecipeDetails recipeDetails = new RecipeDetails();

            if (recipeId > 0)
            {
                _executerRecipeDetailsCmd executer = new _executerRecipeDetailsCmd(ExecuteRecipeDetailsSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetRecipeDetailsById", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                        recipeDetails = executer(sqlConnection, cmd);
                    }
                }
            }

            return recipeDetails;
        }

        public List<RecipeIngridientView> GetRecipeIngridientsByRecipeId(int recipeId)
        {
            List<RecipeIngridientView> recipeIngridientsList = new List<RecipeIngridientView>();

            if (recipeId > 0)
            {
                _executerRecipeIngridientCmd executer = new _executerRecipeIngridientCmd(ExecuteRecipeIngridientSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetRecipeIngridientsByRecipeId", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                        recipeIngridientsList = executer(sqlConnection, cmd);
                    }
                }
            }

            return recipeIngridientsList;
        }

        public RecipeIngridientView GetRecipeIngridient(int recipeId, int ingridientId)
        {
            List<RecipeIngridientView> recipeIngridientsList = new List<RecipeIngridientView>();
            RecipeIngridientView recipeIngridientView = new RecipeIngridientView();

            if (recipeId > 0 && ingridientId > 0)
            {
                _executerRecipeIngridientCmd executer = new _executerRecipeIngridientCmd(ExecuteRecipeIngridientSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetRecipeIngridient", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                        cmd.Parameters.AddWithValue("@IngridientId", ingridientId);
                        recipeIngridientsList = executer(sqlConnection, cmd);
                        recipeIngridientView = recipeIngridientsList.FirstOrDefault();
                    }
                }
            }

            return recipeIngridientView;
        }

        public List<RecipeView> GetRecipes()
        {
            _executerRecipeViewCmd executer = new _executerRecipeViewCmd(ExecuteRecipeViewSqlCmd);
            List<RecipeView> recipesList = new List<RecipeView>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetRecipes", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    recipesList = executer(sqlConnection, cmd);
                }
            }

            return recipesList;
        }

        public IEnumerable<RecipeView> GetRecipesByCategoryId(int categoryId)
        {            
            List<RecipeView> recipesList = new List<RecipeView>();

            if (categoryId > 0)
            {
                _executerRecipeViewCmd executer = new _executerRecipeViewCmd(ExecuteRecipeViewSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetRecipesByCategoryId", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        recipesList = executer(sqlConnection, cmd);
                    }
                }
            }

            return recipesList;
        }

        public IEnumerable<RecipeView> GetRecipesByRecipeName(string recipeName)
        {            
            List<RecipeView> recipesList = new List<RecipeView>();

            if (!string.IsNullOrEmpty(recipeName))
            {
                _executerRecipeViewCmd executer = new _executerRecipeViewCmd(ExecuteRecipeViewSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetRecipesByRecipeName", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@recipeName", recipeName);
                        recipesList = executer(sqlConnection, cmd);
                    }
                }
            }

            return recipesList;
        }

        public IEnumerable<RecipeView> GetRecipesByCategoryName(string categoryName)
        {            
            List<RecipeView> recipesList = new List<RecipeView>();

            if (!string.IsNullOrEmpty(categoryName))
            {
                _executerRecipeViewCmd executer = new _executerRecipeViewCmd(ExecuteRecipeViewSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetRecipesByCategoryName", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@categoryName", categoryName);
                        recipesList = executer(sqlConnection, cmd);
                    }
                }
            }

            return recipesList;
        }

        public IEnumerable<RecipeView> GetRecipesByIngridientName(string ingridientName)
        {            
            List<RecipeView> recipesList = new List<RecipeView>();

            if (!string.IsNullOrEmpty(ingridientName))
            {
                _executerRecipeViewCmd executer = new _executerRecipeViewCmd(ExecuteRecipeViewSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetRecipesByIngridientName", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ingridientName", ingridientName);
                        recipesList = executer(sqlConnection, cmd);
                    }
                }
            }

            return recipesList;
        }
    }
}
