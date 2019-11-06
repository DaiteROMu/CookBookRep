using System.Linq;
using System.Collections.Generic;
using CookBook.Common.Models;
using CookBook.DAL.Repositories;
using CookBook.Common.Enums;

namespace CookBook.Business.Providers
{
    public class CategoryProvider : ICategoryProvider
    {
        private const string KEYINCACHE = "TopCategories";

        private readonly ICategoryRepository _categoryRepository;
        private readonly ICacheProvider _cacheProvider;

        public CategoryProvider(ICategoryRepository categoryRepository, ICacheProvider cacheProvider)
        {
            if (categoryRepository != null)
            {
                _categoryRepository = categoryRepository;
            }
            else
            {
                RepositoriesFactory factory = new RepositoriesFactory();
                _categoryRepository = factory.GetCategoryRepository();
            }

            if(cacheProvider!=null)
            {
                _cacheProvider = cacheProvider;
            }
            else
            {
                ProvidersFactory factory = new ProvidersFactory();
                _cacheProvider = factory.GetCacheProvider();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            if(categoryId > 0)
            {                
                Category category = GetCategoryById(categoryId);
                _categoryRepository.DeleteCategory(categoryId);

                if (category.ParentId == 0)
                {
                    _cacheProvider.DeleteFromCache(KEYINCACHE);
                    List<Category> categories = GetTopCategories();
                    _cacheProvider.InsertIntoCache(KEYINCACHE, categories);
                }
            }            
        }

        public List<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public List<Category> GetTopCategories()
        {
            if (_cacheProvider.IsInCache(KEYINCACHE))
            {
                return _cacheProvider.GetCategoriesFromCache(KEYINCACHE);
            }

            return _categoryRepository.GetTopCategories();
        }

        public void UpdateCategory(Category category)
        {
            if(category!=null)
            {
                Category oldCategory = GetCategoryById(category.CategoryId);
                _categoryRepository.UpdateCategory(category);

                if (oldCategory.ParentId == 0 && category.ParentId != 0)
                {
                    _cacheProvider.DeleteFromCache(KEYINCACHE);
                    List<Category> categories = GetTopCategories();
                    _cacheProvider.InsertIntoCache(KEYINCACHE, categories);
                }

                if (category.ParentId == 0)
                {
                    _cacheProvider.DeleteFromCache(KEYINCACHE);
                    List<Category> categories = GetTopCategories();
                    _cacheProvider.InsertIntoCache(KEYINCACHE, categories);
                }
            }            
        }

        public void InsertCategory(Category category)
        {
            if (category != null)
            {                
                _categoryRepository.InsertCategory(category);

                if (category.ParentId == 0)
                {
                    _cacheProvider.DeleteFromCache(KEYINCACHE);
                    List<Category> categories = GetTopCategories();
                    _cacheProvider.InsertIntoCache(KEYINCACHE, categories);
                }
            }            
        }

        public Category GetCategoryById(int categoryId)
        {
            if(categoryId>0)
            {
                return _categoryRepository.GetCategoryById(categoryId);
            }
            else
            {
                return null;
            }
        }
        public List<Category> GetChildCategoriesById(int categoryId)
        {
            if (categoryId > 0)
            {
                return _categoryRepository.GetChildCategoriesById(categoryId);
            }
            else
            {
                return null;
            }
        }

        public UniqueValidation IsUnique(int categoryId, string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return UniqueValidation.Error;
            }
            else
            {
                if (categoryId == 0)
                {
                    List<Category> categories = GetCategories();
                    Category checkedCategory = categories.FirstOrDefault(c => c.Name == categoryName);
                    if (checkedCategory == null)
                    {
                        return UniqueValidation.Unique;
                    }
                    else
                    {
                        return UniqueValidation.Dublicate;
                    }
                }
                else
                {
                    List<Category> categories = GetCategories();
                    Category checkedCategory = categories.FirstOrDefault(c => c.Name == categoryName);
                    if (checkedCategory == null)
                    {
                        return UniqueValidation.Dublicate;
                    }
                    else
                    {
                        if (checkedCategory.CategoryId == categoryId)
                        {
                            return UniqueValidation.Unique;
                        }
                        else
                        {
                            return UniqueValidation.Dublicate;
                        }
                    }
                }
            }            
        }
    }
}
