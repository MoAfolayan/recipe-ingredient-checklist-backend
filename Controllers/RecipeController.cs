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
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeService _recipeService;
        private readonly IApplicationUserService _applicationUserService;

        public RecipeController(ILogger<RecipeController> logger, 
            IRecipeService recipeService,
            IApplicationUserService applicationUserService)
        {
            _logger = logger;
            _recipeService = recipeService;
            _applicationUserService = applicationUserService;
        }

        [HttpGet]
        public List<Recipe> Get()
        {
            var applicationUser = _applicationUserService.GetUserByUsername(User.Identity.Name);
            return _recipeService.FindRecipeWithIngredientsByUserId(applicationUser.Id);
        }
    }
}
