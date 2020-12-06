using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;  
using Microsoft.AspNetCore.Identity;  
using Microsoft.IdentityModel.Tokens;  
using System.Text;  
using recipe_ingredient_checklist_backend.Data;
using recipe_ingredient_checklist_backend.Data.Repositories;
using recipe_ingredient_checklist_backend.Data.UnitOfWork;
using recipe_ingredient_checklist_backend.Services;

namespace recipe_ingredient_checklist_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            // For Entity Framework  
            services.AddDbContext<RecipeApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
  
            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()  
                .AddEntityFrameworkStores<RecipeApplicationDbContext>()  
                .AddDefaultTokenProviders();  
  
            // Adding Authentication  
            services.AddAuthentication(options =>  
            {  
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
            })  
  
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>  
            {  
                options.SaveToken = true;  
                options.RequireHttpsMetadata = false;  
                options.TokenValidationParameters = new TokenValidationParameters()  
                {  
                    ValidateIssuer = true,  
                    ValidateAudience = true,  
                    ValidAudience = Configuration["JWT:ValidAudience"],  
                    ValidIssuer = Configuration["JWT:ValidIssuer"],  
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))  
                };  
            });

            // Services
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IRecipeService, RecipeService>();
            services.AddTransient<ICheckListService, CheckListService>();
            services.AddTransient<ICheckListItemService, CheckListItemService>();

            // Repositories
            services.AddTransient<IRepository<ApplicationUser>, ApplicationUserRepository>();
            services.AddTransient<IRepository<Recipe>, RecipeRepository>();
            services.AddTransient<IRepository<Ingredient>, IngredientRepository>();
            services.AddTransient<IRepository<CheckList>, CheckListRepository>();
            services.AddTransient<IRepository<CheckListItem>, CheckListItemRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
