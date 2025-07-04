using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name is required.")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Required(ErrorMessage = "Display Order is required.")]
        [Range(0, 100, ErrorMessage = "Display Order must be between 0 and 100.")]
        public int DisplayOrder { get; set; }
    }
}
