using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

		//public IActionResult Index()

		public async Task<IActionResult> CouponIndex()
		{
			List<CouponDTO>? list = new();
			try
			{

				Console.WriteLine($"Calling API: {SD.CouponAPIBase}/api/CouponAPI"); // Thêm dòng này

				ResponseDTO? response = await _couponService.GetAllCouponsAsync();
				Console.WriteLine($"Raw Result: {response.Result}");




				if (response != null && response.IsSuccess)
				{
					list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
				}

				Console.WriteLine($"Coupons count: {list?.Count}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}"); // Sửa dòng này để hiển thị message

			}

			return View(list);
		}
		//public async Task<IActionResult> CouponIndex()
		//{
		//	List<CouponDTO>? list = new();
		//	try
		//	{
		//		Console.WriteLine($"Calling API: {SD.CouponAPIBase}/api/CouponAPI");

		//		ResponseDTO? response = await _couponService.GetAllCouponsAsync();

		//		// Debug chi tiết hơn
		//		Console.WriteLine($"Response is null: {response == null}");
		//		Console.WriteLine($"Response.IsSuccess: {response?.IsSuccess}");
		//		Console.WriteLine($"Response.Message: {response?.Message}");
		//		Console.WriteLine($"Response.Result type: {response?.Result?.GetType().Name}");
		//		Console.WriteLine($"Raw Result: {response?.Result}");

		//		if (response != null && response.IsSuccess)
		//		{
		//			// Kiểm tra Result có phải là string không
		//			if (response.Result is string jsonString)
		//			{
		//				Console.WriteLine($"Result is string: {jsonString}");
		//				list = JsonConvert.DeserializeObject<List<CouponDTO>>(jsonString);
		//			}
		//			else
		//			{
		//				Console.WriteLine($"Result is NOT string, it's: {response.Result?.GetType()}");
		//				// Có thể Result đã là List rồi
		//				list = response.Result as List<CouponDTO>;
		//			}
		//		}

		//		Console.WriteLine($"Coupons count: {list?.Count ?? 0}");
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine($"Error: {ex.Message}");
		//		Console.WriteLine($"Stack trace: {ex.StackTrace}");
		//	}
		//	return View(list);
		//}
	}
}
