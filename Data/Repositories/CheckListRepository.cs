using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using recipe_ingredient_checklist_backend.Data;

namespace recipe_ingredient_checklist_backend.Data.Repositories
{
    public class CheckListRepository : Repository<CheckList>, ICheckListRepository
    {
        public CheckListRepository(RecipeApplicationDbContext context) : base(context)
        {
        }

        public List<CheckList> FindActiveCheckListWithCheckListItems(int recipeId)
        {
            return _context.CheckList
                .Where(checkList => checkList.RecipeId == recipeId)
                .Where(checkList => checkList.IsActive == true)
                .Include(checkList => checkList.CheckListItems)
                .ThenInclude(checkListItem => checkListItem.Ingredient)
                .ToList();
        }
    }
}
