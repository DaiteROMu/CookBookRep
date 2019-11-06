using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.Web.Models
{
    public class FullRecipeViewModel
    {
        public RecipeView Recipe { get; set; }
        public RecipeDetails RecipeDetails { get; set; }
        public IEnumerable<RecipeIngridientView> Ingridients { get; set; }
    }
}