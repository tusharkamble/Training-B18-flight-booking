using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServices.Models
{
    public class BookTicket
    {
        public int Id { get; set; }
        public string journeyType { get; set; }
        public string flightIDs { get; set; }
        public string userID { get; set; }
        public Guid? PNR { get; set; } = null;
        public bool isCancelled { get; set; }
        public ICollection<Passenger> passengers { get; set; }
        public int numberOfSeats { get; set; }
        public string name { get; set; }
        public string emailID { get; set; }
        public string mealPreference { get; set; }
        public string selectedSeats { get; set; }

    }
}
