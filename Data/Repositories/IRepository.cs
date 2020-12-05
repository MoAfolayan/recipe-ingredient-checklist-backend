using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace recipe_ingredient_checklist_backend.Data.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T Find(string id);
        IEnumerable<T> All();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
