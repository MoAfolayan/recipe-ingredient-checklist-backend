using System;
using System.Collections.Generic;
using System.Linq;
using recipe_ingredient_checklist_backend.Data.Repositories;
using recipe_ingredient_checklist_backend.Data;
using recipe_ingredient_checklist_backend.Data.UnitOfWork;

namespace recipe_ingredient_checklist_backend.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Recipe> FindRecipeWithIngredients(string id)
        {
            return _unitOfWork.RecipeRepository.FindRecipeWithIngredients(id);
        }
    }
}
