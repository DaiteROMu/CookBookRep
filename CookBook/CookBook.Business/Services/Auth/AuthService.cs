using System;
using Newtonsoft.Json;
using System.Web;
using System.Web.Security;
using CookBook.Common.Models;
using CookBook.Business.Providers;
using CookBook.Common.Enums;

namespace CookBook.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserProvider _userProvider;

        public AuthService(IUserProvider userProvider)
        {
            if(userProvider!=null)
            {
                _userProvider = userProvider;
            }
            else
            {
                ProvidersFactory factory = new ProvidersFactory();
                _userProvider = factory.GetUserProvider();
            }
        }

        public LoginResult Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return LoginResult.EmptyCredentials;
            }

            User user = _userProvider.GetUserByLogin(login);

            if (user == null)
            {
                return LoginResult.InvalidLogin;
            }
            else
            {
                if (password != user.Password)
                {
                    return LoginResult.InvalidPassword;
                }

                string userData = JsonConvert.SerializeObject(user);
                FormsAuthenticationTicket authenticationTicket = new FormsAuthenticationTicket(2, login, DateTime.Now, DateTime.Now.AddHours(1), false, userData);
                string eTicket = FormsAuthentication.Encrypt(authenticationTicket);
                HttpCookie httpAuthCookie = new HttpCookie(FormsAuthentication.FormsCookieName, eTicket);
                HttpContext.Current.Response.Cookies.Add(httpAuthCookie);
                return LoginResult.NoError;
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
