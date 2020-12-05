using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore;  
  
namespace recipe_ingredient_checklist_backend.Data  
{  
    public class RecipeApplicationDbContext : IdentityDbContext<ApplicationUser>  
    {  
        public RecipeApplicationDbContext(DbContextOptions<RecipeApplicationDbContext> options) : base(options)  
        {  
  
        }  

        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        public virtual DbSet<CheckList> CheckList { get; set; }
        public virtual DbSet<CheckListItem> CheckListItem { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(applicationUser => applicationUser.Recipes)
                .WithOne(recipe => recipe.ApplicationUser)
                .IsRequired();

            builder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<ApplicationUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder.Entity<RecipeIngredient>()
                .HasKey(recipeIngredient => new { recipeIngredient.RecipeId, recipeIngredient.IngredientId });  

            builder.Entity<RecipeIngredient>()
                .HasOne(recipeIngredient => recipeIngredient.Recipe)
                .WithMany(recipe => recipe.RecipeIngredients)
                .HasForeignKey(recipeIngredient => recipeIngredient.RecipeId);  

            builder.Entity<RecipeIngredient>()
                .HasOne(recipeIngredient => recipeIngredient.Ingredient)
                .WithMany(ingredient => ingredient.RecipeIngredients)
                .HasForeignKey(recipeIngredient => recipeIngredient.IngredientId);

            builder.Entity<CheckList>()
                .HasOne(checkList => checkList.Recipe)
                .WithMany(recipe => recipe.CheckLists)
                .HasForeignKey(checkList => checkList.RecipeId);

            builder.Entity<CheckList>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Entity<CheckListItem>()
                .HasOne(checkListItem => checkListItem.CheckList)
                .WithMany(checkList => checkList.CheckListItems)
                .HasForeignKey(checkListItem => checkListItem.CheckListId);

            builder.Entity<CheckListItem>()
                .HasOne(checkListItem => checkListItem.Ingredient)
                .WithMany(ingredient => ingredient.CheckListItems)
                .HasForeignKey(checkListItem => checkListItem.IngredientId);
        }  
    }  
}  