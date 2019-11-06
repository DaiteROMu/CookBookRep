using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using CookBook.Common.Enums;

namespace CookBook.Web.Models
{
    public class EditCategoryViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50 symbols")]
        public string Name { get; set; }

        [Display(Name = "Parent Category")]
        public int ParentId { get; set; }

        public UniqueValidation Uniqueness { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}