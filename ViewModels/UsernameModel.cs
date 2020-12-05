using System.ComponentModel.DataAnnotations;  
  
namespace recipe_ingredient_checklist_backend.ViewModels  
{  
    public class UsernameModel  
    {  
        [Required(ErrorMessage = "User Name is required")]  
        public string Username { get; set; }  
    }  
} 