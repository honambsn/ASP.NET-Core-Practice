using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Web.Models
{
    public class VillaListViewModel
    {
        public List<Villa> Villas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}