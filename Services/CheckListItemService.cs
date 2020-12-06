using System;
using System.Collections.Generic;
using System.Linq;
using recipe_ingredient_checklist_backend.Data.Repositories;
using recipe_ingredient_checklist_backend.Data;
using recipe_ingredient_checklist_backend.Data.UnitOfWork;

namespace recipe_ingredient_checklist_backend.Services
{
    public class CheckListItemService : ICheckListItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckListItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CheckListItem ToggleChecked(int checkListItemId)
        {
            var checkListItem = _unitOfWork.CheckListItemRepository
                .Get(checkListItem => checkListItem.Id == checkListItemId)
                .FirstOrDefault();
            
            checkListItem.Checked = checkListItem.Checked ? false : true;
            var numberOfStateEntriesWrittenToDB = _unitOfWork.SaveChanges();
            if (numberOfStateEntriesWrittenToDB > 0)
            {
                return checkListItem;
            }
            else
            {
                return null;
            }
        }
    }
}
