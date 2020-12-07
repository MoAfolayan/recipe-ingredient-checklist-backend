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

        public CheckListItem UpdateCheckListItemStatus(CheckListItem checkListItem)
        {
            var newCheckListItem = _unitOfWork.CheckListItemRepository
                .Get(item => item.Id == checkListItem.Id)
                .FirstOrDefault();
            
            newCheckListItem.Checked = checkListItem.Checked;
            var numberOfStateEntriesWrittenToDB = _unitOfWork.SaveChanges();
            if (numberOfStateEntriesWrittenToDB > 0)
            {
                return newCheckListItem;
            }
            else
            {
                return null;
            }
        }
    }
}
