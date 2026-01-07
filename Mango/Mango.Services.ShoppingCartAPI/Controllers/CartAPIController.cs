using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public CartAPIController(AppDbContext db,
            IMapper mapper)
        {
            _db = db;
            this._response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDTO> CartUpsert(CartDTOs cartDTO)
        {

        }
    }
}
