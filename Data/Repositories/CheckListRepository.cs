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

        public CheckList FindActiveCheckListWithCheckListItemsByRecipeId(int recipeId)
        {
            return _context.CheckList
                .Where(checkList => checkList.RecipeId == recipeId)
                .Where(checkList => checkList.IsActive == true)
                .OrderByDescending(x => x.Id)
                .Include(checkList => checkList.CheckListItems)
                .ThenInclude(checkListItem => checkListItem.Ingredient)
                .FirstOrDefault();
        }

        public CheckList FindActiveCheckListWithCheckListItemsByCheckListId(int checkListId)
        {
            return _context.CheckList
                .Where(checkList => checkList.Id == checkListId)
                .Include(checkList => checkList.CheckListItems)
                .ThenInclude(checkListItem => checkListItem.Ingredient)
                .FirstOrDefault();
        }
    }
}
