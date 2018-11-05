using MyVeryFirstCarSite.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MyVeryFirstCarSite.Areas.Admin.Models
{
    public class VehicleItemModel
    {
        [DisplayName("Vehicle Id")]
        public int VehicleId { get; set; }
        [DisplayName("Item Id")]
        public int ItemId { get; set; }
        [DisplayName("Vehicle Title")]
        public string VehicleTitle { get; set; }
        [DisplayName("Item Title")]
        public string ItemTitle { get; set; }

        //2 X collections that will be used to display the values in the drop downs
        public ICollection<Vehicle>  Vehicles { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}