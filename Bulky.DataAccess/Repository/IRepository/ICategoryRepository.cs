using BulkyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
        //void Save();
        int Count();
        List<Category> GetPaginatedCategories(int pageNumber, int pageSize);
        
    }
}
