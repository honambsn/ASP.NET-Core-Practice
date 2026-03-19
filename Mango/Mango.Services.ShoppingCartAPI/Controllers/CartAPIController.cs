using AutoMapper;
using Mango.MessageBus;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Models.DTOs;
using Mango.Services.ShoppingCartAPI.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.WebSockets;
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
        private ICouponService _couponService;
        private IConfiguration _configuration;

        private readonly IMessageBus _messageBus;

        public CartAPIController(AppDbContext db,
            IMapper mapper, IProductService productService, ICouponService couponService, IMessageBus messageBus, IConfiguration configuration)
        {
            _db = db;
            _productService = productService;
            _couponService = couponService;
            this._response = new ResponseDTO();
            _mapper = mapper;
            _messageBus = messageBus;
            _configuration = configuration;
        }

        #region old ver
        //[HttpGet("GetCart/{userID}")]
        //public async Task<ResponseDTO> GetCart(string userID)
        //{
        //    try
        //    {
        //        CartDTOs cart = new()
        //        {
        //            CartHeader = _mapper.Map<CartHeaderDTOs>(_db.CartHeaders.FirstOrDefault(u => u.UserID == userID))
        //        };

        //        cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDTOs>>(_db.CartDetails.
        //            Where(u => u.CartHeaderID == cart.CartHeader.CartHeaderID));

        //        IEnumerable<ProductDTOs> productDTOs = await _productService.GetProducts();

        //        foreach (var item in cart.CartDetails)
        //        {
        //            item.Product = productDTOs.FirstOrDefault(u => u.ProductID == item.ProductID);
        //            cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
        //        }

        //        if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
        //        {
        //            CouponDTOs coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);

        //            if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
        //            {
        //                cart.CartHeader.CartTotal -= coupon.DiscountAmount;
        //                cart.CartHeader.Discount = coupon.DiscountAmount;
        //            }
        //        }

        //        _response.Result = cart;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        //[HttpGet("GetCart/{userID}")]
        //public async Task<ResponseDTO> GetCart(string userID)
        //{
        //    try
        //    {
        //        // Lấy CartHeader
        //        var cartHeaderFromDb = await _db.CartHeaders
        //            .AsNoTracking()
        //            .FirstOrDefaultAsync(u => u.UserID == userID);

        //        if (cartHeaderFromDb == null)
        //        {
        //            _response.IsSuccess = false;
        //            _response.Message = "Cart not found";
        //            return _response;
        //        }

        //        CartDTOs cart = new()
        //        {
        //            CartHeader = _mapper.Map<CartHeaderDTOs>(cartHeaderFromDb),
        //            CartDetails = new List<CartDetailsDTOs>()
        //        };

        //        // Lấy CartDetails
        //        cart.CartDetails = _mapper.Map<List<CartDetailsDTOs>>(
        //            await _db.CartDetails
        //                .AsNoTracking()
        //                .Where(u => u.CartHeaderID == cart.CartHeader.CartHeaderID)
        //                .ToListAsync()
        //        );

        //        if (!cart.CartDetails.Any())
        //        {
        //            _response.Result = cart;
        //            return _response;
        //        }

        //        // Lấy Product theo ID (tối ưu)
        //        var productIds = cart.CartDetails
        //            .Select(d => d.ProductID)
        //            .Distinct()
        //            .ToList();

        //        IEnumerable<ProductDTOs> products = await _productService
        //            .GetProductsByIds(productIds);

        //        Console.WriteLine("Products from service:");
        //        Console.WriteLine(JsonConvert.SerializeObject(products));


        //        cart.CartHeader.CartTotal = 0;

        //        // Map Product + tính tổng
        //        foreach (var item in cart.CartDetails)
        //        {
        //            var product = products.FirstOrDefault(p => p.ProductID == item.ProductID);
        //            if (product == null)
        //            {
        //                continue; // phòng thủ
        //            }

        //            item.Product = product;
        //            cart.CartHeader.CartTotal += item.Count * product.Price;
        //        }

        //        // Apply coupon
        //        if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
        //        {
        //            CouponDTOs coupon = await _couponService
        //                .GetCoupon(cart.CartHeader.CouponCode);

        //            if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
        //            {
        //                cart.CartHeader.Discount = coupon.DiscountAmount;
        //                cart.CartHeader.CartTotal -= coupon.DiscountAmount;
        //            }
        //        }

        //        _response.Result = cart;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //        Console.WriteLine("GetCart Error: " + ex.Message);
        //    }

        //    return _response;
        //}


        //[HttpGet("GetCart/{userId}")]
        //public async Task<ResponseDTO> GetCart(string userId)
        //{
        //    try
        //    {
        //        CartDTOs cart = new()
        //        {
        //            CartHeader = _mapper.Map<CartHeaderDTOs>(_db.CartHeaders.First(u => u.UserID == userId))
        //        };
        //        cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDTOs>>(_db.CartDetails
        //            .Where(u => u.CartHeaderID == cart.CartHeader.CartHeaderID));

        //        IEnumerable<ProductDTOs> productDtos = await _productService.GetProducts();

        //        foreach (var item in cart.CartDetails)
        //        {
        //            item.Product = productDtos.FirstOrDefault(u => u.ProductID == item.ProductID);
        //            cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
        //        }

        //        //apply coupon if any
        //        if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
        //        {
        //            CouponDTOs coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
        //            if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
        //            {
        //                cart.CartHeader.CartTotal -= coupon.DiscountAmount;
        //                cart.CartHeader.Discount = coupon.DiscountAmount;
        //            }
        //        }

        //        _response.Result = cart;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}
        #endregion

        // fix to json
        [HttpGet("GetCart/{userID}")]
        public async Task<ResponseDTO> GetCart(string userID)
        {
            try
            {
                var cartHeader = await _db.CartHeaders
                    .FirstOrDefaultAsync(u => u.UserID == userID);

                if (cartHeader == null)
                {
                    _response.Result = null;
                    _response.IsSuccess = true;
                    return _response;
                }

                CartDTOs cartDTOs = new CartDTOs()
                {
                    CartHeader = _mapper.Map<CartHeaderDTOs>(cartHeader),
                };

                var cartDetails = await _db.CartDetails
                    .Where(u => u.CartHeaderID == cartHeader.CartHeaderID)
                    .ToListAsync();


                cartDTOs.CartDetails = _mapper
                    .Map<IEnumerable<CartDetailsDTOs>>(cartDetails);

                Console.WriteLine($"Mapped CartDetails count: {cartDTOs.CartDetails.Count()}");

                //IEnumerable<ProductDTOs> products = new List<ProductDTOs>();
                var productIds = cartDTOs.CartDetails
                               .Select(cd => cd.ProductID)
                               .Distinct()
                               .ToList();

                //IEnumerable<ProductDTOs> products = await _productService.GetProductsByIds(productIds);

                Console.WriteLine($"ProductIDs: {string.Join(", ", productIds)}");


                //cartDTOs.CartHeader.CartTotal = 0;
                IEnumerable<ProductDTOs> products = await _productService.GetProductsByIds(productIds);

                Console.WriteLine($"Products from service: {products?.Count() ?? 0}");



                foreach (var item in cartDTOs.CartDetails)
                {
                    item.Product = products?.FirstOrDefault(p => p.ProductID == item.ProductID);
                    item.CartHeader = null;

                    if (item.Product != null)
                    {
                        Console.WriteLine($"Product {item.ProductID}: {item.Product.Name} - ${item.Product.Price}");
                    }
                    else
                    {
                        Console.WriteLine($"Product {item.ProductID}: NOT FOUND");
                    }
                }

                cartDTOs.CartHeader.CartTotal = cartDTOs.CartDetails
                            .Where(d => d.Product != null)
                            .Sum(d => d.Product.Price * d.Count);

                cartDTOs.CartHeader.Discount = 0;


                // Apply Coupon
                if (!string.IsNullOrEmpty(cartDTOs.CartHeader.CouponCode))
                {
                    try
                    {
                        Console.WriteLine("coupon calculate....");
                        CouponDTOs coupon = await _couponService.GetCoupon(cartDTOs.CartHeader.CouponCode);

                        if (coupon != null && cartDTOs.CartHeader.CartTotal >= coupon.MinAmount)
                        {
                            cartDTOs.CartHeader.Discount = coupon.DiscountAmount;
                            cartDTOs.CartHeader.CartTotal -= coupon.DiscountAmount;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Coupon error: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("no coupon");
                }


                Console.WriteLine($"CartTotal: ${cartDTOs.CartHeader.CartTotal}");


                _response.Result = cartDTOs;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDTOs cartDTOs)
        {
            try
            {
                var cartFromDb = await _db.CartHeaders.FirstAsync(u => u.UserID == cartDTOs.CartHeader.UserID);
                cartFromDb.CouponCode = cartDTOs.CartHeader.CouponCode;
                _db.CartHeaders.Update(cartFromDb);
                await _db.SaveChangesAsync();
                _response.Result = true;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }

            return _response;
        }
        
        [HttpPost("EmailCartRequest")]
        public async Task<object> EmailCartRequest([FromBody] CartDTOs cartDTOs)
        {
            try
            {
                await _messageBus.PublishMessage(cartDTOs, _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCart"));
                _response.Result = true;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }

            return _response;
        }
        
        
        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] CartDTOs cartDTOs)
        {
            try
            {
                var cartFromDb = await _db.CartHeaders.FirstAsync(u => u.UserID == cartDTOs.CartHeader.UserID);
                cartFromDb.CouponCode = "";
                _db.CartHeaders.Update(cartFromDb);
                await _db.SaveChangesAsync();
                _response.Result = true;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }

            return _response;
        }
        


        [HttpPost("CartUpsert")]
        public async Task<ResponseDTO> CartUpsert(CartDTOs cartDTO)
        {
            if (cartDTO?.CartHeader == null || string.IsNullOrEmpty(cartDTO.CartHeader.UserID))
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "UserID is required"
                };
            }

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
                Console.WriteLine("cartapicontroller", ex.Message.ToString());

            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDTO> RemoveCart([FromBody]int cartDetailsID)
        {
            try
            {
                CartDetails cartDetails = _db.CartDetails
                    .First(u => u.CartDetailsID == cartDetailsID);

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
