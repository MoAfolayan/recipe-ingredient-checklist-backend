using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using recipe_ingredient_checklist_backend.Services;
using recipe_ingredient_checklist_backend.Data;

namespace recipe_ingredient_checklist_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;
        private readonly IRecipeService _recipeService;
        private readonly IApplicationUserService _applicationUserService;

        public RecipesController(ILogger<RecipesController> logger, 
            IRecipeService recipeService,
            IApplicationUserService applicationUserService)
        {
            _logger = logger;
            _recipeService = recipeService;
            _applicationUserService = applicationUserService;
        }

        [HttpGet]
        public ApplicationUser Get()
        {
            var applicationUser = _applicationUserService.GetUserInfoByUsername(User.Identity.Name);
            return _recipeService.FindRecipeWithIngredientsByUserId(applicationUser.Id);
        }
    }
}
