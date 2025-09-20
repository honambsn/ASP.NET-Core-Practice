using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Web.ViewModels
{
    public class AmenityListVM
    {
        //public VillaNumber? VillaNumber { get; set; }
        //[ValidateNever]
        //public IEnumerable<SelectListItem>? VillaList { get; set; }

        public List<Amenity> Amenities { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages {  get; set; }
    }
}
