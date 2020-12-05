using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace recipe_ingredient_checklist_backend.Data
{
    public partial class RecipeIngredient
    {
        [Required]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        [Required]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
