using Duende.IdentityModel;
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
        private readonly ICartService _cartService;
        public HomeController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
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
                    //list = JsonConvert.DeserializeObject<List<ProductDTOs>>(Convert.ToString(response.Result));
                    list = JsonConvert.DeserializeObject<List<ProductDTOs>>
                        (JsonConvert.SerializeObject(response.Result)
);

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

        //[Authorize]
        //public async Task<IActionResult> ProductDetails(int productID)
        //{
        //    ProductDTOs? model = new();
        //    try
        //    {

        //        Console.WriteLine($"Calling API: {SD.ProductAPIBase}/api/ProductAPI"); // Thêm dòng này

        //        ResponseDTO? response = await _productService.GetProductByIDAsync(productID);
        //        Console.WriteLine($"Raw Result: {response.Result}");

        //        if (response != null && response.IsSuccess)
        //        {
        //            //model = JsonConvert.DeserializeObject<ProductDTOs>(Convert.ToString(response.Result));
        //            model = JsonConvert.DeserializeObject<ProductDTOs>(
        //                JsonConvert.SerializeObject(response.Result)
        //            );

        //        }
        //        else
        //        {
        //            TempData["error"] = response?.Message;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}"); // Sửa dòng này để hiển thị message

        //    }

        //    return View(model);
        //}

        [Authorize]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductDTOs model = new();

            try
            {
                ResponseDTO? response = await _productService.GetProductByIDAsync(productId);

                if (response == null || !response.IsSuccess || response.Result == null)
                {
                    TempData["error"] = response?.Message ?? "Unable to load product";
                    return View(model);
                }

                // Deserialize an toàn cho cả string & object
                model = JsonConvert.DeserializeObject<ProductDTOs>(
                    JsonConvert.SerializeObject(response.Result)
                ) ?? new ProductDTOs();
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while loading product details";
                Console.WriteLine($"ProductDetails GET Error: {ex.Message}");
            }

            return View(model);
        }


        //[Authorize]
        //[HttpPost]
        //[ActionName("ProductDetails")]
        ////public async Task<IActionResult> ProductDetails(int productID)
        //public async Task<IActionResult> ProductDetails(ProductDTOs productDTO)
        //{
        //    var userId = User.FindFirst(JwtClaimTypes.Subject)?.Value;
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        TempData["error"] = "Bạn chưa đăng nhập";
        //        return RedirectToAction("Login", "Auth");
        //    }

        //    if (productDTO.Count <= 0)
        //    {
        //        TempData["error"] = "Số lượng không hợp lệ";
        //        return View(productDTO);
        //    }

        //    CartDTOs cartDTO = new CartDTOs()
        //    {
        //        CartHeader = new CartHeaderDTOs
        //        {
        //            UserID = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?.FirstOrDefault()?.Value
        //        }
        //    };

        //    CartDetailsDTOs cartDetails = new CartDetailsDTOs()
        //    {
        //        Count = productDTO.Count,
        //        ProductID = productDTO.ProductID,
        //    };

        //    List<CartDetailsDTOs> cartDetailsDTOs = new() { cartDetails };
        //    cartDTO.CartDetails = cartDetailsDTOs;

        //    ResponseDTO? response = await _cartService.UpsertCartAsync(cartDTO);

        //    if (response != null && response.IsSuccess)
        //    {
        //        TempData["success"] = "Item has been added to the Shopping";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        Debug.WriteLine("err");
        //        Debug.WriteLine("err: ",response?.Message);
        //        TempData["error"] = response?.Message;
        //    }
        //    return View(productDTO);
        //}

        [Authorize]
        [HttpPost]
        [ActionName("ProductDetails")]
        public async Task<IActionResult> ProductDetails(ProductDTOs productDto)
        {
            // Validate user
            var userId = User.FindFirst(JwtClaimTypes.Subject)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "Please login to continue";
                return RedirectToAction("Login", "Auth");
            }

            // Validate input
            if (productDto == null || productDto.ProductID <= 0 || productDto.Count <= 0)
            {
                TempData["error"] = "Invalid product or quantity";
                return View(productDto);
            }

            CartDTOs cartDto = new()
            {
                CartHeader = new CartHeaderDTOs
                {
                    UserID = userId
                },
                CartDetails = new List<CartDetailsDTOs>
                {
                    new CartDetailsDTOs
                    {
                        ProductID = productDto.ProductID,
                        Count = productDto.Count
                    }
                }
            };

            try
            {
                ResponseDTO? response = await _cartService.UpsertCartAsync(cartDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Item has been added to the Shopping Cart";
                    return RedirectToAction(nameof(Index));
                }

                TempData["error"] = response?.Message ?? "Failed to add item to cart";

            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while adding item to cart";
                Console.WriteLine($"ProductDetails POST Error: {ex.Message}");
            }

            return View(productDto);
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
