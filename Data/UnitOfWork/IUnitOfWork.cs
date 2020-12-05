using recipe_ingredient_checklist_backend.Data.Repositories;

namespace recipe_ingredient_checklist_backend.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<ApplicationUser> ApplicationUserRepository { get; }
        IRecipeRepository RecipeRepository { get; }
        IRepository<Ingredient> IngredientRepository { get; }

        void SaveChanges();
    }
}