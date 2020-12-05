using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipe_ingredient_checklist_backend.Data
{
    public partial class CheckListItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CheckListId { get; set; }

        [Required]
        public int IngredientId { get; set; }

        [Required]
        public bool Checked { get; set; }

        public CheckList CheckList { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
