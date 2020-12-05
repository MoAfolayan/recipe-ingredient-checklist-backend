using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipe_ingredient_checklist_backend.Data
{
    public partial class CheckList
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public Recipe Recipe { get; set; }

        public ICollection<CheckListItem> CheckListItems { get; set; }
    }
}
