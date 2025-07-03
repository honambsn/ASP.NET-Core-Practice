using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            // Constructor logic can be added here if needed
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();

            ///return View(objCategoryList);
            ///
            // Logic to decide the theme based on the time of day
            var currentHour = DateTime.Now.Hour;
            string navbarClass;
            string textClass;

            var check = true;
            // Set theme based on the time of day (Night: 6 PM to 6 AM, Day: 6 AM to 6 PM)
            //if (currentHour >= 18 || currentHour < 6) // Nighttime (Dark Mode)
            if(check)
            {
                navbarClass = "navbar-dark bg-primary";
                textClass = "";  // Remove text-dark class
            }
            else // Daytime (Light Mode)
            {
                navbarClass = "navbar-light bg-white";
                textClass = "text-dark";  // Add text-dark class
            }

            Debug.WriteLine("Navbar Class: " + navbarClass);
            Debug.WriteLine("Text Class: " + textClass);


            // Passing data to the View
            ViewBag.NavbarClass = navbarClass;
            ViewBag.TextClass = textClass;
            ViewBag.Categories = objCategoryList;

            return View();
        }
    }
}
