using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace recipe_ingredient_checklist_backend.Data.Repositories
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        List<ApplicationUser> FindRecipeWithIngredientsByUserId(string userId);
    }
}