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

        public List<Recipe> FindRecipeWithIngredients(string userId)
        {
            return _context.Recipe.Where(recipe => recipe.ApplicationUserId == userId)
                .Include(recipe => recipe.RecipeIngredients)
                .ThenInclude(recipeIngredients => recipeIngredients.Ingredient)
                .ToList();
        }
    }
}
