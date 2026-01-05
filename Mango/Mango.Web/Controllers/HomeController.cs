using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDTOs>? list = new();
            try
            {

                Console.WriteLine($"Calling API: {SD.ProductAPIBase}/api/ProductAPI"); // Thêm dòng này

                ResponseDTO? response = await _productService.GetAllProductsAsync();
                Console.WriteLine($"Raw Result: {response.Result}");

                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<ProductDTOs>>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }

                Console.WriteLine($"Products count: {list?.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Sửa dòng này để hiển thị message

            }

            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> ProductDetails(int productID)
        {
            ProductDTOs? model = new();
            try
            {

                Console.WriteLine($"Calling API: {SD.ProductAPIBase}/api/ProductAPI"); // Thêm dòng này

                ResponseDTO? response = await _productService.GetProductByIDAsync(productID);
                Console.WriteLine($"Raw Result: {response.Result}");

                if (response != null && response.IsSuccess)
                {
                    model = JsonConvert.DeserializeObject<ProductDTOs>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Sửa dòng này để hiển thị message

            }

            return View(model);
        }

        [Authorize(Roles = SD.RoleAdmin)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
