using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.DAL.Repositories
{
    public interface IUserRepository
    {
        void InsertUser(User user, List<int> selectedUserRoles);
        void UpdateUser(User user, List<int> selectedUserRoles);
        void DeleteUser(int userId);
        User GetUserByLogin(string login);
        User GetUserById(int userId);
        List<User> GetUsers();
        List<UserRole> GetUserRoles();
        List<UserRole> GetUserRolesByUserId(int userId);
    }
}
