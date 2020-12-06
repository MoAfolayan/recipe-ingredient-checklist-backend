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
                var numberOfStateEntriesWrittenToDB = _unitOfWork.SaveChanges();
                result = numberOfStateEntriesWrittenToDB > 0 ? true : false;
            }
            return result;
        }

        public CheckList Add(CheckList checkList)
        {
            var result = _unitOfWork.CheckListRepository.Add(checkList);
            var numberOfStateEntriesWrittenToDB = _unitOfWork.SaveChanges();
            if (numberOfStateEntriesWrittenToDB > 0)
            {
                return result;
            }
            else
            {
                return null;
            }     
        }
    }
}
