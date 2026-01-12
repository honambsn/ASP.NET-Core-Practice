using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Models.DTOs;
using Mango.Services.ShoppingCartAPI.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private IProductService _productService;

        public CartAPIController(AppDbContext db,
            IMapper mapper, IProductService productService)
        {
            _db = db;
            this._response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet("GetCart/{userID}")]
        public async Task<ResponseDTO> GetCart(string userID)
        {
            try
            {
                CartDTOs cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDTOs>(_db.CartHeaders.First(u => u.UserID == userID))
                };
                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDTOs>>(_db.CartDetails
                    .Where(u => u.CartHeaderID == cart.CartHeader.CartHeaderID));

                IEnumerable<ProductDTOs> productDTOs = await _productService.GetProducts();

                foreach (var item in cart.CartDetails)
                {
                    item.Product = productDTOs.FirstOrDefault(u => u.ProductID == item.ProductID);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }
                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDTO> CartUpsert(CartDTOs cartDTO)
        {
            try
            {
                var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                    u => u.UserID == cartDTO.CartHeader.UserID);

                if (cartHeaderFromDb == null)
                {
                    // create header and details
                    CartHeader cartHeader =  _mapper.Map<CartHeader>(cartDTO.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartDTO.CartDetails.First().CartHeaderID = cartHeader.CartHeaderID;
                    _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    // if header is not null
                    // check if details has same product
                    var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.ProductID == cartDTO.CartDetails.First().ProductID &&
                        u.CartHeaderID == cartHeaderFromDb.CartHeaderID);

                    if (cartDetailsFromDb == null)
                    {
                        // create cartdetails
                        cartDTO.CartDetails.First().CartHeaderID = cartHeaderFromDb.CartHeaderID;
                        _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        // update count in cart details
                        cartDTO.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDTO.CartDetails.First().CartHeaderID = cartDetailsFromDb.CartHeaderID;
                        cartDTO.CartDetails.First().CartDetailsID = cartDetailsFromDb.CartDetailsID;
                        _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                }
                _response.Result = cartDTO;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;

            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDTO> RemoveCart([FromBody]int cartDetailsID)
        {
            try
            {
                CartDetails cartDetails = _db.CartDetails
                    .First(u => u.CartHeaderID == cartDetailsID);

                int totalCountofCartItem = _db.CartDetails.Where(u => u.CartHeaderID == cartDetails.CartHeaderID).
                    Count();
                _db.CartDetails.Remove(cartDetails);

                if (totalCountofCartItem == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders
                        .FirstOrDefaultAsync(u => u.CartHeaderID == cartDetails.CartHeaderID);

                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _db.SaveChangesAsync();

                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;

            }

            return _response;
        }
    }
}
