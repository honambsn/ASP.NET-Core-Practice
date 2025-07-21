using BulkyWeb.DataAccess.Data;
using Bulky.Utility;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using Bulky.DataAccess.Repository.IRepository;
using System.Net.NetworkInformation;
using Bulky.DataAccess.Repository;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _db;

        //private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        //public CategoryController(ApplicationDbContext db)
        //public CategoryController(ICategoryRepository categoryRepository)
        //{
        //    // Constructor logic can be added here if needed
        //    _categoryRepository = categoryRepository;
        //}
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            //List<Category> objCategoryList = _db.Categories.ToList();

            //return View(objCategoryList);

            int totalCategories = _unitOfWork.Category.Count();
            int totalPages = (int)Math.Ceiling((double)totalCategories / pageSize);

            var paginatedCategories = _unitOfWork.Category.GetPaginatedCategories(pageNumber, pageSize);

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

            //Category? categoryFromDb = _categoryRepository.Get(id);
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.ID == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.ID== id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.ID == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if (obj.Name.ToString() == obj.DisplayOrder.ToString())
            {
                Debug.WriteLine("error equals");
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Category? categoryFromDb = _categoryRepository.GetById(id); // good and faster but not used for complex queries


            //Category? categoryFromDb = _db.Categories.Find(id);
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.ID == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.ID== id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.ID == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (obj.Name.ToString() == obj.DisplayOrder.ToString())
            {
                Debug.WriteLine("error equals");
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                //_db.Categories.Update(obj);
                _unitOfWork.Category.Update(obj);
                //_db.SaveChanges();
                _unitOfWork.Save();

                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index");
            }

            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Category? categoryFromDb = _db.Categories.Find(id);
            Category? categoryFromDb = _unitOfWork.Category.GetById(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.ID== id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.ID == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            //Category? obj = _db.Categories.Find(id);
            Category? obj = _unitOfWork.Category.Get(u => u.ID == id);

            if (obj == null)
                return NotFound();

            //_db.Categories.Remove(obj);
            //_db.SaveChanges();
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }


    }
}
