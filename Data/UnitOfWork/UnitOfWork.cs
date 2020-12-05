using recipe_ingredient_checklist_backend.Data.Repositories;

namespace recipe_ingredient_checklist_backend.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private RecipeApplicationDbContext _context;

        public UnitOfWork(RecipeApplicationDbContext context)
        {
            _context = context;
        }

        private IRepository<ApplicationUser> _applicationUserRepository;
        public IRepository<ApplicationUser> ApplicationUserRepository
        {
            get
            {
                if (_applicationUserRepository == null)
                {
                    _applicationUserRepository = new ApplicationUserRepository(_context);
                }

                return _applicationUserRepository;
            }
        }

        private IRecipeRepository _recipeRepository;
        public IRecipeRepository RecipeRepository
        {
            get
            {
                if(_recipeRepository == null)
                {
                    _recipeRepository = new RecipeRepository(_context);
                }

                return _recipeRepository;
            }
        }

        private IRepository<Ingredient> _ingredientRepository;
        public IRepository<Ingredient> IngredientRepository
        {
            get
            {
                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new IngredientRepository(_context);
                }

                return _ingredientRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
