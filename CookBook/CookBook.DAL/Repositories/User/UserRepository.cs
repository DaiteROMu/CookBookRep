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
    public class UserRepository : IUserRepository
    {
        private ILogger _logger;
        private string _connectionString = ConfigurationManager.ConnectionStrings["CookBookDB"].ConnectionString;
        private delegate void _executerVoidCmd(SqlConnection sqlConnection, SqlCommand cmd);
        private delegate List<User> _executerUserCmd(SqlConnection sqlConnection, SqlCommand cmd);
        private delegate List<UserUserRole> _executerUserUserUserRoleCmd(SqlConnection sqlConnection, SqlCommand cmd);
        private delegate List<UserRole> _executerUserRoleCmd(SqlConnection sqlConnection, SqlCommand cmd);

        public UserRepository(ILogger logger)
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

        private List<User> ExecuteUserSqlCmd(SqlConnection sqlConnection, SqlCommand cmd)
        {
            List<User> users = new List<User>();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.UserId = reader.GetFieldValue<int>(0);
                        user.Login = reader.GetFieldValue<string>(1);
                        user.Password = reader.GetFieldValue<string>(2);
                        user.Email = reader.GetFieldValue<string>(3);
                        users.Add(user);
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

            return users;
        }

        private List<UserUserRole> ExecuteUserUserUserRoleSqlCmd(SqlConnection sqlConnection, SqlCommand cmd)
        {
            List<UserUserRole> returnedList = new List<UserUserRole>();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserUserRole userUserRole = new UserUserRole();
                        userUserRole.UserId = reader.GetFieldValue<int>(0);
                        userUserRole.UserRoleId = reader.GetFieldValue<int>(0);
                        returnedList.Add(userUserRole);
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

        private List<UserRole> ExecuteUserRoleSqlCmd(SqlConnection sqlConnection, SqlCommand cmd)
        {
            List<UserRole> userRoles = new List<UserRole>();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserRole userRole = new UserRole()
                        {
                            UserRoleId = reader.GetFieldValue<int>(0),
                            Name = reader.GetFieldValue<string>(1)
                        };
                        userRoles.Add(userRole);
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

            return userRoles;
        }

        public void DeleteUser(int userId)
        {
            if (userId > 0)
            {
                _executerVoidCmd executer = new _executerVoidCmd(ExecuteVoidSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteUser", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        executer(sqlConnection, cmd);
                    }
                }
            }
        }

        public User GetUserById(int userId)
        {
            User user = new User();

            if (userId > 0)
            {
                _executerUserCmd executer = new _executerUserCmd(ExecuteUserSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserById", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        user = executer(sqlConnection, cmd).FirstOrDefault();
                    }
                }
            }

            return user;
        }

        public User GetUserByLogin(string login)
        {
            User user = new User();

            if (!string.IsNullOrEmpty(login))
            {
                _executerUserCmd executer = new _executerUserCmd(ExecuteUserSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserByLogin", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Login", login);
                        user = executer(sqlConnection, cmd).FirstOrDefault();
                        if (user != null)
                        {
                            user.UserRoles = GetUserRolesByUserId(user.UserId);
                        }
                    }
                }
            }

            return user;
        }

        public List<UserRole> GetUserRoles()
        {
            _executerUserRoleCmd executer = new _executerUserRoleCmd(ExecuteUserRoleSqlCmd);
            List<UserRole> userRoles = new List<UserRole>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUserRoles", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    userRoles = executer(sqlConnection, cmd);
                }
                return userRoles;
            }
        }

        public List<User> GetUsers()
        {
            _executerUserCmd executer = new _executerUserCmd(ExecuteUserSqlCmd);
            List<User> users = new List<User>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUsers", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    users = executer(sqlConnection, cmd);
                }
                return users;
            }
        }

        public void InsertUser(User user, List<int> selectedUserRoles)
        {
            if (user != null && selectedUserRoles != null)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    SqlCommand insertUserCmd = new SqlCommand("InsertUser", sqlConnection);
                    insertUserCmd.CommandType = CommandType.StoredProcedure;
                    insertUserCmd.Parameters.AddWithValue("@Login", user.Login);
                    insertUserCmd.Parameters.AddWithValue("@Password", user.Password);
                    insertUserCmd.Parameters.AddWithValue("@Email", user.Email);

                    SqlCommand cmdLastUserId = new SqlCommand("GetLastInsertedUserId", sqlConnection);
                    cmdLastUserId.CommandType = CommandType.StoredProcedure;

                    SqlTransaction transaction = null;

                    try
                    {
                        sqlConnection.Open();
                        transaction = sqlConnection.BeginTransaction();
                        insertUserCmd.Transaction = transaction;
                        cmdLastUserId.Transaction = transaction;
                        insertUserCmd.ExecuteNonQuery();
                        object userIdObj = cmdLastUserId.ExecuteScalar();
                        int userId = int.Parse(userIdObj.ToString());
                        foreach (int i in selectedUserRoles)
                        {
                            SqlCommand cmdInsertUserUserRole = new SqlCommand("InsertUserUserRole", sqlConnection);
                            cmdInsertUserUserRole.CommandType = CommandType.StoredProcedure;
                            cmdInsertUserUserRole.Parameters.AddWithValue("@UserId", userId);
                            cmdInsertUserUserRole.Parameters.AddWithValue("@UserRoleId", i);
                            cmdInsertUserUserRole.Transaction = transaction;
                            cmdInsertUserUserRole.ExecuteNonQuery();
                        }

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

        public void UpdateUser(User user, List<int> selectedUserRoles)
        {
            if (user != null && selectedUserRoles != null)
            {
                List<UserRole> currentUserRoles = GetUserRolesByUserId(user.UserId);

                List<int> userRolesForDelete = new List<int>();
                List<int> userRolesForInsert = new List<int>();

                foreach (int i in selectedUserRoles)
                {
                    if (currentUserRoles.FirstOrDefault(u => u.UserRoleId == i) == null)
                    {
                        userRolesForInsert.Add(i);
                    }
                }

                foreach (UserRole item in currentUserRoles)
                {
                    if (selectedUserRoles.FirstOrDefault(i => i == item.UserRoleId) == 0)
                    {
                        userRolesForDelete.Add(item.UserRoleId);
                    }
                }

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    SqlCommand updateUserCmd = new SqlCommand("UpdateUser", sqlConnection);
                    updateUserCmd.CommandType = CommandType.StoredProcedure;
                    updateUserCmd.Parameters.AddWithValue("@UserId", user.UserId);
                    updateUserCmd.Parameters.AddWithValue("@Login", user.Login);
                    updateUserCmd.Parameters.AddWithValue("@Password", user.Password);
                    updateUserCmd.Parameters.AddWithValue("@Email", user.Email);

                    SqlTransaction transaction = null;

                    try
                    {
                        sqlConnection.Open();
                        transaction = sqlConnection.BeginTransaction();
                        updateUserCmd.Transaction = transaction;
                        updateUserCmd.ExecuteNonQuery();
                        foreach (int i in userRolesForInsert)
                        {
                            SqlCommand cmdInsertUserUserRole = new SqlCommand("InsertUserUserRole", sqlConnection);
                            cmdInsertUserUserRole.CommandType = CommandType.StoredProcedure;
                            cmdInsertUserUserRole.Parameters.AddWithValue("@UserId", user.UserId);
                            cmdInsertUserUserRole.Parameters.AddWithValue("@UserRoleId", i);
                            cmdInsertUserUserRole.Transaction = transaction;
                            cmdInsertUserUserRole.ExecuteNonQuery();
                        }

                        foreach (int i in userRolesForDelete)
                        {
                            SqlCommand cmdDeleteUserUserRole = new SqlCommand("DeleteUserUserRole", sqlConnection);
                            cmdDeleteUserUserRole.CommandType = CommandType.StoredProcedure;
                            cmdDeleteUserUserRole.Parameters.AddWithValue("@UserId", user.UserId);
                            cmdDeleteUserUserRole.Parameters.AddWithValue("@UserRoleId", i);
                            cmdDeleteUserUserRole.Transaction = transaction;
                            cmdDeleteUserUserRole.ExecuteNonQuery();
                        }

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

        public List<UserRole> GetUserRolesByUserId(int userId)
        {
            List<UserRole> userRoles = new List<UserRole>();

            if (userId > 0)
            {
                _executerUserRoleCmd executer = new _executerUserRoleCmd(ExecuteUserRoleSqlCmd);

                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserRolesByUserId", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        userRoles = executer(sqlConnection, cmd);
                    }
                }
            }

            return userRoles;
        }
    }
}
