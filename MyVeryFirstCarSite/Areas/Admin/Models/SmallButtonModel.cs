using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyVeryFirstCarSite.Areas.Admin.Models
{
    public class SmallButtonModel
    {
        public string Action { get; set; }
        public string Text { get; set; }
        public string Glyph { get; set; }
        public string ButtonType { get; set; }
        //the int below is nullable due to the "?"
        public int? Id { get; set; }
        public int? ItemId { get; set; }
        public int? VehicleId { get; set; }
        public int? SubscriptionId { get; set; }

        public string ActionParameters {
            get {
                //seeded with an initial value of the "?". When I build the URL, anything to the right of the "?" 
                //will be a list of parameters, sent with the URL to the server
                var param = new StringBuilder("?");
                if (Id != null && Id > 0)
                    param.Append(String.Format("{0}={1}&", "id", Id));

                if (ItemId != null && Id > 0)
                    param.Append(String.Format("{0}={1}&", "itemId", ItemId));

                if (VehicleId != null && Id > 0)
                    param.Append(String.Format("{0}={1}&", "vehicleId", VehicleId));

                if (SubscriptionId != null && Id > 0)
                    param.Append(String.Format("{0}={1}&", "subscriptionId", SubscriptionId));

                return param.ToString().Substring(0, param.Length - 1);
            }
        }
    }
}