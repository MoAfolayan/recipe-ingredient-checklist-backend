using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using recipe_ingredient_checklist_backend.Data;

namespace recipe_ingredient_checklist_backend.Data.Repositories
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RecipeApplicationDbContext context) : base(context)
        {
        }

        public ApplicationUser FindRecipeWithIngredientsByUserId(string userId)
        {
            return _context.ApplicationUser.Where(applicationUser => applicationUser.Id == userId)
                .Include(applicationUser => applicationUser.Recipes)
                .ThenInclude(recipe => recipe.RecipeIngredients)
                .ThenInclude(recipeIngredients => recipeIngredients.Ingredient)
                .FirstOrDefault();
        }

        public Recipe FindRecipeWithIngredientsByRecipeId(int recipeId)
        {
            return _context.Recipe.Where(recipe => recipe.Id == recipeId)
                .Include(recipe => recipe.RecipeIngredients)
                .ThenInclude(recipeIngredients => recipeIngredients.Ingredient)
                .FirstOrDefault();
        }

        public List<Recipe> FindRecipesByUserId(string userId)
        {
            return _context.Recipe.Where(recipe => recipe.ApplicationUserId == userId)
                .ToList();
        }
    }
}
