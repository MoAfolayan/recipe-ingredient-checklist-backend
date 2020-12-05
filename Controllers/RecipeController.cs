using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace recipe_ingredient_checklist_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(ILogger<RecipeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Recipe")]
        public string Get()
        {
            return "recipe";
        }
    }
}
