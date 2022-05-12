using BookingServices.DbContextDAO;
using BookingServices.Interfaces;
using BookingServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServices.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _bookingDbContext;
        public BookingRepository(BookingDbContext bookingDbContext)
        {        
            this._bookingDbContext = bookingDbContext;
        }
        public void AddDiscount(Discount discount)
        {
            this._bookingDbContext.Add(discount);
            this._bookingDbContext.SaveChanges();
        }
        public List<Discount> GetDiscounts()
        {
            return this._bookingDbContext.Discounts.ToList();
        }
        
        public Nullable<Guid> BookTicketsAsPerJourney(string flightsIDs, List<BookTicket> bookTickets)
        {
            Guid guid = Guid.NewGuid();
            int[] arrFlightInventoryIds= flightsIDs.Split(",").Select(fi=>Convert.ToInt32(fi)).ToArray<int>();
            if (arrFlightInventoryIds.Length== bookTickets.Count && bookTickets.Count<2)
            {
                for (int i = 0; i < bookTickets.Count; i++)
                {
                    BookTicket ticket = bookTickets[i];
                    ticket.PNR = guid;
                    
                    foreach (var ticketPassager in ticket.passengers)
                    {
                        ticketPassager.BookTicketId = ticket.Id;
                        ticketPassager.PNR = guid;
                        ticketPassager.flightInventoryId = arrFlightInventoryIds[i];
                    }

                    _bookingDbContext.Tickets.Add(ticket);                    
                }
                _bookingDbContext.SaveChanges();
                return guid;
            }
            else
            {
                return null;
                //bad request
            }            
        }
        public List<BookTicket> GetBookedTicketsAsPerJourney(Guid PNR)
        {
            return _bookingDbContext.Tickets.Where(b=>b.PNR==PNR).ToList();
        }
        public List<BookTicket> GetAllBookedTickets()
        {
            return _bookingDbContext.Tickets.ToList();
        }

        public bool CancelTicket(Guid PNR)
        {        
            var tickets = _bookingDbContext.Tickets.Where(bs => bs.PNR == PNR).ToList();
            foreach (var ticket in tickets)
            {
                ticket.isCancelled = true;
                //_bookingDbContext.Entry(ticket).CurrentValues.SetValues(ticket);//Working
            }
            _bookingDbContext.UpdateRange(tickets);//Working
            _bookingDbContext.SaveChanges();
            return true;
        }

        public List<BookTicket> GetBookedFlightsHistory(string emailId)
        {
            var collectionTickets = _bookingDbContext.Tickets.Where(t => t.emailID == emailId);
            foreach (var ticket in collectionTickets)
            {
                ticket.passengers = _bookingDbContext.Passengers.Where(p => p.BookTicketId == ticket.Id).ToList();
            }
            List <BookTicket> lstBookTicket = collectionTickets.ToList();
            return lstBookTicket;
        }

        public List<BookTicket> GetTickedStatus(Guid PNR)
        {
            return _bookingDbContext.Tickets.Where(t => t.PNR == PNR).ToList();
        }
    }
}
