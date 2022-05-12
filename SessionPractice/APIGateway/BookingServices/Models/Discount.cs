using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServices.Models
{
    public class Discount
    {
        public int? Id { get; set; }
        public string CouponCode { get; set; }
        public double Value { get; set; }
        public string DiscountType { get; set; }//% or Currency, etc
    }
}
