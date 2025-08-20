using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Category
        IEnumerable<T> GetAll(string? includeProperties = null);
        //T Get(Expression<Func<T, bool>> filter = null,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //    string includeProperties = null);
        T Get(Expression<Func<T, bool>> fiter, string? includeProperties = null);
        void Add(T entity);
        //void Update(T entity);
        //void Delete(T entity);
        //void DeleteRange(IEnumerable<T> entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        T? GetById(object id);
    }
}
