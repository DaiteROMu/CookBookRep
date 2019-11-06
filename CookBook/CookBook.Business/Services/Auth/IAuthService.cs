using CookBook.Common.Enums;

namespace CookBook.Business.Services
{
    public interface IAuthService
    {
        LoginResult Login(string login, string password);
        void Logout();
    }
}
