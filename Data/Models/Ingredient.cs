using System;
using System.Collections.Generic;

namespace recipe_ingredient_checklist_backend.Data.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            Recipe = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Recipe> Recipe { get; set; }
    }
}
