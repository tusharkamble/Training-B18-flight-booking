using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BookingServices.Models
{
    public class BookingSave
    {
        public string journeyType { get; set; }
        public string flightIDs { get; set; }
        public string userID { get; set; }
        public Guid PNR { get; set; }
        public bool isCancelled { get; set; }
    }
}
