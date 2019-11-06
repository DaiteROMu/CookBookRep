using System.Collections.Generic;
using System.Linq;
using CookBook.Common.Models;
using CookBook.Common.Enums;
using CookBook.DAL.Repositories;

namespace CookBook.Business.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;

        public UserProvider(IUserRepository userRepository)
        {
            if(userRepository!=null)
            {
                _userRepository = userRepository;
            }
            else
            {
                RepositoriesFactory factory = new RepositoriesFactory();
                _userRepository = factory.GetUserRepository();
            }
        }

        public void DeleteUser(int userId)
        {
            if (userId > 0)
            {
                _userRepository.DeleteUser(userId);
            }            
        }

        public User GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return null;
            }
            else
            {
                return _userRepository.GetUserByLogin(login);
            }            
        }

        public User GetUserById(int userId)
        {
            if (userId > 0)
            {
                return _userRepository.GetUserById(userId);
            }
            else
            {
                return null;
            }
        }

        public List<UserRole> GetUserRoles()
        {
            return _userRepository.GetUserRoles();
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public void InsertUser(User user, List<int> selectedUserRoles)
        {
            if(user != null && selectedUserRoles != null)
            {
                _userRepository.InsertUser(user, selectedUserRoles);
            }            
        }

        public void UpdateUser(User user, List<int> selectedUserRoles)
        {
            if (user != null && selectedUserRoles != null)
            {
                _userRepository.UpdateUser(user, selectedUserRoles);
            }            
        }

        public UniqueValidation IsUniqueEmail(int userId, string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return UniqueValidation.Error;
            }
            else
            {
                if (userId == 0)
                {
                    User user = GetUsers().FirstOrDefault(u => u.Email == email);
                    if (user == null)
                    {
                        return UniqueValidation.Unique;
                    }
                    else
                    {
                        return UniqueValidation.Dublicate;
                    }
                }
                else
                {
                    User user = GetUsers().FirstOrDefault(u => u.Email == email);
                    if (user == null)
                    {
                        return UniqueValidation.Unique;
                    }
                    else
                    {
                        if (user.UserId == userId)
                        {
                            return UniqueValidation.Unique;
                        }
                        else
                        {
                            return UniqueValidation.Dublicate;
                        }
                    }
                }
            }
        }

        public UniqueValidation IsUniqueLogin(int userId, string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return UniqueValidation.Error;
            }
            else
            {
                if (userId == 0)
                {
                    User user = GetUsers().FirstOrDefault(u => u.Login == login);
                    if (user == null)
                    {
                        return UniqueValidation.Unique;
                    }
                    else
                    {
                        return UniqueValidation.Dublicate;
                    }
                }
                else
                {
                    User user = GetUsers().FirstOrDefault(u => u.Login == login);
                    if (user == null)
                    {
                        return UniqueValidation.Unique;
                    }
                    else
                    {
                        if (user.UserId == userId)
                        {
                            return UniqueValidation.Unique;
                        }
                        else
                        {
                            return UniqueValidation.Dublicate;
                        }
                    }
                }
            }
        }

        public List<UserRole> GetUserRolesByUserId(int userId)
        {
            if (userId > 0)
            {
                return _userRepository.GetUserRolesByUserId(userId);
            }
            else
            {
                return null;
            }
        }
    }
}
