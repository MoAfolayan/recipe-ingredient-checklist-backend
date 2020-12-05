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
  
namespace recipe_ingredient_checklist_backend.Controllers  
{  
    [Route("[controller]")]  
    [ApiController]  
    public class AuthenticateController : ControllerBase  
    {  
        private readonly UserManager<ApplicationUser> _applicationUserManager;  
        private readonly RoleManager<IdentityRole> _roleManager;  
        private readonly IConfiguration _configuration;  
  
        public AuthenticateController(UserManager<ApplicationUser> applicationUserManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)  
        {  
            _applicationUserManager = applicationUserManager;  
            _roleManager = roleManager;  
            _configuration = configuration;  
        }  
  
        [HttpPost]  
        [Route("login")]  
        public async Task<IActionResult> Login([FromBody] LoginModel model)  
        {  
            var applicationUser = await _applicationUserManager.FindByNameAsync(model.Username);  
            if (applicationUser != null && await _applicationUserManager.CheckPasswordAsync(applicationUser, model.Password))  
            {  
                var userRoles = await _applicationUserManager.GetRolesAsync(applicationUser);  
  
                var authClaims = new List<Claim>  
                {  
                    new Claim(ClaimTypes.Name, applicationUser.UserName),  
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                };  
  
                foreach (var userRole in userRoles)  
                {  
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
                }  
  
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  
  
                var token = new JwtSecurityToken(  
                    issuer: _configuration["JWT:ValidIssuer"],  
                    audience: _configuration["JWT:ValidAudience"],  
                    expires: DateTime.Now.AddHours(3),  
                    claims: authClaims,  
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
                    );  
  
                return Ok(new  
                {  
                    token = new JwtSecurityTokenHandler().WriteToken(token),  
                    expiration = token.ValidTo  
                });  
            }  
            return Unauthorized();  
        }  
  
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register([FromBody] RegisterModel model)  
        {  
            var userExists = await _applicationUserManager.FindByNameAsync(model.Username);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  
  
            ApplicationUser applicationUser = new ApplicationUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };  
            var result = await _applicationUserManager.CreateAsync(applicationUser, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });  
  
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }
    }  
} 