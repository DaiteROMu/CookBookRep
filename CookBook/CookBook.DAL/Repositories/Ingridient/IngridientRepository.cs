using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CookBook.Common.Models;
using CookBook.Common.Log;

namespace CookBook.DAL.Repositories
{
    public class IngridientRepository : IIngridientRepository
    {
        private readonly ILogger _logger;
        private string _connectionString = ConfigurationManager.ConnectionStrings["CookBookDB"].ConnectionString;
        private delegate void _executerVoidCmd(SqlConnection sqlConnection, SqlCommand cmd);
        private delegate List<Ingridient> _executerIngridientCmd(SqlConnection sqlConnection, SqlCommand cmd);

        public IngridientRepository(ILogger logger)
        {
            if(logger!=null)
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

        private List<Ingridient> ExecuteIngridientSqlCmd(SqlConnection sqlConnection, SqlCommand cmd)
        {
            List<Ingridient> returnedList = new List<Ingridient>();
            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ingridient ingridient = new Ingridient();
                        ingridient.IngridientId = reader.GetFieldValue<int>(0);
                        ingridient.Name = reader.GetFieldValue<string>(1);
                        returnedList.Add(ingridient);
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

        public void InsertInrgridient(Ingridient ingridient)
        {
            if (ingridient != null)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertIngridient", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", ingridient.Name);
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public void UpdateIngridient(Ingridient ingridient)
        {
            if (ingridient != null)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateIngridient", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IngridientId", ingridient.IngridientId);
                        cmd.Parameters.AddWithValue("@Name", ingridient.Name);
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public void DeleteIngridient(int ingridientId)
        {
            if (ingridientId > 0)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteIngridient", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IngridientId", ingridientId);
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public Ingridient GetIngridientById(int ingridientId)
        {
            Ingridient ingridient = new Ingridient();

            if (ingridientId > 0)
            {
                _executerIngridientCmd executer = new _executerIngridientCmd(ExecuteIngridientSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetIngridientById", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IngridientId", ingridientId);
                        ingridient = executer(sqlConnection, cmd).FirstOrDefault();
                    }
                }
            }

            return ingridient;
        }

        public List<Ingridient> GetIngridients()
        {
            List<Ingridient> ingridientsList = new List<Ingridient>();
            _executerIngridientCmd executer = new _executerIngridientCmd(ExecuteIngridientSqlCmd);

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetIngridients", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    ingridientsList = executer(sqlConnection, cmd);
                }                
            }

            return ingridientsList;
        }
    }
}
