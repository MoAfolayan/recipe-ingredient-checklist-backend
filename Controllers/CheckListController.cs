using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;  
using recipe_ingredient_checklist_backend.Services;
using recipe_ingredient_checklist_backend.Data;
using recipe_ingredient_checklist_backend.ViewModels;

namespace recipe_ingredient_checklist_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CheckListController : ControllerBase
    {
        private readonly ILogger<CheckListController> _logger;
        private readonly ICheckListService _checkListService;
        public CheckListController(ILogger<CheckListController> logger,
            ICheckListService checkListService)
        {
            _logger = logger;
            _checkListService = checkListService;
        }

        [HttpGet]
        [Route("{recipeId}")]
        public List<CheckList> Get(int recipeId)
        {
            return _checkListService.FindActiveCheckListWithCheckListItems(recipeId);
        }

        [HttpGet]
        [Route("deactivate/{checkListId}")]
        public IActionResult Deactivate(int checkListId)
        {
            var result = _checkListService.Deactivate(checkListId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "CheckList Id does not exist!" }); 
            }
        }
    }
}
