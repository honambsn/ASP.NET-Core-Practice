using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infracstructure.Data;
using WhiteLagoon.Web.ViewModels;

namespace WhiteLagoon.Web.Controllers
{
    public class AmenityController : Controller
    {
        //private readonly ApplicationDbContext _db;
        //public AmenityController(ApplicationDbContext db)
        //{
        //    _db = db;
        //}

        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Index
        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            //var totalVillaNumbers = _unitOfWork.VillaNumber.GetAll().Count();
            var totalAmenities = _unitOfWork.Amenity.GetAll().Count();

            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa")
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            var totalPages = (int)Math.Ceiling(totalAmenities / (double)pageSize);

            var viewModel = new AmenityListVM
            {
                //VillaNumbers = villaNumbers,
                Amenities = amenities,
                CurrentPage = page,
                TotalPages = totalPages
            };

            //var villaNumbers = _db.VillaNumbers.Include(u => u.Villa).ToList();
            return View(viewModel);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                 VillaList = _unitOfWork.Amenity.GetAll().ToList().Select(i => new SelectListItem
                 {
                     Text = i.Name,
                     Value = i.ID.ToString()
                 })
            };

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {
            //ModelState.Remove("Villa");
            // bool roomNumberExists = _unitOfWork.VillaNumber.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid)
            {
                //_db.VillaNumbers.Add(obj.VillaNumber);
                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The amenity has been created successfully";

                return RedirectToAction(nameof(Index));
            }

            obj.VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            });

            return View(obj);
        }
        #endregion

        #region Update
        public IActionResult Update(int amenityID)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.ID == amenityID)
            };

            if (amenityVM.Amenity is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Update(amenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The amenity updated successfully";
                return RedirectToAction(nameof(Index));
            }

            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            });

            return View(amenityVM);
        }

        #endregion

        #region Delete
        public IActionResult Delete(int amenityID)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.ID == amenityID)
            };

            if (amenityVM.Amenity is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {
            Amenity? objFromDB = _unitOfWork.Amenity
                .Get(u => u.ID == amenityVM.Amenity.ID);
            
            if (objFromDB is not null)
            {
                _unitOfWork.Amenity.Remove(objFromDB);
                _unitOfWork.Save();
                TempData["success"] = "The amenity has been deleted successfully";

                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error while deleting Villa";
            return View();
        }

        #endregion
    }
}
