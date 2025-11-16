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

		[HttpGet]
		public async Task<IActionResult> CouponCreate()
		{
			return View();	
		}

		[HttpPost]
		public async Task<IActionResult> CouponCreate(CouponDTO model)
		{
			if  (ModelState.IsValid)
			{
				ResponseDTO? response = await _couponService.CreateCouponsAsync(model);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(CouponIndex));
				}

			}
			return View(model);
		}

        [HttpGet]
        public async Task<IActionResult> CouponDelete(int couponID)
		{
			ResponseDTO? response = await _couponService.GetCouponByIDAsync(couponID);

			if (response != null && response.IsSuccess)
			{
				CouponDTO? model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
				return View(model);
			}

			return NotFound();
		}

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDTO couponDTO)
		{
			ResponseDTO? response = await _couponService.DeleteCouponsAsync(couponDTO.CouponID);

			if (response != null && response.IsSuccess)
			{
				return RedirectToAction(nameof(CouponIndex));
			}

			return View(couponDTO);
		}


	}
}
