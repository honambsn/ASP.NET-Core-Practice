using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Models.DTOs
{
    public class CartDetailsDTOs
    {
        public int CartDetailsID { get; set; }
        public int CartHeaderID { get; set; }
        public CartDetailsDTOs? CartHeader { get; set; }
        public int ProductID { get; set; }
        public ProductDTOs? Product { get; set; }
        public int Count { get; set; }
    }
}
