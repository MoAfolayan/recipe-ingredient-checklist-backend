using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace recipe_ingredient_checklist_backend.Data
{
    public partial class Ingredient
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
