namespace Mango.Web.Models
{
    public class CartDTOs
    {
        public CartHeaderDTOs CartHeader { get; set; }
        public IEnumerable<CartDetailsDTOs>? CartDetails { get; set; }
    }
}
