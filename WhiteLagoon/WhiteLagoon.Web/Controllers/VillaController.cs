using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infracstructure.Data;
using WhiteLagoon.Web.Models;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaRepository _villaRepo;
        public VillaController(IVillaRepository villaRepo)
        {
            _villaRepo = villaRepo;
        }
        public IActionResult Index(int page = 1)
        {

            //var villas = _villaRepo.GetAll().ToList();
            //return View(villas);

            int pageSize = 5; // Number of items per page
            var totalVillas = _db.Villas.Count();

            var vilass = _db.Villas.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToList();

            var totalPages = (int)Math.Ceiling(totalVillas / (double)pageSize);

            var model = new VillaListViewModel
            {
                Villas = vilass,
                CurrentPage = page,
                TotalPages = totalPages
            };
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "The description cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Villas.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Villa created successfully"; 

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Update(int villaID)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u => u.ID == villaID);
            
            
            //Villa? obj = _db.Villas.Find(villaID);
            //var VillaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 0);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid &&  obj.ID > 0)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Villa updated successfully";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Delete(int villaID)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u => u.ID == villaID);
            
            
            //Villa? obj = _db.Villas.Find(villaID);
            //var VillaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 0);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDB = _db.Villas.FirstOrDefault(u => u.ID == obj.ID);
            if (objFromDB is not null)
            {
                _db.Villas.Remove(objFromDB);
                _db.SaveChanges();
                TempData["success"] = "Villa deleted successfully";

                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error while deleting Villa";
            return View();
        }
    }
}
