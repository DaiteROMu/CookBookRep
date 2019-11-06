using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.Web.Models
{
    public class RecipeIngridientViewModel
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public IEnumerable<RecipeIngridientView> RecipeIngridients { get; set; }
    }
}