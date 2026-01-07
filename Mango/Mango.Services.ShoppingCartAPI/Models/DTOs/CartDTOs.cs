namespace Mango.Services.ShoppingCartAPI.Models.DTOs
{
    public class CartDTOs
    {
        public CartHeaderDTOs CartHeader { get; set; }
        public IEnumerable<CartDetailsDTOs>? CartDetails { get; set; }
    }
}
