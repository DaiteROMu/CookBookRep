using System.Collections.Generic;
using System.Security.Principal;
using CookBook.Common.Models;

namespace CookBook.Web.AuthAttributes
{
    public class UserPrincipal : IPrincipal
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public List<UserRole> UserRoles { get; set; }

        public IIdentity Identity { get; private set; }

        public UserPrincipal(string userName)
        {
            Identity = new GenericIdentity(userName);
        }

        public bool IsInRole(string roleName)
        {            
            return UserRoles.Exists(r => r.Name.ToLower() == roleName.ToLower());
        }
    }
}