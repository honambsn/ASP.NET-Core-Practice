using BulkyWeb.DataAccess.Data;
using Bulky.Utility;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using Bulky.DataAccess.Repository.IRepository;
using System.Net.NetworkInformation;
using Bulky.DataAccess.Repository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.Models.ViewModels;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //private readonly ApplicationDbContext _db;

        //private readonly IProductRepository _ProductRepository;
        private readonly IUnitOfWork _unitOfWork;
        //public ProductController(ApplicationDbContext db)
        //public ProductController(IProductRepository ProductRepository)
        //{
        //    // Constructor logic can be added here if needed
        //    _ProductRepository = ProductRepository;
        //}
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            //List<Product> objProductList = _db.Categories.ToList();

            //return View(objProductList);
            
            int totalProducts = _unitOfWork.Product.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var paginatedCategories = _unitOfWork.Product.GetPaginatedCategories(pageNumber, pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(paginatedCategories);
        }

        //
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Product? ProductFromDb = _ProductRepository.Get(id);
            Product? ProductFromDb = _unitOfWork.Product.Get(u => u.ID == id);
            //Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u => u.ID== id);
            //Product? ProductFromDb2 = _db.Categories.Where(u => u.ID == id).FirstOrDefault();

            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }

        public IActionResult Upsert(int? id) // update & insert
        {
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll()
                .Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                }),
                Product = new Product() // Initialize the Product property
            };

            if (id == null || id == 0)
            {
                // create -> insert
                return View(productVM);
            }
            else
            {
                // update
                productVM.Product = _unitOfWork.Product.Get(u => u.ID == id);
                return View(productVM);
            }

            
        }

        [HttpPost]
        //public IActionResult Create(Product obj)
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {

            //if (obj.ID.ToString() == obj.ID.ToString())
            //{
            //    Debug.WriteLine("error equals");
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";

                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll()
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.ID.ToString()
                    });

                return View(productVM);
            }

            //return View();

        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    //Product? ProductFromDb = _ProductRepository.GetById(id); // good and faster but not used for complex queries


        //    //Product? ProductFromDb = _db.Categories.Find(id);
        //    Product? ProductFromDb = _unitOfWork.Product.Get(u => u.ID == id);
        //    //Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u => u.ID== id);
        //    //Product? ProductFromDb2 = _db.Categories.Where(u => u.ID == id).FirstOrDefault();

        //    if (ProductFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ProductFromDb);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{

        //    //if (obj.ID.ToString() == obj.ID.ToString())
        //    //{
        //    //    Debug.WriteLine("error equals");
        //    //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        //    //}

        //    if (ModelState.IsValid)
        //    {
        //        //_db.Categories.Update(obj);
        //        _unitOfWork.Product.Update(obj);
        //        //_db.SaveChanges();
        //        _unitOfWork.Save();

        //        TempData["success"] = "Product updated successfully";

        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Product? ProductFromDb = _db.Categories.Find(id);
            Product? ProductFromDb = _unitOfWork.Product.GetById(id);
            //Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u => u.ID== id);
            //Product? ProductFromDb2 = _db.Categories.Where(u => u.ID == id).FirstOrDefault();

            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            //Product? obj = _db.Categories.Find(id);
            Product? obj = _unitOfWork.Product.Get(u => u.ID == id);

            if (obj == null)
                return NotFound();

            //_db.Categories.Remove(obj);
            //_db.SaveChanges();
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "Product deleted successfully";

            return RedirectToAction("Index");
        }


    }
}
