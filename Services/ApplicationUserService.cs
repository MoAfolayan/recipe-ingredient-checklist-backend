using System;
using System.Collections.Generic;
using System.Linq;
using recipe_ingredient_checklist_backend.Data.Repositories;
using recipe_ingredient_checklist_backend.Data;
using recipe_ingredient_checklist_backend.Data.UnitOfWork;

namespace recipe_ingredient_checklist_backend.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApplicationUser GetUserById(string id)
        {
            return _unitOfWork.ApplicationUserRepository.Get(id);
        }
    }
}
