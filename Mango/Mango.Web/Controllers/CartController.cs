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
            return View(LoadCartDTOBasedOnLoggedInUser());
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
