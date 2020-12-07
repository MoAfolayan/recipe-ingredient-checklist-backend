using System;
using System.Collections.Generic;
using System.Linq;
using recipe_ingredient_checklist_backend.Data.Repositories;
using recipe_ingredient_checklist_backend.Data;
using recipe_ingredient_checklist_backend.Data.UnitOfWork;

namespace recipe_ingredient_checklist_backend.Services
{
    public class CheckListService : ICheckListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CheckList FindActiveCheckListWithCheckListItems(int recipeId)
        {
            return _unitOfWork.CheckListRepository.FindActiveCheckListWithCheckListItemsByRecipeId(recipeId);
        }

        public bool Deactivate(int checkListId)
        {
            var result = false;
            var checkList = _unitOfWork.CheckListRepository.Get(checkList => checkList.Id == checkListId).FirstOrDefault();
            if (checkList != null)
            {
                checkList.IsActive = false;
                _unitOfWork.CheckListRepository.Update(checkList);
                var numberOfStateEntriesWrittenToDB = _unitOfWork.SaveChanges();
                result = numberOfStateEntriesWrittenToDB > 0 ? true : false;
            }
            return result;
        }

        public CheckList Add(CheckList checkList)
        {
            var newCheckList = _unitOfWork.CheckListRepository.Add(checkList);
            var numberOfStateEntriesWrittenToDB = _unitOfWork.SaveChanges();
            if (numberOfStateEntriesWrittenToDB > 0)
            {
                var recipe = _unitOfWork.RecipeRepository.FindRecipeWithIngredientsByRecipeId(newCheckList.RecipeId);
                var success = true;
                foreach (var recipeIngredient in recipe.RecipeIngredients)
                {
                    _unitOfWork.CheckListItemRepository.Add(new CheckListItem()
                    {
                        CheckListId = newCheckList.Id,
                        IngredientId = recipeIngredient.Ingredient.Id
                    });

                    var secondNumberOfStateEntriesWrittenToDB = _unitOfWork.SaveChanges();
                    if (secondNumberOfStateEntriesWrittenToDB == 0)
                    {
                        success = false;
                    }
                }

                return success ? 
                    _unitOfWork.CheckListRepository.FindActiveCheckListWithCheckListItemsByCheckListId(newCheckList.Id) :
                    null;
            }
            else
            {
                return null;
            }
        }
    }
}
