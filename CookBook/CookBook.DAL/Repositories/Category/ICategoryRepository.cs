using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.DAL.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        List<Category> GetTopCategories();
        List<Category> GetChildCategoriesById(int categoryId);
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
        Category GetCategoryById(int categoryId);
    }
}
