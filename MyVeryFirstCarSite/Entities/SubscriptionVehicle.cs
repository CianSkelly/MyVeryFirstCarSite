using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyVeryFirstCarSite.Entities
{
    [Table("SubscriptionVehicle")]
    public class SubscriptionVehicle
    {
        //the following VehicleId & ItemId will combine to make a composite primary key
        [Required]
        [Key, Column(Order = 1)]
        public int VehicleId { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int SubscriptionId { get; set; }
        //the following is not reflected in the database just in the class. entity Framework will not create any columns for these in the table:
        [NotMapped]
        public int OldVehicleId { get; set; }
        [NotMapped]
        public int OldSubscriptionId { get; set; }
    }
}
