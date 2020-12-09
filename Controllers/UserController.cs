using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using recipe_ingredient_checklist_backend.Services;
using recipe_ingredient_checklist_backend.Data;
using recipe_ingredient_checklist_backend.ViewModels;

namespace recipe_ingredient_checklist_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IApplicationUserService _applicationUserService;

        public UserController(ILogger<UserController> logger, 
            IApplicationUserService applicationUserService)
        {
            _logger = logger;
            _applicationUserService = applicationUserService;
        }

        [HttpGet]
        public ApplicationUser Get()
        {
            return _applicationUserService.GetUserByUsername(User.Identity.Name);
        }
    }
}
