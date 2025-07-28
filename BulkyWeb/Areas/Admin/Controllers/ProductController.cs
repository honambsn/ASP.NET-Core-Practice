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
using System.IO;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //private readonly ApplicationDbContext _db;

        //private readonly IProductRepository _ProductRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //public ProductController(ApplicationDbContext db)
        //public ProductController(IProductRepository ProductRepository)
        //{
        //    // Constructor logic can be added here if needed
        //    _ProductRepository = ProductRepository;
        //}
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // delete the old image
                        var oldImagePath = 
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }

                    using (var fileStream = 
                        new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName; // Save the image URL to the Product object
                }

                if (productVM.Product.ID == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }

                //_unitOfWork.Product.Add(productVM.Product);
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
