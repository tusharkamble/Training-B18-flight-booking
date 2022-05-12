using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServices.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public int BookTicketId { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public Guid? PNR { get; set; }
        public int? flightInventoryId { get; set; }
    }
}
