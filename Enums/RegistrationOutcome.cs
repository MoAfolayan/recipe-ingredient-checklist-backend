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
  
namespace recipe_ingredient_checklist_backend.Enums  
{  
    public enum RegistrationOutcome  
    {
        Success,
        UserExists,
        Failure
    }  
} 