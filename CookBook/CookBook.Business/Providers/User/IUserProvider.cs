using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Common.Models;
using CookBook.Common.Enums;

namespace CookBook.Business.Providers
{
    public interface IUserProvider
    {
        void InsertUser(User user, List<int> selectedUserRoles);
        void UpdateUser(User user, List<int> selectedUserRoles);
        void DeleteUser(int userId);
        User GetUserByLogin(string login);
        User GetUserById(int userId);
        List<User> GetUsers();
        List<UserRole> GetUserRoles();
        List<UserRole> GetUserRolesByUserId(int userId);
        UniqueValidation IsUniqueEmail(int userId, string email);
        UniqueValidation IsUniqueLogin(int userId, string login);
    }
}
