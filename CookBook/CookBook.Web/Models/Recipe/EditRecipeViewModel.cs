using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.Models
{
    public class EditRecipeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Max length is 100 symbols")]
        public string Name { get; set; }
                
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "Delete Image")]
        public bool IsNullImage { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [MaxLength(50, ErrorMessage = "Max length is 50 symbols")]
        [Display(Name = "Cooking Time")]
        public string CookingTime { get; set; }

        [MaxLength(50, ErrorMessage = "Max length is 50 symbols")]
        [Display(Name = "Cooking Temperature")]
        public string CookingTemperature { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(200, ErrorMessage = "Max length is 200 symbols")]
        public string Description { get; set; }
        
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}