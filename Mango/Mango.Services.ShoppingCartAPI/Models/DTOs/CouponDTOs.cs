namespace Mango.Services.ShoppingCartAPI.Models.DTO
{
    public class CouponDTOs
    {
        public int CouponID { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
    