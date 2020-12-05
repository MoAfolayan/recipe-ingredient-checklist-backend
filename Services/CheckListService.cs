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

        public List<CheckList> FindActiveCheckListWithCheckListItems(int recipeId)
        {
            return _unitOfWork.CheckListRepository.FindActiveCheckListWithCheckListItems(recipeId);
        }

        public bool Deactivate(int checkListId)
        {
            var result = false;
            var checkList = _unitOfWork.CheckListRepository.Get(checkList => checkList.Id == checkListId).FirstOrDefault();
            if (checkList != null)
            {
                checkList.IsActive = false;
                _unitOfWork.CheckListRepository.Update(checkList);
                _unitOfWork.SaveChanges();

                result = true;
            }
            return result;
        }
    }
}
