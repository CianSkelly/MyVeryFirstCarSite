using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyVeryFirstCarSite.Entities
{
    public class UserSubscription
    {
        //SubscriptionId and UserId combine to become parts of a composite key, ordered as signified by 1 & 2
        [Required]
        [Key, Column(Order = 1)]
        public int SubscriptionId { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        [MaxLength(128)]
        public string UserId { get; set; }
        //The "?" below signifies the follwoing property is nullable
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}