using System;
using System.Collections.Generic;

namespace recipe_ingredient_checklist_backend.Data.Models
{
    public partial class Users
    {
        public Users()
        {
            Recipe = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Recipe> Recipe { get; set; }
    }
}
