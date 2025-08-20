using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.DataAccess.Data;
using BulkyWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }

        public int Count()
        {
            return _db.Categories.Count();
        }

        public List<Category> GetPaginatedCategories(int pageNumber, int pageSize)
        {
            return _db.Categories
                .OrderBy(c => c.DisplayOrder) // Order by DisplayOrder or any other property
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
