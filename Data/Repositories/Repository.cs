using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace recipe_ingredient_checklist_backend.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected RecipeApplicationDbContext _context;

        public Repository(RecipeApplicationDbContext context)
        {
            _context = context;
        }

        public virtual T Add(T entity)
        {
            return _context.Add(entity).Entity;
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsQueryable().Where(predicate).ToList();
        }

        public virtual T Find(string id)
        {
            return _context.Find<T>(id);
        }

        public virtual IEnumerable<T> All()
        {
            return _context.Set<T>().AsQueryable().ToList();
        }

        public virtual T Update(T entity)
        {
            return _context.Update(entity).Entity;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
