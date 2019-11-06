using CookBook.Common.Enums;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.Models
{
    public class IngridientViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int IngridientId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Max length is 100 symbols")]
        public string Name { get; set; }

        public UniqueValidation Uniqueness { get; set; }
    }
}