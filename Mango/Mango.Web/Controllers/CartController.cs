using Mango.Web.Extensions;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;


namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDTOBasedOnLoggedInUser());
        }

        public async Task<IActionResult> Remove(int cartDetailsID)
        {
            var userID = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?
                .FirstOrDefault()?.Value;

            ResponseDTO? response = await _cartService.RemoveFromCartAsync(cartDetailsID);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpPost]        
        public async Task<IActionResult> ApplyCoupon(CartDTOs cartDTO)
        {
            //var userID = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?
            //    .FirstOrDefault()?.Value;

            ResponseDTO? response = await _cartService.ApplyCouponAsync(cartDTO);
            
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }
        
        [HttpPost]        
        public async Task<IActionResult> EmailCart(CartDTOs cartDTO)
        {
            //var userID = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?
            //    .FirstOrDefault()?.Value;
            CartDTOs cart = await LoadCartDTOBasedOnLoggedInUser();
            cart.CartHeader.Email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;

            ResponseDTO? response = await _cartService.EmailCart(cart);
            
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Email will be proccessed and sent shortly.";
                return RedirectToAction(nameof(CartIndex));
            }

            TempData["warning"] = "Something failed";
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDTOs cartDTO)
        {
            cartDTO.CartHeader.CouponCode = "";

            ResponseDTO? response = await _cartService.ApplyCouponAsync(cartDTO);
            
            if (response != null && response.IsSuccess)
            {
                //TempData["info"] = "Coupon removed";
                //TempData["success"] = "Cart updated successfully";
                this.Success("Cart updated successfully");
                this.Info("Coupon removed");
                

                return RedirectToAction(nameof(CartIndex));
            }

            this.Error("Failed to update cart");

            return View();
        }


        private async Task<CartDTOs> LoadCartDTOBasedOnLoggedInUser()
        {
            var userID = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?
                .FirstOrDefault()?.Value;   

            ResponseDTO? response = await _cartService.GetCartByUserIDAsync(userID);
            if (response != null && response.IsSuccess)
            {
                CartDTOs cartDTO = JsonConvert.DeserializeObject<CartDTOs>(Convert.ToString(response.Result));
                return cartDTO;
            }
            return new CartDTOs();
        }
    }
}
