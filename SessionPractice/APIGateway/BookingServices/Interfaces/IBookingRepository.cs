using BookingServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServices.Interfaces
{
    public interface IBookingRepository
    {
        void AddDiscount(Discount discount);
        List<Discount> GetDiscounts();
        //void BookFlights(string flightsIDs);
        List<BookTicket> GetBookedFlightsHistory(string emailId);
        List<BookTicket> GetTickedStatus(Guid PNR);
        bool CancelTicket(Guid PNR);
        Nullable<Guid> BookTicketsAsPerJourney(string flightIDs, List<BookTicket> bookTickets);
        List<BookTicket> GetBookedTicketsAsPerJourney(Guid PNR);
        List<BookTicket> GetAllBookedTickets();
    }
}
