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
        [Display(Name = "Make")]
        public string Make { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Max Price")]
        public int? Price { get; set; }
        [DisplayName("Min Year")]
        public int? MinManufacturerYear { get; set; }
        [DisplayName("Max Year")]
        public int? MaxManufacturerYear { get; set; }
        [Display(Name = "County Sold From")]
        public County? CountySoldFrom { get; set; }
        //[Display(Name = "Max Mileage")]
        //public int? Mileage { get; set; }
    }
}