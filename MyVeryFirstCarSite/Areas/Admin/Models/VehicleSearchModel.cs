using MyVeryFirstCarSite.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyVeryFirstCarSite.Areas.Admin.Models
{
    public class VehicleSearchModel
    {
        [Display(Name = "Make & Model")]
        public string Title { get; set; }
        [Display(Name = "Price")]
        public string Price { get; set; }
        [DisplayName("Year")]
        public int ManufacturerYear { get; set; }
        [Display(Name = "County Sold From")]
        public County? CountySoldFrom { get; set; }
        [Display(Name = "Cubic Capicity")]
        public CC? CubicCapicity { get; set; }
        [DisplayName("Fuel Type")]
        public Fuel? FuelType { get; set; }
    }
}