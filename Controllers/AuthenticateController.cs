using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;  
using Microsoft.AspNetCore.Identity;  
using Microsoft.Extensions.Configuration;  
using Microsoft.IdentityModel.Tokens;  
using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;  
using System.Text;  
using recipe_ingredient_checklist_backend.Data; 
using recipe_ingredient_checklist_backend.ViewModels;
using recipe_ingredient_checklist_backend.Services;
using recipe_ingredient_checklist_backend.Enums;
  
namespace recipe_ingredient_checklist_backend.Controllers  
{  
    [Route("[controller]")]  
    [ApiController]  
    public class AuthenticateController : ControllerBase  
    {
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IApplicationUserService _applicationUserService;
  
        public AuthenticateController(ILogger<AuthenticateController> logger,
            IApplicationUserService applicationUserService)  
        {  
            _logger = logger;
            _applicationUserService = applicationUserService;  
        }  
  
        [HttpPost]  
        [Route("login")]  
        public async Task<IActionResult> Login([FromBody] LoginModel model)  
        {  
            (bool success, TokenModel token) = await _applicationUserService.Login(model);
            if (success)
            {
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }  
  
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register([FromBody] RegisterModel model)  
        {
            IActionResult result = null;
            RegistrationOutcome registrationOutcome = await _applicationUserService.Register(model);
            switch (registrationOutcome)
            {
                case RegistrationOutcome.Success:
                    result = Ok
                    (
                        new Response 
                        { 
                            Status = "Success", 
                            Message = "User created successfully!" 
                        }
                    );
                    break;

                case RegistrationOutcome.UserExists:
                    result = StatusCode
                    (
                        StatusCodes.Status406NotAcceptable, 
                        new Response 
                        { 
                            Status = "Error", 
                            Message = "User already exists!" 
                        }
                    );  
                    break;

                case RegistrationOutcome.Failure:
                    result = StatusCode
                    (
                        StatusCodes.Status500InternalServerError, 
                        new Response 
                        { 
                            Status = "Error", 
                            Message = "User creation failed! Please check user details and try again." 
                        }
                    );
                    break;
                
                default: 
                    result = StatusCode
                    (
                        StatusCodes.Status500InternalServerError, 
                        new Response 
                        { 
                            Status = "Error", 
                            Message = "User creation failed! Please check user details and try again." 
                        }
                    );
                    break;
            }
  
            return result;
        }
    }  
} 