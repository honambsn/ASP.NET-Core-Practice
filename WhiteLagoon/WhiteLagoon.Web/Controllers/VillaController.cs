using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infracstructure.Data;
using WhiteLagoon.Web.Models;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        // private readonly IVillaRepository _unitOfWork.Villa;

        private readonly IUnitOfWork _unitOfWork;

        //public VillaController(IVillaRepository villaRepo)
        //{
        //    _unitOfWork.Villa = villaRepo;
        //}

        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int page = 1)
        {

            //var villas = _unitOfWork.Villa.GetAll().ToList();
            //return View(villas);

            int pageSize = 5; // Number of items per page
            //var totalVillas = _unitOfWork.Villa.GetCount();
            var totalVillas = _unitOfWork.Villa.GetCount();

            var villas =  _unitOfWork.Villa.GetPaginated(page, pageSize);

            var totalPages = (int)Math.Ceiling(totalVillas / (double)pageSize);

            var model = new VillaListViewModel
            {
                Villas = villas.ToList(),
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
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Villa created successfully"; 

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Update(int villaID)
        {
            Villa? obj = _unitOfWork.Villa.Get(u  => u.ID == villaID);
            
            
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
                _unitOfWork.Villa.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Villa updated successfully";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Delete(int villaID)
        {
            Villa? obj = _unitOfWork.Villa.Get(u =>u.ID == villaID);
            
            
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
            Villa? objFromDB = _unitOfWork.Villa.Get(u => u.ID == obj.ID);
            if (objFromDB is not null)
            {
                _unitOfWork.Villa.Remove(objFromDB);
                _unitOfWork.Save();
                TempData["success"] = "Villa deleted successfully";

                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error while deleting Villa";
            return View();
        }
    }
}
