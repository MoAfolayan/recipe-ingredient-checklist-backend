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
        private readonly ICheckListItemService _checkListItemService;

        public CheckListController(ILogger<CheckListController> logger,
            ICheckListService checkListService,
            ICheckListItemService checkListItemService)
        {
            _logger = logger;
            _checkListService = checkListService;
            _checkListItemService = checkListItemService;
        }

        [HttpGet]
        [Route("{recipeId}")]
        public CheckList Get(int recipeId)
        {
            return _checkListService.FindActiveCheckListWithCheckListItems(recipeId);
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
                    StatusCodes.Status400BadRequest, 
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
