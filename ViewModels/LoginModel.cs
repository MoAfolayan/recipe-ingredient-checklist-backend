using System.ComponentModel.DataAnnotations;  
  
namespace recipe_ingredient_checklist_backend.ViewModels  
{  
    public class LoginModel  
    {  
        [Required(ErrorMessage = "User Name is required")]  
        public string Username { get; set; }  
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; set; }  
    }  
} 