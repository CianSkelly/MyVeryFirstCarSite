using MyVeryFirstCarSite.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyVeryFirstCarSite.Areas.Admin.Models
{
    public class VehicleModel
    {
        //"Id,Title,Description,ImageUrl,CubicCapicity,
        //FuelType,Colour,CountySoldFrom,ManufacturerYear,
        //NumberOfPreviousOwners,VehicleLinkTextId,VehicleTypeId"
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(1024)]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }
        public int VehicleLinkTextId { get; set; }
        public int VehicleTypeId { get; set; }
        [Required]
        [DisplayName("Price")]
        public string Price { get; set; }
        [Required]
        [DisplayName("Cubic Capicity")]
        public CC CubicCapicity { get; set; }
        [Required]
        [DisplayName("Fuel Type")]
        public Fuel FuelType { get; set; }
        [DisplayName("Colour")]
        public string Colour { get; set; }
        [DisplayName("County")]
        public County CountySoldFrom { get; set; }
        [Range(1980, 2018)]
        [DisplayName("Year")]
        public int ManufacturerYear { get; set; }
        [DisplayName("Number Of Previous Owners")]
        public int NumberOfPreviousOwners { get; set; }
        [DisplayName("Vehicle Link Text")]
        public ICollection<VehicleLinkText> VehicleLinkTexts { get; set; }
        [DisplayName("Vehicle Type")]
        public ICollection<VehicleType> VehicleTypes { get; set; }
        public string VehicleType {
            get
            {
                return VehicleTypes == null || VehicleTypes.Count.Equals(0) ?
                    String.Empty : VehicleTypes.First(vt => vt.Id.Equals(VehicleTypeId)).Title;
            }
        }

        public string VehicleLinkText
        {
            get
            {
                return VehicleLinkTexts == null || VehicleLinkTexts.Count.Equals(0) ?
                    String.Empty : VehicleLinkTexts.First(vt => vt.Id.Equals(VehicleLinkTextId)).Title;
            }
        }

    }
}