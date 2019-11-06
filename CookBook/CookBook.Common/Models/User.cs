using System.Collections.Generic;

namespace CookBook.Common.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
