using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;  
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;  
using Microsoft.Extensions.Configuration;  
using Microsoft.IdentityModel.Tokens;  
using System.IdentityModel.Tokens.Jwt;  
using recipe_ingredient_checklist_backend.Data.Repositories;
using recipe_ingredient_checklist_backend.Data;
using recipe_ingredient_checklist_backend.Data.UnitOfWork;
using recipe_ingredient_checklist_backend.ViewModels;
using recipe_ingredient_checklist_backend.Enums;

namespace recipe_ingredient_checklist_backend.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _applicationUserManager;
        private readonly IConfiguration _configuration;  

        public ApplicationUserService(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> applicationUserManager,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _applicationUserManager = applicationUserManager;
            _configuration = configuration; 
        }

        public async Task<RegistrationOutcome> Register(RegisterModel model)
        {
            var existingApplicationUser = await _applicationUserManager.FindByNameAsync(model.Username);  
            if (existingApplicationUser != null)
            {
                return RegistrationOutcome.UserExists;
            }
  
            ApplicationUser applicationUser = new ApplicationUser()  
            {  
                Email = model.Email,
                Name = model.Name,
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };

            var result = await _applicationUserManager.CreateAsync(applicationUser, model.Password);  
            return result.Succeeded ? RegistrationOutcome.Success : RegistrationOutcome.Failure;
        }

        public async Task<(bool, TokenModel)> Login(LoginModel model)
        {
            TokenModel token = null;
            bool success = false;

            var applicationUser = await _applicationUserManager.FindByNameAsync(model.Username);  
            if (applicationUser != null && await _applicationUserManager.CheckPasswordAsync(applicationUser, model.Password))  
            {  
                token = await CreateToken(applicationUser);
                success = true;
            }

            return (success, token);
        }

        public ApplicationUser GetUserInfoByUsername(string username)
        {
            return _unitOfWork.ApplicationUserRepository
                .Get(ApplicationUser => ApplicationUser.UserName == username)
                .FirstOrDefault();
        }

        private async Task<TokenModel> CreateToken(ApplicationUser applicationUser)
        {
            var authClaims = new List<Claim>  
            {  
                new Claim(ClaimTypes.Name, applicationUser.UserName),  
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
            };  

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  

            var token = new JwtSecurityToken
            (  
                issuer: _configuration["JWT:ValidIssuer"],  
                audience: _configuration["JWT:ValidAudience"],  
                expires: DateTime.Now.AddHours(3),  
                claims: authClaims,  
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo  
            };
        }
    }
}
