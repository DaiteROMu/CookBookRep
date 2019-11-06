using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CookBook.Web.Models;
using CookBook.Common.Models;
using CookBook.Business.Providers;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CookBook.Web.AuthAttributes;

namespace CookBook.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeProvider _recipeProvider;
        private readonly ICategoryProvider _categoryProvider;
        private readonly IIngridientProvider _ingridientProvider;

        public RecipeController(IRecipeProvider recipeProvider, ICategoryProvider categoryProvider, IIngridientProvider ingridientProvider)
        {
            ProvidersFactory factory = new ProvidersFactory();

            if (recipeProvider != null)
            {
                _recipeProvider = recipeProvider;
            }
            else
            {
                _recipeProvider = factory.GetRecipeProvider();
            }

            if (categoryProvider != null)
            {
                _categoryProvider = categoryProvider;
            }
            else
            {
                _categoryProvider = factory.GetCategoryProvider();
            }

            if (ingridientProvider != null)
            {
                _ingridientProvider = ingridientProvider;
            }
            else
            {
                _ingridientProvider = factory.GetIngridientProvider();
            }
        }

        [Guest]
        [ChildActionOnly]
        public ActionResult ShowSearchFields()
        {
            return PartialView("_SearchFields");
        }

        [Guest]
        [HttpGet]
        public ActionResult ShowSearchResult(string searchRecipeName, string searchCategoryName, string searchIngridientName)
        {
            SearchViewModel model = new SearchViewModel();
            model.RecipeModel = new ShortRecipeViewModel();
            List<RecipeView> recipes = _recipeProvider.GetRecipes();
            model.Header = "";
            IEnumerable<RecipeView> recipesR = null;
            IEnumerable<RecipeView> recipesC = null;
            IEnumerable<RecipeView> recipesI = null;

            if (!string.IsNullOrEmpty(searchRecipeName))
            {
                recipesR = _recipeProvider.GetRecipesByRecipeName(searchRecipeName);
                model.Header = model.Header + "Searching \"" + searchRecipeName + "\" in Recipes \n";
            }
            if (!string.IsNullOrEmpty(searchCategoryName))
            {
                recipesC = _recipeProvider.GetRecipesByCategoryName(searchCategoryName);
                model.Header = model.Header + "Searching \"" + searchCategoryName + "\" in Categories \n";
            }
            if (!string.IsNullOrEmpty(searchIngridientName))
            {
                recipesI = _recipeProvider.GetRecipesByIngridientName(searchIngridientName);
                model.Header = model.Header + "Searching \"" + searchIngridientName + "\" in Ingridients \n";
            }

            if (recipesR == null && recipesC == null && recipesI == null)
            {
                model.RecipeModel.Recipes = null;
            }
            else
            {
                if (recipesR != null)
                {
                    recipes.RemoveAll(r1 => recipesR.FirstOrDefault(r2 => r2.RecipeId == r1.RecipeId) == null);
                }

                if (recipesC != null)
                {
                    recipes.RemoveAll(r1 => recipesC.FirstOrDefault(r2 => r2.RecipeId == r1.RecipeId) == null);
                }

                if (recipesI != null)
                {
                    recipes.RemoveAll(r1 => recipesI.FirstOrDefault(r2 => r2.RecipeId == r1.RecipeId) == null);
                }

                model.RecipeModel.Recipes = recipes;
            }

            return PartialView("_SearchResult", model);
        }

        [Guest]
        public ActionResult ShowAllRecipes()
        {
            IEnumerable<RecipeView> recipes = _recipeProvider.GetRecipes();
            ShortRecipeViewModel model = ParseShortRecipeViewModels(recipes);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ShowAllRecipes", model);
            }
            else
            {
                return View(model);
            }
        }

        [Guest]
        [HttpGet]
        public FileResult GetRecipeImage(string imageUrl)
        {
            if (imageUrl == null)
            {
                imageUrl = @"E:\GitRepos\CookBookRep\CookBook\CookBook.Web\Content\NoImage.png";
            }
            string mimeType = "image/";
            char[] sep = new char[] { '.' };
            string[] imageArr = imageUrl.Split(sep);
            mimeType += imageArr[imageArr.Length - 1];
            return File(imageUrl, mimeType);
        }

        [Guest]
        [HttpGet]
        public ActionResult ShowRecipesByCategoryId(int categoryId)
        {
            IEnumerable<RecipeView> recipes = _recipeProvider.GetRecipesByCategoryId(categoryId);
            ShortRecipeViewModel model = ParseShortRecipeViewModels(recipes, categoryId);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ShowRecipesByCategoryId", model);
            }
            else
            {
                return View("ShowRecipesByCategoryId", model);
            }
        }

        [Guest]
        [HttpGet]
        public FileResult DownloadRecipe(int recipeId)
        {
            Recipe recipe = _recipeProvider.GetRecipeById(recipeId);
            Category category = _categoryProvider.GetCategoryById(recipe.CategoryId);
            RecipeDetails recipeDetails = _recipeProvider.GetRecipeDetailsByRecipeId(recipeId);
            IEnumerable<RecipeIngridientView> recipeIngridients = _recipeProvider.GetRecipeIngridientsByRecipeId(recipeId);

            string fileName = recipe.Name + ".pdf";
            Document document = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);

            pdfWriter.CloseStream = false;
            document.Open();

            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font fontDefault = new Font(baseFont, 16, Font.NORMAL);
            Font fontMainHeader = new Font(baseFont, 24, Font.BOLD);
            Font fontHeader = new Font(baseFont, 20, Font.NORMAL);

            Paragraph par = new Paragraph(recipe.Name, fontMainHeader);
            par.Alignment = Element.ALIGN_CENTER;
            document.Add(par);

            par = new Paragraph(category.Name, fontHeader);
            par.Alignment = Element.ALIGN_CENTER;
            document.Add(par);

            if (recipeDetails.Description != null && recipeDetails.Description != "")
            {
                par = new Paragraph("Description", fontHeader);
                par.Alignment = Element.ALIGN_CENTER;
                document.Add(par);
                par = new Paragraph(recipeDetails.Description, fontDefault);
                par.Alignment = Element.ALIGN_CENTER;
                document.Add(par);
            }

            Image image;
            if (recipe.ImageUrl != null && recipe.ImageUrl != "")
            {
                image = Image.GetInstance(recipe.ImageUrl);
            }
            else
            {
                image = Image.GetInstance(@"E:\GitRepos\CookBookRep\CookBook\CookBook.Web\Content\NoImage.png");
            }
            image.Alignment = Element.ALIGN_CENTER;
            image.Border = Rectangle.BOX;
            image.BorderColor = BaseColor.BLACK;
            image.BorderWidth = 3f;
            image.ScaleAbsolute(400f, 300f);
            document.Add(image);

            par = new Paragraph("\n");
            document.Add(par);

            PdfPTable table = new PdfPTable(2);

            PdfPCell cell = new PdfPCell(new Phrase("Cooking Time", fontHeader));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Cooking Temperature", fontHeader));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(recipeDetails.CookingTime, fontDefault));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(recipeDetails.CookingTemperature, fontDefault));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            document.Add(table);

            par = new Paragraph("Ingridients", fontHeader);
            par.Alignment = Element.ALIGN_CENTER;
            document.Add(par);

            par = new Paragraph("\n");
            document.Add(par);

            table = new PdfPTable(2);

            cell = new PdfPCell(new Phrase("Ingridient", fontHeader));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Weight", fontHeader));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            foreach (RecipeIngridientView item in recipeIngridients)
            {
                cell = new PdfPCell(new Phrase(item.Name, fontDefault));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.Weight, fontDefault));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
            }

            document.Add(table);

            par = new Paragraph("Sequencing", fontHeader);
            par.Alignment = Element.ALIGN_CENTER;
            document.Add(par);

            par = new Paragraph(recipeDetails.Sequencing, fontDefault);
            par.Alignment = Element.ALIGN_JUSTIFIED;
            document.Add(par);

            document.Close();
            memoryStream.Flush();
            memoryStream.Position = 0;

            return File(memoryStream, "application/pdf", fileName);
        }

        [Guest]
        [HttpGet]
        public ActionResult ShowFullRecipe(int recipeId)
        {
            Recipe recipe = _recipeProvider.GetRecipeById(recipeId);
            FullRecipeViewModel model = ParseRecipe(recipe);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ShowFullRecipe", model);
            }
            else
            {
                return View(model);
            }
        }

        [Editor]
        [HttpGet]
        public ActionResult AddOrEditRecipe(int recipeId)
        {
            Recipe recipe = _recipeProvider.GetRecipeById(recipeId);
            RecipeDetails recipeDetails = _recipeProvider.GetRecipeDetailsByRecipeId(recipeId);
            EditRecipeViewModel model = ParseEditRecipeViewModel(recipe, recipeDetails);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_AddOrEditRecipe", model);
            }
            else
            {
                return View(model);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Editor]
        public ActionResult AddOrEditRecipe(EditRecipeViewModel recipeViewModel)
        {
            if (recipeViewModel != null)
            {
                bool flag = false;
                if (recipeViewModel.ImageFile != null)
                {
                    switch (recipeViewModel.ImageFile.ContentType)
                    {
                        case "image/gif":
                            flag = false;
                            break;
                        case "image/jpeg":
                            flag = false;
                            break;
                        case "image/png":
                            flag = false;
                            break;
                        default:
                            flag = true;
                            break;
                    }
                    if (flag)
                    {
                        ModelState.AddModelError("ImageFile", "This field is for png/gif/jpg image");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                string imagePath = @"D:\Images\";
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                if (recipeViewModel != null)
                {
                    if (recipeViewModel.RecipeId == 0)
                    {
                        if (recipeViewModel.ImageFile != null)
                        {
                            imagePath += recipeViewModel.ImageFile.FileName;
                            recipeViewModel.ImageFile.SaveAs(imagePath);
                            recipeViewModel.ImageUrl = imagePath;
                        }
                    }
                    else
                    {
                        Recipe oldRecipe = _recipeProvider.GetRecipeById(recipeViewModel.RecipeId);

                        if (recipeViewModel.IsNullImage)
                        {
                            recipeViewModel.ImageFile = null;
                            recipeViewModel.ImageUrl = null;
                        }
                        else
                        {
                            if (recipeViewModel.ImageFile != null)
                            {
                                imagePath += recipeViewModel.ImageFile.FileName;
                                if (oldRecipe.ImageUrl != imagePath)
                                {
                                    recipeViewModel.ImageFile.SaveAs(imagePath);
                                    recipeViewModel.ImageUrl = imagePath;
                                }
                                else
                                {
                                    recipeViewModel.ImageUrl = oldRecipe.ImageUrl;
                                }
                            }
                        }
                    }
                }

                int recipeId = 0;
                Recipe recipe = ParseRecipe(recipeViewModel);
                RecipeDetails recipeDetails = ParseRecipeDetails(recipeViewModel);
                if (recipeViewModel.RecipeId == 0)
                {
                    recipeId = _recipeProvider.InsertRecipe(recipe, recipeDetails);
                }
                else
                {
                    _recipeProvider.UpdateRecipe(recipe, recipeDetails);
                    recipeId = recipe.RecipeId;
                }

                if (recipeId != 0)
                {
                    return RedirectToAction("ShowFullRecipe", new { recipeId });
                }
                else
                {
                    return RedirectToAction("ShowAllRecipes");
                }
            }
            else
            {
                Recipe recipe = ParseRecipe(recipeViewModel);
                RecipeDetails recipeDetails = ParseRecipeDetails(recipeViewModel);
                recipeViewModel = ParseEditRecipeViewModel(recipe, recipeDetails);
                return View("AddOrEditRecipe", recipeViewModel);
            }
        }

        [Editor]
        [HttpGet]
        public ActionResult DeleteRecipe(int recipeId)
        {
            int categoryId = _recipeProvider.GetRecipeById(recipeId).CategoryId;
            _recipeProvider.DeleteRecipe(recipeId);

            return RedirectToAction("ShowRecipesByCategoryId", new { categoryId });
        }

        [Editor]
        [HttpGet]
        public ActionResult ShowIngridientsByRecipeId(int recipeId)
        {
            Recipe recipe = _recipeProvider.GetRecipeById(recipeId);
            IEnumerable<RecipeIngridientView> recipeIngridients = _recipeProvider.GetRecipeIngridientsByRecipeId(recipeId);
            RecipeIngridientViewModel model = ParseRecipeIngridients(recipeIngridients, recipeId, recipe.Name);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ShowIngridientsByRecipeId", model);
            }
            else
            {
                return View("ShowIngridientsByRecipeId", model);
            }
        }

        [Editor]
        [HttpGet]
        public ActionResult AddOrEditRecipeIngridient(int recipeId, int ingridientId)
        {
            RecipeIngridientView recipeIngridient = null;

            if (ingridientId != 0)
            {
                recipeIngridient = _recipeProvider.GetRecipeIngridient(recipeId, ingridientId);
            }

            EditRecipeIngridientViewModel model = ParseRecipeIngridient(recipeIngridient, recipeId);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AddOrEditRecipeIngridient", model);
            }
            else
            {
                return View("AddOrEditRecipeIngridient", model);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Editor]
        public ActionResult AddOrEditRecipeIngridient(EditRecipeIngridientViewModel editRecipeIngridientViewModel)
        {
            RecipeIngridient recipeIngridient = ParseRecipeIngridient(editRecipeIngridientViewModel);

            if (ModelState.IsValid)
            {
                if (_recipeProvider.IsInsertRecipeIngridient(editRecipeIngridientViewModel.RecipeId, editRecipeIngridientViewModel.IngridientId))
                {
                    _recipeProvider.InsertRecipeIngridient(recipeIngridient);
                }
                else
                {
                    _recipeProvider.UpdateRecipeIngridient(recipeIngridient);
                }

                return RedirectToAction("ShowIngridientsByRecipeId", new { @recipeId = editRecipeIngridientViewModel.RecipeId });
            }
            else
            {
                editRecipeIngridientViewModel = ParseRecipeIngridient(recipeIngridient, recipeIngridient.RecipeId);
                return View("AddOrEditRecipeIngridient", editRecipeIngridientViewModel);
            }
        }

        [Editor]
        [HttpGet]
        public ActionResult DeleteRecipeIngridient(int recipeId, int ingridientId)
        {
            _recipeProvider.DeleteRecipeIngridient(recipeId, ingridientId);
            return RedirectToAction("ShowIngridientsByRecipeId", new { recipeId });
        }

        [Editor]
        [HttpGet]
        public ActionResult ShowSequencingByRecipeId(int recipeId)
        {
            IEnumerable<RecipeIngridientView> recipeIngridients = _recipeProvider.GetRecipeIngridientsByRecipeId(recipeId);
            Recipe recipe = _recipeProvider.GetRecipeById(recipeId);
            RecipeDetails recipeDetails = _recipeProvider.GetRecipeDetailsByRecipeId(recipeId);
            EditSequencingViewModel model = ParseEditSequencingViewModel(recipeIngridients, recipeId, recipe.Name, recipeDetails.Sequencing);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ShowSequencingByRecipeId", model);
            }
            else
            {
                return View("ShowSequencingByRecipeId", model);
            }
        }

        [Editor]
        [HttpPost]
        public ActionResult AddOrEditSequencing(EditSequencingViewModel editSequencingViewModel)
        {
            Recipe recipe = _recipeProvider.GetRecipeById(editSequencingViewModel.RecipeId);
            IEnumerable<RecipeIngridientView> recipeIngridients = _recipeProvider.GetRecipeIngridientsByRecipeId(editSequencingViewModel.RecipeId);
            
            if (ModelState.IsValid)
            {
                _recipeProvider.UpdateRecipeDetailsSequencing(editSequencingViewModel.RecipeId, editSequencingViewModel.Sequencing);
                return RedirectToAction("ShowFullRecipe", new { @recipeId = editSequencingViewModel.RecipeId });
            }
            else
            {
                editSequencingViewModel = ParseEditSequencingViewModel(recipeIngridients, editSequencingViewModel.RecipeId, recipe.Name, editSequencingViewModel.Sequencing);
                return View("ShowSequencingByRecipeId", editSequencingViewModel);
            }
        }

        #region Helpers

        private ShortRecipeViewModel ParseShortRecipeViewModels(IEnumerable<RecipeView> recipes)
        {
            ShortRecipeViewModel shortRecipeViewModels = new ShortRecipeViewModel();
            shortRecipeViewModels.Recipes = recipes;
            return shortRecipeViewModels;
        }

        private ShortRecipeViewModel ParseShortRecipeViewModels(IEnumerable<RecipeView> recipes, int categoryId)
        {
            ShortRecipeViewModel shortRecipeViewModels = new ShortRecipeViewModel();
            shortRecipeViewModels.Recipes = recipes;
            shortRecipeViewModels.Category = _categoryProvider.GetCategoryById(categoryId);
            return shortRecipeViewModels;
        }

        private FullRecipeViewModel ParseRecipe(Recipe recipe)
        {
            FullRecipeViewModel fullRecipeViewModel = new FullRecipeViewModel();
            fullRecipeViewModel.RecipeDetails = _recipeProvider.GetRecipeDetailsByRecipeId(recipe.RecipeId);
            fullRecipeViewModel.Ingridients = _recipeProvider.GetRecipeIngridientsByRecipeId(recipe.RecipeId);
            fullRecipeViewModel.Recipe = new RecipeView();
            fullRecipeViewModel.Recipe.RecipeId = recipe.RecipeId;
            fullRecipeViewModel.Recipe.Name = recipe.Name;
            fullRecipeViewModel.Recipe.ImageUrl = recipe.ImageUrl;
            fullRecipeViewModel.Recipe.CategoryId = recipe.CategoryId;
            fullRecipeViewModel.Recipe.CategoryName = _categoryProvider.GetCategoryById(recipe.CategoryId).Name;
            return fullRecipeViewModel;
        }

        private EditRecipeViewModel ParseEditRecipeViewModel(Recipe recipe, RecipeDetails recipeDetails)
        {
            EditRecipeViewModel returnedModel = new EditRecipeViewModel();
            IEnumerable<Category> categories = _categoryProvider.GetCategories();

            if (recipe != null)
            {
                returnedModel.RecipeId = recipe.RecipeId;
                returnedModel.Name = recipe.Name;
                returnedModel.ImageUrl = recipe.ImageUrl;
                returnedModel.CategoryId = recipe.CategoryId;
                returnedModel.Categories = new SelectList(categories, "CategoryId", "Name", returnedModel.CategoryId);
            }
            else
            {
                returnedModel.Categories = new SelectList(categories, "CategoryId", "Name");
            }

            if (recipeDetails != null)
            {
                returnedModel.CookingTime = recipeDetails.CookingTime;
                returnedModel.CookingTemperature = recipeDetails.CookingTemperature;
                returnedModel.Description = recipeDetails.Description;                
            }

            return returnedModel;
        }

        private Recipe ParseRecipe(EditRecipeViewModel editRecipeViewModel)
        {
            Recipe recipe = new Recipe();
            if (editRecipeViewModel != null)
            {
                recipe.RecipeId = editRecipeViewModel.RecipeId;
                recipe.Name = editRecipeViewModel.Name;
                recipe.ImageUrl = editRecipeViewModel.ImageUrl;
                recipe.CategoryId = editRecipeViewModel.CategoryId;
            }
            return recipe;
        }

        private RecipeDetails ParseRecipeDetails(EditRecipeViewModel editRecipeViewModel)
        {
            RecipeDetails recipeDetails = new RecipeDetails();
            if (editRecipeViewModel != null)
            {
                recipeDetails.RecipeId = editRecipeViewModel.RecipeId;
                recipeDetails.CookingTemperature = editRecipeViewModel.CookingTemperature;
                recipeDetails.CookingTime = editRecipeViewModel.CookingTime;
                recipeDetails.Description = editRecipeViewModel.Description;
                recipeDetails.Sequencing = null;
            }
            return recipeDetails;
        }

        private RecipeIngridient ParseRecipeIngridient(EditRecipeIngridientViewModel editRecipeIngridientViewModel)
        {
            RecipeIngridient recipeIngridient = new RecipeIngridient();
            if (editRecipeIngridientViewModel != null)
            {
                recipeIngridient.IngridientId = editRecipeIngridientViewModel.IngridientId;
                recipeIngridient.RecipeId = editRecipeIngridientViewModel.RecipeId;
                recipeIngridient.Weight = editRecipeIngridientViewModel.Weight;
            }
            return recipeIngridient;
        }

        private EditRecipeIngridientViewModel ParseRecipeIngridient(RecipeIngridientView recipeIngridient, int recipeId)
        {
            EditRecipeIngridientViewModel editRecipeIngridientViewModel = new EditRecipeIngridientViewModel();
            List<Ingridient> ingridients = _ingridientProvider.GetIngridients();
            List<RecipeIngridientView> recipeIngridients = _recipeProvider.GetRecipeIngridientsByRecipeId(recipeId);
            editRecipeIngridientViewModel.RecipeId = recipeId;

            if (recipeIngridient != null)
            {
                List<Ingridient> ingridientsOne = new List<Ingridient>();
                Ingridient ingridient = _ingridientProvider.GetIngridientById(recipeIngridient.IngridientId);
                ingridientsOne.Add(ingridient);
                editRecipeIngridientViewModel.IngridientId = recipeIngridient.IngridientId;
                editRecipeIngridientViewModel.Weight = recipeIngridient.Weight;
                editRecipeIngridientViewModel.Ingridients = new SelectList(ingridientsOne, "IngridientId", "Name", recipeIngridient.IngridientId);
            }
            else
            {
                if (recipeIngridients != null)
                {
                    foreach (RecipeIngridientView item in recipeIngridients)
                    {
                        Ingridient ingridient = ingridients.FirstOrDefault(i => i.IngridientId == item.IngridientId);
                        if (ingridient != null)
                        {
                            ingridients.Remove(ingridient);
                        }
                    }
                }

                editRecipeIngridientViewModel.Ingridients = new SelectList(ingridients, "IngridientId", "Name");
            }

            return editRecipeIngridientViewModel;
        }

        private EditRecipeIngridientViewModel ParseRecipeIngridient(RecipeIngridient recipeIngridient, int recipeId)
        {
            EditRecipeIngridientViewModel editRecipeIngridientViewModel = new EditRecipeIngridientViewModel();
            List<Ingridient> ingridients = _ingridientProvider.GetIngridients();
            List<RecipeIngridientView> recipeIngridients = _recipeProvider.GetRecipeIngridientsByRecipeId(recipeId);
            editRecipeIngridientViewModel.RecipeId = recipeId;            
            if (recipeIngridients != null)
            {
                foreach (RecipeIngridientView item in recipeIngridients)
                {
                    Ingridient ingridient = ingridients.FirstOrDefault(i => i.IngridientId == item.IngridientId);
                    if (ingridient != null)
                    {
                        ingridients.Remove(ingridient);
                    }
                }
            }

            if (recipeIngridient != null)
            {
                editRecipeIngridientViewModel.IngridientId = recipeIngridient.IngridientId;
                editRecipeIngridientViewModel.Weight = recipeIngridient.Weight;
                editRecipeIngridientViewModel.Ingridients = new SelectList(ingridients, "IngridientId", "Name", recipeIngridient.IngridientId);
            }
            else
            {
                editRecipeIngridientViewModel.Ingridients = new SelectList(ingridients, "IngridientId", "Name");
            }

            return editRecipeIngridientViewModel;
        }

        private RecipeIngridientViewModel ParseRecipeIngridients(IEnumerable<RecipeIngridientView> recipeIngridients, int recipeId, string recipeName)
        {
            RecipeIngridientViewModel recipeIngridientsViewModel = new RecipeIngridientViewModel();
            recipeIngridientsViewModel.RecipeIngridients = recipeIngridients;
            recipeIngridientsViewModel.RecipeId = recipeId;
            recipeIngridientsViewModel.RecipeName = recipeName;

            return recipeIngridientsViewModel;
        }

        private EditSequencingViewModel ParseEditSequencingViewModel(IEnumerable<RecipeIngridientView> recipeIngridients, int recipeId, string recipeName, string sequencing)
        {
            EditSequencingViewModel editSequencingViewModel = new EditSequencingViewModel();
            editSequencingViewModel.RecipeId = recipeId;
            editSequencingViewModel.RecipeName = recipeName;
            editSequencingViewModel.RecipeIngridients = recipeIngridients;
            editSequencingViewModel.Sequencing = sequencing;

            return editSequencingViewModel;
        }

        #endregion
    }
}