using System;
using System.Collections.Generic;
using System.Linq;
using recipe_ingredient_checklist_backend.Data;

namespace recipe_ingredient_checklist_backend.Data.Repositories
{
    public class IngredientRepository : Repository<Ingredient>
    {
        public IngredientRepository(RecipeApplicationDbContext context) : base(context)
        {
        }
    }
}
