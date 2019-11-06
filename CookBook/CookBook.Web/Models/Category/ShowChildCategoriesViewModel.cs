using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.Web.Models
{
    public class ShowChildCategoriesViewModel
    {
        public Category CurrentCategory { get; set; }

        public IEnumerable<Category> ChildCategories { get; set; }
    }
}