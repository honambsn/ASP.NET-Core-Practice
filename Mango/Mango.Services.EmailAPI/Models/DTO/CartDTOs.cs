namespace Mango.Services.EmailAPI.Models.DTO
{
    public class CartDTOs
    {
        public CartHeaderDTOs CartHeader { get; set; }
        public IEnumerable<CartDetailsDTOs>? CartDetails { get; set; }
    }
}
