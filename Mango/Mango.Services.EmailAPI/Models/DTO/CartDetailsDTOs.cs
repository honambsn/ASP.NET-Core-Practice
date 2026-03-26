using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.EmailAPI.Models.DTO
{
    public class CartDetailsDTOs
    {
        public int CartDetailsID { get; set; }
        public int CartHeaderID { get; set; }
        public CartHeaderDTOs? CartHeader { get; set; }
        public int ProductID { get; set; }
        public ProductDTOs? Product { get; set; }
        public int Count { get; set; }
    }
}
