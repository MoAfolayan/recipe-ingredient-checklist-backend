using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using recipe_ingredient_checklist_backend.Data.Models;

namespace recipe_ingredient_checklist_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly RecipeDBContext _dbContext;
        private readonly ILogger<UserController> _logger;

        public UserController(RecipeDBContext dbContext, ILogger<UserController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public Users Users(int id)
        {
            return _dbContext.Users.Find(id);
        }
    }
}
