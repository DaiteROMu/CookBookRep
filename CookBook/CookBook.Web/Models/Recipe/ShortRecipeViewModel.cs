using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.Web.Models
{
    public class ShortRecipeViewModel
    {
        public IEnumerable<RecipeView> Recipes { get; set; }      
        public Category Category { get; set; }
    }
}