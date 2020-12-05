using System;
using System.Collections.Generic;
using System.Linq;
using recipe_ingredient_checklist_backend.Data;

namespace recipe_ingredient_checklist_backend.Data.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>
    {
        public ApplicationUserRepository(RecipeApplicationDbContext context) : base(context)
        {
        }
    }
}
