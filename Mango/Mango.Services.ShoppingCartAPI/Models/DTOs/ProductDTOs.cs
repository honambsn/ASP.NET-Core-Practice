namespace Mango.Services.ShoppingCartAPI.Models.DTOs
{
    public class ProductDTOs
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }
    }
}
