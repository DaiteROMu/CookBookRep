using System.Collections.Generic;
using System.Web.Caching;
using CookBook.Common.Models;

namespace CookBook.Business.Providers
{
    public class CacheProvider: ICacheProvider
    {
        private Cache _cache;

        public CacheProvider()
        {
            _cache = new Cache();
        }

        public List<Category> GetCategoriesFromCache(string name)
        {
            return _cache.Get(name) as List<Category>;
        }

        public void InsertIntoCache(string name, List<Category> categories)
        {
            _cache.Insert(name, categories);
        }

        public void DeleteFromCache(string name)
        {
            _cache.Remove(name);
        }

        public bool IsInCache(string name)
        {
            return _cache.Get(name) as List<Category> != null;
        }
    }
}
