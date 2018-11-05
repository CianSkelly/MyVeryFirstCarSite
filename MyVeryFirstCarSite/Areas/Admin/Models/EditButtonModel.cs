using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyVeryFirstCarSite.Areas.Admin.Models
{
    public class EditButtonModel
    {
        public int ItemId { get; set; }
        public int VehicleId { get; set; }
        public int SubscriptionId { get; set; }
        public string Link {
            get
            {
                var s = new StringBuilder("?");
                if (ItemId > 0) s.Append(String.Format("{0}={1}&", "itemId", ItemId));
                if (VehicleId > 0) s.Append(String.Format("{0}={1}&", "vehicleId", VehicleId));
                if (SubscriptionId > 0) s.Append(String.Format("{0}={1}&", "subscriptionId", SubscriptionId));
                return s.ToString().Substring(0, s.Length - 1);
            }
        }

    }
}