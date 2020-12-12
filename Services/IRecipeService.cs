using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using recipe_ingredient_checklist_backend.Data.Repositories;
using recipe_ingredient_checklist_backend.Data;

namespace recipe_ingredient_checklist_backend.Services
{
    public interface IRecipeService
    {
        ApplicationUser FindRecipeWithIngredientsByUserId(string userId);
        List<Recipe> FindRecipesByUserId(string userId);
    }
}
