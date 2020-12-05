using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace recipe_ingredient_checklist_backend.Data.Repositories
{
    public interface ICheckListRepository : IRepository<CheckList>
    {
        List<CheckList> FindActiveCheckListWithCheckListItems(int recipeId);
    }
}