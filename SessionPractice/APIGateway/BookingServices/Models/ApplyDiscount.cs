using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServices.Models
{
    public class ApplyDiscount
    {
        public string CouponCode { get; set; }
        public string ReturnJourneySeat { get; set; }
        public string OnwardJourneySeat { get; set; }
    }
}
