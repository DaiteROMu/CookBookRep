using System;
using CookBook.Web.AuthAttributes;
using CookBook.Common.Models;
using Newtonsoft.Json;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Routing;
using CookBook.Common.Log;

namespace CookBook.Web
{
    public class MvcApplication : HttpApplication
    {
        private readonly ILogger _logger;

        public MvcApplication()
        {
            LoggerFactory factory = new LoggerFactory();
            _logger = factory.GetLogger();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie auth = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (auth != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(auth.Value);
                User model = JsonConvert.DeserializeObject<User>(ticket.UserData);
                UserPrincipal principal = new UserPrincipal(ticket.Name)
                {
                    UserId = model.UserId,
                    Login = model.Login,
                    UserRoles = model.UserRoles
                };
                HttpContext.Current.User = principal;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            _logger.ErrorMessage(exception);
        }
    }
}