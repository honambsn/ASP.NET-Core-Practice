using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infracstructure.Data;

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
            var villaNumbers = _db.VillaNumbers.ToList();
            return View(villaNumbers);
        }

        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(VillaNumber obj)
        //{
        //    if (obj.Name == obj.Description)
        //    {
        //        ModelState.AddModelError("name", "The description cannot exactly match the Name.");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        _db.VillaNumbers.Add(obj);
        //        _db.SaveChanges();
        //        TempData["success"] = "Villa created successfully"; 

        //        return RedirectToAction("Index", "Villa");
        //    }

        //    return View();
        //}

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
