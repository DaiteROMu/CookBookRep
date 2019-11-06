using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.Models
{
    public class LoginViewModel
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}