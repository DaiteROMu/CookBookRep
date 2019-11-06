using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CookBook.Web.Models;
using CookBook.Common.Models;
using CookBook.Business.Providers;
using CookBook.Common.Enums;
using CookBook.Web.AuthAttributes;

namespace CookBook.Web.Controllers
{    
    public class CategoryController : Controller
    {
        private readonly ICategoryProvider _categoryProvider;
        
        public CategoryController(ICategoryProvider categoryProvider)
        {
            if(categoryProvider!=null)
            {
                _categoryProvider = categoryProvider;
            }
            else
            {
                ProvidersFactory factory = new ProvidersFactory();
                _categoryProvider = factory.GetCategoryProvider();
            }
        }

        [Editor]        
        public ActionResult ShowCategories()
        {
            IEnumerable<Category> categories = _categoryProvider.GetCategories();
            IEnumerable<ShortCategoryViewModel> model = ParseCategoryViewModels(categories);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ShowCategories", model);
            }
            else
            {
                return View(model);
            }
        }
        
        [Guest]
        [ChildActionOnly]
        public ActionResult ShowTopCategories()
        {
            IEnumerable<Category> topCategories = _categoryProvider.GetTopCategories();
            IEnumerable<ShortCategoryViewModel> model = ParseCategoryViewModels(topCategories);

            return PartialView("_ShowTopCategories", model);
        }

        [Guest]
        [ChildActionOnly]
        public ActionResult ShowChildCategories(int categoryId)
        {
            IEnumerable<Category> childCategories = _categoryProvider.GetChildCategoriesById(categoryId);
            ShowChildCategoriesViewModel model = ParseShowChildCategoriesViewModel(childCategories, categoryId);

            return PartialView("_ShowChildCategories", model);
        }
                
        [Editor]        
        [HttpGet]
        public ActionResult AddOrEditCategory(int categoryId)
        {
            Category category = _categoryProvider.GetCategoryById(categoryId);
            EditCategoryViewModel model = ParseCategory(category);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_AddOrEditCategory", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Editor]        
        public ActionResult AddOrEditCategory(EditCategoryViewModel categoryViewModel)
        {
            categoryViewModel.Uniqueness = _categoryProvider.IsUnique(categoryViewModel.CategoryId, categoryViewModel.Name);
            if (categoryViewModel.Uniqueness == UniqueValidation.Dublicate)
            {
                ModelState.AddModelError("Name", "This category is already exists");
            }
            if (categoryViewModel.Uniqueness == UniqueValidation.Error)
            {
                ModelState.AddModelError("Name", "Name is required");
            }

            Category category = ParseCategory(categoryViewModel);

            if (ModelState.IsValid)
            {
                if (categoryViewModel.CategoryId == 0)
                {                    
                    _categoryProvider.InsertCategory(category);
                }
                else
                {                    
                    _categoryProvider.UpdateCategory(category);
                }       
                
                return RedirectToAction("ShowCategories");
            }
            else
            {                
                categoryViewModel = ParseCategory(category);
                return View("AddOrEditCategory", categoryViewModel);                
            }
        }

        [Editor]        
        [HttpGet]
        public ActionResult DeleteCategory(int categoryId)
        {            
            _categoryProvider.DeleteCategory(categoryId);

            return RedirectToAction("ShowCategories");
        }

        #region Helpers
        private ShowChildCategoriesViewModel ParseShowChildCategoriesViewModel(IEnumerable<Category> categories, int categoryId)
        {
            ShowChildCategoriesViewModel returnedModel = new ShowChildCategoriesViewModel();
            returnedModel.ChildCategories = categories;
            returnedModel.CurrentCategory = _categoryProvider.GetCategoryById(categoryId);
            return returnedModel;
        }

        private IEnumerable<ShortCategoryViewModel> ParseCategoryViewModels(IEnumerable<Category> categories)
        {
            List<ShortCategoryViewModel> categoryViewModels = new List<ShortCategoryViewModel>();
            foreach(Category item in categories)
            {
                ShortCategoryViewModel viewModelItem = new ShortCategoryViewModel();
                viewModelItem.CategoryId = item.CategoryId;
                viewModelItem.Name = item.Name;                
                Category cry = _categoryProvider.GetCategoryById(item.ParentId);
                viewModelItem.ParentName = cry!=null ? cry.Name : "";
                categoryViewModels.Add(viewModelItem);
            }
            return categoryViewModels;
        }

        private Category ParseCategory(EditCategoryViewModel categoryViewModel)
        {
            Category category = new Category();
            if(categoryViewModel!=null)
            {
                category.CategoryId = categoryViewModel.CategoryId;
                category.Name = categoryViewModel.Name;
                category.ParentId = categoryViewModel.ParentId;
            }            
            return category;
        }

        private EditCategoryViewModel ParseCategory(Category category)
        {
            EditCategoryViewModel categoryViewModel = new EditCategoryViewModel();
            IEnumerable<Category> categories = _categoryProvider.GetCategories();
            List<Category> categoriesList = categories.ToList();
            Category newCategory = new Category { CategoryId = 0, Name = "NULL", ParentId = 0 };
            categoriesList.Add(newCategory);            
            if (category!=null)
            {
                categoryViewModel.CategoryId = category.CategoryId;
                categoryViewModel.Name = category.Name;
                Category parentCategory = _categoryProvider.GetCategoryById(category.ParentId);
                categoryViewModel.ParentId = parentCategory != null ? parentCategory.CategoryId : 0;
            }
            categoryViewModel.Uniqueness = UniqueValidation.Unique;
            categoryViewModel.Categories = new SelectList(categoriesList, "CategoryId", "Name", categoryViewModel.ParentId);            

            return categoryViewModel;
        }
        #endregion
    }
}