using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.Business.Providers
{
    public interface ICacheProvider
    {
        void InsertIntoCache(string name, List<Category> categories);
        void DeleteFromCache(string name);
        List<Category> GetCategoriesFromCache(string name);
        bool IsInCache(string name);
    }
}
