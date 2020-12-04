using System;
using System.Collections.Generic;

namespace recipe_ingredient_checklist_backend.Data.Models
{
    public partial class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Users User { get; set; }
    }
}
