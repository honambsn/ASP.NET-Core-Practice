using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.DTO;
using Mango.Services.ProductAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/Product")]
    //[Route("api/ProductAPI")] // hardcoded
    [ApiController]
    //[Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private IMapper _mapper;

        public ProductAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDTO();
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Product> objList = _db.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDTOs>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Product obj = _db.Products.First(u => u.ProductID == id);
                //ProductDTOs ProductDTOs = new ProductDTOs()
                //{
                //    ProductID = obj.ProductID,
                //    ProductCode = obj.ProductCode,
                //    DiscountAmount = obj.DiscountAmount,
                //    MinAmount = obj.MinAmount,
                //};
                _response.Result = _mapper.Map<ProductDTOs>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[HttpGet]
        //[Route("GetByCode/{code}")]
        //public ResponseDTO GetByCode(string code)
        //{
        //    try
        //    {
        //        Product obj = _db.Products.First(u => u.ProductCode.ToLower().Trim() == code.ToLower().Trim());
        //        //ProductDTOs ProductDTOs = new ProductDTOs()
        //        //{
        //        //    ProductID = obj.ProductID,
        //        //    ProductCode = obj.ProductCode,
        //        //    DiscountAmount = obj.DiscountAmount,
        //        //    MinAmount = obj.MinAmount,
        //        //};
        //        _response.Result = _mapper.Map<ProductDTOs>(obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        //[Route("GetByCode/{code}")]
        public ResponseDTO Post([FromBody] ProductDTOs ProductDTOs)
        {
            try
            {
                Product obj = _mapper.Map<Product>(ProductDTOs);
                _db.Products.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDTOs>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        //[Route("GetByCode/{code}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Put([FromBody] ProductDTOs ProductDTOs)
        {
            try
            {
                Product obj = _mapper.Map<Product>(ProductDTOs);
                _db.Products.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDTOs>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        //[Route("GetByCode/{code}")]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Delete(int id)
        {
            try
            {
                Product obj = _db.Products.First(u => u.ProductID == id);
                _db.Products.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
