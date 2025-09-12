using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infracstructure.Data;
using WhiteLagoon.Web.ViewModels;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Index
        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            var totalVillaNumbers = _db.VillaNumbers.Count();

            var villaNumbers = _db.VillaNumbers
                .Include(u => u.Villa)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            var totalPages = (int)Math.Ceiling(totalVillaNumbers / (double)pageSize);

            var viewModel = new VillaNumberListVM
            {
                VillaNumbers = villaNumbers,
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
            VillaNumberVM villaNumberVM = new()
            {
                 VillaList = _db.Villas.ToList().Select(i => new SelectListItem
                 {
                     Text = i.Name,
                     Value = i.ID.ToString()
                 })
            };

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            //ModelState.Remove("Villa");
            bool roomNumberExists = _db.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !roomNumberExists)
            {
                _db.VillaNumbers.Add(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa Number has been created successfully";

                return RedirectToAction(nameof(Index));
            }

            if (roomNumberExists)
            {
                TempData["error"] = "Villa Number already exists";
            }

            obj.VillaList = _db.Villas.ToList().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            });

            return View(obj);
        }
        #endregion

        #region Update
        public IActionResult Update(int villaNumberID)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                }),
                VillaNumber = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberID)
            };

            if (villaNumberVM.VillaNumber is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Update(villaNumberVM.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "Villa Number updated successfully";
                return RedirectToAction(nameof(Index));
            }

            villaNumberVM.VillaList = _db.Villas.ToList().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            });

            return View(villaNumberVM);
        }

        #endregion

        #region Delete
        public IActionResult Delete(int villaNumberID)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                }),
                VillaNumber = _db.VillaNumbers.Include(u => u.Villa).FirstOrDefault(u => u.Villa_Number == villaNumberID)
            };

            if (villaNumberVM.VillaNumber is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? objFromDB = _db.VillaNumbers
                    .FirstOrDefault(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
            
            if (objFromDB is not null)
            {
                _db.VillaNumbers.Remove(objFromDB);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been deleted successfully";

                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error while deleting Villa";
            return View();
        }

        #endregion
    }
}
