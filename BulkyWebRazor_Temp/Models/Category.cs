using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRazor_Temp.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name is required.")]
        [MaxLength(50, ErrorMessage = "Category Name cannot exceed 50 characters.")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Required(ErrorMessage = "Display Order is required.")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 0 and 100.")]
        public int DisplayOrder { get; set; }
    }
}
