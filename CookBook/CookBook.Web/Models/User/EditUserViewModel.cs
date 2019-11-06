using System.Collections.Generic;
using CookBook.Common.Enums;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.Models
{
    public class EditUserViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Login is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50 symbols")]
        [MinLength(3, ErrorMessage = "Min length is 3 symbols")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[0-9])[0-9a-zA-Z]{5,}$", ErrorMessage = "Wrong password format")]
        [MaxLength(20, ErrorMessage = "Max length is 20 symbols")]
        [MinLength(5, ErrorMessage = "Min length is 5 symbols")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "E-mail is required")]
        [MaxLength(100, ErrorMessage = "Max length is 100 symbols")]
        [Display(Name = "E-mail")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong e-mail format")]
        public string Email { get; set; }
                
        [Display(Name = "User Roles")]
        public List<int> SelectedUserRoles { get; set; }

        public UniqueValidation LoginUniqueness { get; set; }

        public UniqueValidation EmailUniqueness { get; set; }

        public IEnumerable<SelectListItem> UserRoles { get; set; }
    }
}