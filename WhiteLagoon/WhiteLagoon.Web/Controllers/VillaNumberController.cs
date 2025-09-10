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
        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.Include(u => u.Villa).ToList();
            return View(villaNumbers);
        }

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
            //IEnumerable<SelectListItem> list = _db.Villas.ToList().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.ID.ToString()
            //});

            //ViewData["VillaList"] = list;
            //ViewBag.VillaList = list;

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

                return RedirectToAction("Index", "Villa");
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

        //public IActionResult Update(int villaID)
        //{
        //    VillaNumber? obj = _db.VillaNumbers.FirstOrDefault(u => u.VillaID == villaID);


        //    //Villa? obj = _db.VillaNumbers.Find(villaID);
        //    //var VillaList = _db.VillaNumbers.Where(u => u.Price > 50 && u.Occupancy > 0);

        //    if (obj is null)
        //    {
        //        return RedirectToAction("Error", "Home");
        //    }

        //    return View(obj);
        //}

        //[HttpPost]
        //public IActionResult Update(Villa obj)
        //{
        //    if (ModelState.IsValid &&  obj.ID > 0)
        //    {
        //        _db.VillaNumbers.Update(obj);
        //        _db.SaveChanges();
        //        TempData["success"] = "Villa updated successfully";

        //        return RedirectToAction("Index", "Villa");
        //    }

        //    return View();
        //}

        //public IActionResult Delete(int villaID)
        //{
        //    Villa? obj = _db.VillaNumbers.FirstOrDefault(u => u.ID == villaID);


        //    //Villa? obj = _db.VillaNumbers.Find(villaID);
        //    //var VillaList = _db.VillaNumbers.Where(u => u.Price > 50 && u.Occupancy > 0);

        //    if (obj is null)
        //    {
        //        return RedirectToAction("Error", "Home");
        //    }

        //    return View(obj);
        //}

        //[HttpPost]
        //public IActionResult Delete(Villa obj)
        //{
        //    Villa? objFromDB = _db.VillaNumbers.FirstOrDefault(u => u.ID == obj.ID);
        //    if (objFromDB is not null)
        //    {
        //        _db.VillaNumbers.Remove(objFromDB);
        //        _db.SaveChanges();
        //        TempData["success"] = "Villa deleted successfully";

        //        return RedirectToAction("Index");
        //    }
        //    TempData["error"] = "Error while deleting Villa";
        //    return View();
        //}
    }
}
