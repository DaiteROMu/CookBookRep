using System.Collections.Generic;
using CookBook.Common.Models;
using CookBook.Common.Enums;

namespace CookBook.Business.Providers
{
    public interface ICategoryProvider
    {        
        List<Category> GetCategories();
        List<Category> GetTopCategories();
        List<Category> GetChildCategoriesById(int categoryId);
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
        Category GetCategoryById(int categoryId);
        UniqueValidation IsUnique(int categoryId, string categoryName);
    }
}
