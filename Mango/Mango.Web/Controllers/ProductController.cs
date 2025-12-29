using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService ProductService)
        {
            _productService = ProductService;
        }

		//public IActionResult Index()

		public async Task<IActionResult> ProductIndex()
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

		[HttpGet]
		public async Task<IActionResult> ProductCreate()
		{
			return View();	
		}

		[HttpPost]
		public async Task<IActionResult> ProductCreate(ProductDTOs model)
		{
			if  (ModelState.IsValid)
			{
				ResponseDTO? response = await _productService.CreateProductsAsync(model);

				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Product created successfully";
                    return RedirectToAction(nameof(ProductIndex));
				}
                else
                {
                    TempData["error"] = response?.Message;
                }

            }
			return View(model);
		}

        [HttpGet]
        public async Task<IActionResult> ProductDelete(int ProductID)
		{
			ResponseDTO? response = await _productService.GetProductByIDAsync(ProductID);

			if (response != null && response.IsSuccess)
			{
				ProductDTOs? model = JsonConvert.DeserializeObject<ProductDTOs>(Convert.ToString(response.Result));
				return View(model);
			}
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound();
		}

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDTOs ProductDTOs)
		{
			ResponseDTO? response = await _productService.DeleteProductsAsync(ProductDTOs.ProductID);

			if (response != null && response.IsSuccess)
			{
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(ProductIndex));
			}
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(ProductDTOs);
		}


	}
}
