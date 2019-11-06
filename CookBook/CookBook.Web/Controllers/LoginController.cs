using System;
using System.Web.Mvc;
using CookBook.Common.Enums;
using CookBook.Common.Log;
using CookBook.Web.Models;
using CookBook.Business.Services;

namespace CookBook.Web.Controllers
{    
    public class LoginController : Controller
    {
        private readonly ILogger _logger;
        private readonly IAuthService _authService;

        public LoginController(ILogger logger, IAuthService authService)
        {
            if(logger!=null)
            {
                _logger = logger;
            }
            else
            {
                LoggerFactory factory = new LoggerFactory();
                _logger = factory.GetLogger();
            }

            if (authService != null)
            {
                _authService = authService;
            }
            else
            {
                ServicesFactory factory = new ServicesFactory();
                _authService = factory.GetAuthService();
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]        
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            LoginResult result;
            try
            {
                result = _authService.Login(loginViewModel.Login, loginViewModel.Password);
                switch (result)
                {
                    case LoginResult.NoError:
                        return RedirectToAction("ShowAllRecipes", "Recipe");
                    case LoginResult.EmptyCredentials:
                        ModelState.AddModelError("Login", "This field can't be empty");
                        ModelState.AddModelError("Password", "This field can't be empty");
                        break;
                    case LoginResult.InvalidLogin:
                        ModelState.AddModelError("Login", "There is no login in system");
                        break;
                    case LoginResult.InvalidPassword:
                        ModelState.AddModelError("Password", "Wrong password");
                        break;
                    default:
                        return View("Login");
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorMessage(ex);
                return View("Login");
            }

            return View("Login");
        }
                
        public ActionResult Logout()
        {
            _authService.Logout();
            return RedirectToAction("Login", "Login");
        }
    }
}