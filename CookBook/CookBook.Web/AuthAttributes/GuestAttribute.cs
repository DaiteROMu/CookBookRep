﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CookBook.Web.AuthAttributes
{
    public class GuestAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            UserPrincipal userPrincipal = HttpContext.Current.User as UserPrincipal;
            if (userPrincipal != null)
            {
                if (!userPrincipal.IsInRole("guest"))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Forbidden" }));
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login" }));
            }
        }
    }
}