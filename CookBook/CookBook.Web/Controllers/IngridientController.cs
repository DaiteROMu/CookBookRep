using System.Collections.Generic;
using System.Web.Mvc;
using CookBook.Web.Models;
using CookBook.Common.Models;
using CookBook.Common.Enums;
using CookBook.Business.Providers;
using CookBook.Web.AuthAttributes;

namespace CookBook.Web.Controllers
{    
    [Editor]
    public class IngridientController : Controller
    {
        private readonly IIngridientProvider _ingridientProvider;

        public IngridientController(IIngridientProvider ingridientProvider)
        {
            if (ingridientProvider != null)
            {
                _ingridientProvider = ingridientProvider;
            }
            else
            {
                ProvidersFactory factory = new ProvidersFactory();
                _ingridientProvider = factory.GetIngridientProvider();
            }
        }

        public ActionResult ShowIngridients()
        {
            IEnumerable<Ingridient> ingridients = _ingridientProvider.GetIngridients();
            IEnumerable<IngridientViewModel> model = ParseIngridientViewModels(ingridients);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ShowIngridients", model);
            }
            else
            {
                return View(model);
            }
        }
                
        [HttpGet]
        public ActionResult AddOrEditIngridient(int ingridientId)
        {
            Ingridient ingridient = _ingridientProvider.GetIngridientById(ingridientId);
            IngridientViewModel model = ParseIngridient(ingridient);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AddOrEditIngridient", model);
            }
            else
            {
                return View(model);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]        
        public ActionResult AddOrEditIngridient(IngridientViewModel ingridientViewModel)
        {
            ingridientViewModel.Uniqueness = _ingridientProvider.IsUnique(ingridientViewModel.Name);
            if(ingridientViewModel.Uniqueness==UniqueValidation.Dublicate)
            {
                ModelState.AddModelError("Name", "This ingridient is already exists");
            }
            if (ingridientViewModel.Uniqueness == UniqueValidation.Error)
            {
                ModelState.AddModelError("Name", "Name is required");
            }

            if (ModelState.IsValid)
            {
                if (ingridientViewModel.IngridientId == 0)
                {
                    Ingridient ingridient = ParseIngridient(ingridientViewModel);
                    _ingridientProvider.InsertInrgridient(ingridient);
                }
                else
                {
                    Ingridient ingridient = ParseIngridient(ingridientViewModel);
                    _ingridientProvider.UpdateIngridient(ingridient);
                }

                return RedirectToAction("ShowIngridients");
            }
            else
            {
                return View("AddOrEditIngridient", ingridientViewModel);                
            }
        }
                
        [HttpGet]
        public ActionResult DeleteIngridient(int ingridientId)
        {            
            _ingridientProvider.DeleteIngridient(ingridientId);

            return RedirectToAction("ShowIngridients");
        }

        #region Helpers
        private IEnumerable<IngridientViewModel> ParseIngridientViewModels(IEnumerable<Ingridient> ingridients)
        {
            List<IngridientViewModel> ingridientViewModels = new List<IngridientViewModel>();
            foreach (Ingridient item in ingridients)
            {
                IngridientViewModel viewModelItem = new IngridientViewModel();
                viewModelItem.IngridientId = item.IngridientId;
                viewModelItem.Name = item.Name;
                ingridientViewModels.Add(viewModelItem);
            }
            return ingridientViewModels;
        }

        private Ingridient ParseIngridient(IngridientViewModel ingridientViewModel)
        {
            Ingridient ingridient = new Ingridient();
            if (ingridientViewModel != null)
            {
                ingridient.IngridientId = ingridientViewModel.IngridientId;
                ingridient.Name = ingridientViewModel.Name;
            }
            return ingridient;
        }

        private IngridientViewModel ParseIngridient(Ingridient ingridient)
        {
            IngridientViewModel ingridientViewModel = new IngridientViewModel();
            if (ingridient != null)
            {
                ingridientViewModel.IngridientId = ingridient.IngridientId;
                ingridientViewModel.Name = ingridient.Name;
                ingridientViewModel.Uniqueness = UniqueValidation.Unique;
            }
            return ingridientViewModel;
        }
        #endregion
    }
}