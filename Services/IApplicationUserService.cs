using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using recipe_ingredient_checklist_backend.Data;
using recipe_ingredient_checklist_backend.ViewModels;
using recipe_ingredient_checklist_backend.Enums;

namespace recipe_ingredient_checklist_backend.Services
{
    public interface IApplicationUserService
    {
        ApplicationUser GetUserInfoByUsername(string id);
        Task<(bool, TokenModel)> Login(LoginModel model);
        Task<RegistrationOutcome> Register(RegisterModel model);
    }
}
