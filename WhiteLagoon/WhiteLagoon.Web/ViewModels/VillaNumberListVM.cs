using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Web.ViewModels
{
    public class VillaNumberListVM
    {
        public IEnumerable<VillaNumber> VillaNumbers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
