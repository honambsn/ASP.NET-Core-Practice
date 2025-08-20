using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using BulkyWeb.DataAccess.Data;
using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        //public void Update(Product obj)
        //{
        //    _db.Products.Update(obj);
        //}
         
        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.ID == obj.ID);

            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                obj.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryID = obj.CategoryID;
                objFromDb.Author = obj.Author;

                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }

        public int Count()
        {
            return _db.Products.Count();
        }

        public List<Product> GetPaginatedCategories(int pageNumber, int pageSize)
        {
            var prod = _db.Products
                .Include(Product => Product.Category) // Include the Category navigation property
                .OrderBy(c => c.ID) // Order by DisplayOrder or any other property
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
            .ToList();

            //Debug.WriteLine($"Total Products Retrieved: {prod.Count}");

            return prod;
        }

        //public void IncludeCategoryForProduct(Product product)
        //{
        //    product.Category = _unitOfWork.Category.GetById(product.CategoryId);
        //}
    }
}
