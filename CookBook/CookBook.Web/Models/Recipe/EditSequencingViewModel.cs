using System.Collections.Generic;
using CookBook.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.Models
{
    public class EditSequencingViewModel
    {
        public int RecipeId { get; set; }

        public string RecipeName { get; set; }

        [DataType(DataType.MultilineText)]        
        public string Sequencing { get; set; }

        public IEnumerable<RecipeIngridientView> RecipeIngridients { get; set; }
    }
}