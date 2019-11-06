using System.Collections.Generic;
using CookBook.Common.Models;
using CookBook.DAL.Repositories;

namespace CookBook.Business.Providers
{
    public class RecipeProvider : IRecipeProvider
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeProvider(IRecipeRepository recipeRepository)
        {
            if (recipeRepository != null)
            {
                _recipeRepository = recipeRepository;
            }
            else
            {
                RepositoriesFactory factory = new RepositoriesFactory();
                _recipeRepository = factory.GetRecipeRepository();
            }
        }

        public void DeleteRecipe(int recipeId)
        {
            if (recipeId > 0)
            {
                _recipeRepository.DeleteRecipe(recipeId);
            }            
        }

        public Recipe GetRecipeById(int recipeId)
        {
            if (recipeId > 0)
            {
                return _recipeRepository.GetRecipeById(recipeId);
            }
            else
            {
                return null;
            }
        }

        public RecipeDetails GetRecipeDetailsByRecipeId(int recipeId)
        {
            if (recipeId > 0)
            {
                return _recipeRepository.GetRecipeDetailsByRecipeId(recipeId);
            }
            else
            {
                return null;
            }
        }

        public List<RecipeIngridientView> GetRecipeIngridientsByRecipeId(int recipeId)
        {
            if (recipeId > 0)
            {
                return _recipeRepository.GetRecipeIngridientsByRecipeId(recipeId);
            }
            else
            {
                return null;
            }
        }

        public List<RecipeView> GetRecipes()
        {
            return _recipeRepository.GetRecipes();
        }

        public IEnumerable<RecipeView> GetRecipesByCategoryId(int categoryId)
        {
            if (categoryId > 0)
            {
                return _recipeRepository.GetRecipesByCategoryId(categoryId);
            }
            else
            {
                return null;
            }
        }

        public int InsertRecipe(Recipe recipe, RecipeDetails recipeDetails)
        {
            if(recipe!=null && recipeDetails != null)
            {
                return _recipeRepository.InsertRecipe(recipe, recipeDetails);
            }
            else
            {
                return 0;
            }
        }

        public void UpdateRecipe(Recipe recipe, RecipeDetails recipeDetails)
        {
            if (recipe != null && recipeDetails != null)
            {
                _recipeRepository.UpdateRecipe(recipe, recipeDetails);
            }                
        }

        public IEnumerable<RecipeView> GetRecipesByRecipeName(string recipeName)
        {
            if (string.IsNullOrEmpty(recipeName))
            {
                return null;
            }
            else
            {
                return _recipeRepository.GetRecipesByRecipeName(recipeName);
            }            
        }

        public IEnumerable<RecipeView> GetRecipesByCategoryName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return null;
            }
            else
            {
                return _recipeRepository.GetRecipesByCategoryName(categoryName);
            }            
        }

        public IEnumerable<RecipeView> GetRecipesByIngridientName(string ingridientName)
        {
            if (string.IsNullOrEmpty(ingridientName))
            {
                return null;
            }
            else
            {
                return _recipeRepository.GetRecipesByIngridientName(ingridientName);
            }
        }

        public void InsertRecipeIngridient(RecipeIngridient recipeIngridient)
        {
            if (recipeIngridient != null)
            {
                _recipeRepository.InsertRecipeIngridient(recipeIngridient);
            }            
        }

        public void UpdateRecipeIngridient(RecipeIngridient recipeIngridient)
        {
            if (recipeIngridient != null)
            {
                _recipeRepository.UpdateRecipeIngridient(recipeIngridient);
            }            
        }

        public void UpdateRecipeDetailsSequencing(int recipeId, string sequencing)
        {
            if (recipeId > 0)
            {
                _recipeRepository.UpdateRecipeDetailsSequencing(recipeId, sequencing);
            }
        }

        public void DeleteRecipeIngridient(int recipeId, int ingridientId)
        {
            if(recipeId>0 && ingridientId > 0)
            {
                _recipeRepository.DeleteRecipeIngridient(recipeId, ingridientId);
            }            
        }

        public RecipeIngridientView GetRecipeIngridient(int recipeId, int ingridientId)
        {
            if(recipeId>0 && ingridientId > 0)
            {
                return _recipeRepository.GetRecipeIngridient(recipeId, ingridientId);
            }
            else
            {
                return null;
            }
        }

        public bool IsInsertRecipeIngridient(int recipeId, int ingridientId)
        {
            RecipeIngridientView recipeIngridient = GetRecipeIngridient(recipeId, ingridientId);
            return recipeIngridient == null;
        }
    }
}
