using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.Business.Providers
{
    public interface IRecipeProvider
    {
        int InsertRecipe(Recipe recipe, RecipeDetails recipeDetails);
        void UpdateRecipe(Recipe recipe, RecipeDetails recipeDetails);
        void UpdateRecipeDetailsSequencing(int recipeId, string sequencing);
        void DeleteRecipe(int recipeId);
        void InsertRecipeIngridient(RecipeIngridient recipeIngridient);
        void UpdateRecipeIngridient(RecipeIngridient recipeIngridient);
        void DeleteRecipeIngridient(int recipeId, int ingridientId);
        Recipe GetRecipeById(int recipeId);
        RecipeDetails GetRecipeDetailsByRecipeId(int recipeId);
        List<RecipeIngridientView> GetRecipeIngridientsByRecipeId(int recipeId);
        RecipeIngridientView GetRecipeIngridient(int recipeId, int ingridientId);
        List<RecipeView> GetRecipes();
        IEnumerable<RecipeView> GetRecipesByCategoryId(int categoryId);
        IEnumerable<RecipeView> GetRecipesByRecipeName(string recipeName);
        IEnumerable<RecipeView> GetRecipesByCategoryName(string categoryName);
        IEnumerable<RecipeView> GetRecipesByIngridientName(string ingridientName);
        bool IsInsertRecipeIngridient(int recipeId, int ingridientId);
    }
}
