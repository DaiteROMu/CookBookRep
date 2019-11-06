using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.Models
{
    public class EditRecipeIngridientViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Ingridient is required")]
        [Display(Name = "Ingridient")]
        public int IngridientId { get; set; }

        [MaxLength(100, ErrorMessage = "Max length is 100 symbols")]
        [Required(ErrorMessage = "Weight is required")]
        public string Weight { get; set; }
        
        public IEnumerable<SelectListItem> Ingridients { get; set; }
    }
}