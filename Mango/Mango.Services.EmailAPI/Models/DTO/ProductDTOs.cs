using System.ComponentModel.DataAnnotations;

namespace Mango.Services.EmailAPI.Models.DTO
{
    public class ProductDTOs
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }

        //[Range(1,100)]
        public int Count { get; set; } = 1;
    }
}
