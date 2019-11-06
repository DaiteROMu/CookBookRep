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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger _logger;

        private string _connectionString = ConfigurationManager.ConnectionStrings["CookBookDB"].ConnectionString;
        private delegate void _executerVoidCmd(SqlConnection sqlConnection, SqlCommand cmd);
        private delegate List<Category> _executerCategoryCmd(SqlConnection sqlConnection, SqlCommand cmd);

        public CategoryRepository(ILogger logger)
        {
            if (logger != null)
            {
                _logger = logger;
            }
            else
            {
                LoggerFactory loggerFactory = new LoggerFactory();
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

        private List<Category> ExecuteCategorySqlCmd(SqlConnection sqlConnection, SqlCommand cmd)
        {
            List<Category> returnedList = new List<Category>();
            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.CategoryId = reader.GetFieldValue<int>(0);
                        category.Name = reader.GetFieldValue<string>(1);
                        if (!reader.IsDBNull(2))
                        {
                            category.ParentId = reader.GetFieldValue<int>(2);
                        }
                        else
                        {
                            category.ParentId = 0;
                        }

                        returnedList.Add(category);
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

        private void UpdateCategoryParentId(int categoryId, int parentId)
        {
            _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCategoryParentId", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@ParentId", parentId);
                    executer(sqlConnection, cmd);
                }
            }
        }

        public void InsertCategory(Category category)
        {
            if (category != null)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertCategory", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", category.Name);
                        if (category.ParentId == 0)
                        {
                            cmd.Parameters.AddWithValue("@ParentId", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ParentId", category.ParentId);
                        }
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public void UpdateCategory(Category category)
        {
            if (category != null)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCategory", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                        cmd.Parameters.AddWithValue("@Name", category.Name);
                        if (category.ParentId == 0)
                        {
                            cmd.Parameters.AddWithValue("@ParentId", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ParentId", category.ParentId);
                        }
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public void DeleteCategory(int categoryId)
        {
            if (categoryId > 0)
            {
                Category category = GetCategoryById(categoryId);
                List<Category> childCategories = GetChildCategoriesById(categoryId);

                if (childCategories != null)
                {
                    if (childCategories.Count > 0)
                    {
                        foreach(Category item in childCategories)
                        {
                            UpdateCategoryParentId(item.CategoryId, category.ParentId);
                        }
                    }
                }

                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteCategory", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = new Category();

            if (categoryId > 0)
            {
                _executerCategoryCmd executer = new _executerCategoryCmd(ExecuteCategorySqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetCategoryById", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        category = executer(sqlConnection, cmd).FirstOrDefault();
                    }
                }
            }

            return category;
        }

        public List<Category> GetCategories()
        {
            List<Category> categoriesList = new List<Category>();
            _executerCategoryCmd executer = new _executerCategoryCmd(ExecuteCategorySqlCmd);

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCategories", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    categoriesList = executer(sqlConnection, cmd);
                }
            }

            return categoriesList;
        }

        public List<Category> GetTopCategories()
        {
            List<Category> categoriesList = new List<Category>();
            _executerCategoryCmd executer = new _executerCategoryCmd(ExecuteCategorySqlCmd);

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetTopCategories", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    categoriesList = executer(sqlConnection, cmd);
                }
            }

            return categoriesList;
        }

        public List<Category> GetChildCategoriesById(int categoryId)
        {
            List<Category> categoriesList = new List<Category>();
            
            if(categoryId>0)
            {
                _executerCategoryCmd executer = new _executerCategoryCmd(ExecuteCategorySqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetChildCategoriesById", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        categoriesList = executer(sqlConnection, cmd);
                    }
                }
            }

            return categoriesList;
        }
    }
}
