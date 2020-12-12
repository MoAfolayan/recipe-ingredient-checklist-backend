using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
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
        private readonly ICheckListItemService _checkListItemService;
        private readonly IRecipeService _recipeService;
        private readonly IApplicationUserService _applicationUserService;

        public CheckListController(ILogger<CheckListController> logger,
            ICheckListService checkListService,
            ICheckListItemService checkListItemService,
            IRecipeService recipeService,
            IApplicationUserService applicationUserService)
        {
            _logger = logger;
            _checkListService = checkListService;
            _checkListItemService = checkListItemService;
            _recipeService = recipeService;
            _applicationUserService = applicationUserService;
        }

        [HttpGet]
        [Route("{recipeId}")]
        public IActionResult Get(int recipeId)
        {
            var applicationUser = _applicationUserService.GetUserInfoByUsername(User.Identity.Name);
            var recipes = _recipeService.FindRecipesByUserId(applicationUser.Id);
            bool recipeBelongsToUser = false;
            foreach (var recipe in recipes)
            {
                if (recipe.Id == recipeId)
                {
                    recipeBelongsToUser = true;
                }
            }
            if (recipeBelongsToUser)
            {
                var checklist = _checkListService.FindActiveCheckListWithCheckListItems(recipeId);
                return Ok(checklist);  
            }
            else
            {
                return StatusCode
                (
                    StatusCodes.Status401Unauthorized,
                    new Response
                    {
                        Status = "Error",
                        Message = "Requested data does not belong to authenticated user"
                    }
                );
            }
            
        }

        [HttpPost]
        [Route("deactivate")]
        public IActionResult Deactivate(CheckList checkList)
        {
            var result = _checkListService.Deactivate(checkList.Id);
            if (result)
            {
                return Ok(new  
                {
                    checkListId = checkList.Id,
                    deactivated = result
                });  
            }
            else
            {
                return StatusCode
                (
                    StatusCodes.Status404NotFound,
                    new Response
                    {
                        Status = "Error",
                        Message = "CheckList Id does not exist!"
                    }
                );
            }
        }

        [HttpPut]
        public CheckList Put(CheckList checkList)
        {
            return _checkListService.Add(checkList);
        }

        [HttpPost]
        [Route("updatechecklistitemstatus")]
        public CheckListItem UpdateCheckListItemStatus(CheckListItem checkListItem)
        {
            return _checkListItemService.UpdateCheckListItemStatus(checkListItem);
        }
    }
}
