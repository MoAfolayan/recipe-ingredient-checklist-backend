using Microsoft.AspNetCore.Identity;  
using System.Collections.Generic;
  
namespace recipe_ingredient_checklist_backend.Data  
{  
    public class ApplicationUser: IdentityUser  
    {
        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }  
}  
