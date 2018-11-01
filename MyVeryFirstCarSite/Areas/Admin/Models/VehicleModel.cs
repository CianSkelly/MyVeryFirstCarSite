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
        public ICollection<VehicleLinkText> VehicleLinkTexts { get; set; }
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