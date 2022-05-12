using BookingServices.Interfaces;
using BookingServices.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;
        public BookingController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        [Route("GetDiscounts")]
        [HttpGet]
        public List<Discount> GetDiscounts()
        {
            return this.bookingRepository.GetDiscounts();
        }

        [Route("AddDiscounts")]
        [HttpPost]
        public void AddDiscount(Discount discount)
        {
            this.bookingRepository.AddDiscount(discount);
        }

        [Route("BookTicketsAsPerJourney/{flightsIDs}")]
        [HttpPost]
        public IActionResult BookTicketsAsPerJourney(string flightsIDs,[FromBody] List<BookTicket> bookTickets)//New
        {
            try
            {
                Nullable<Guid> guid = bookingRepository.BookTicketsAsPerJourney(flightsIDs, bookTickets);
                return Ok(new { Message= "Ticket booked successfully.",PNR=guid});
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }            
        }

        [Route("GetBookedTicketsAsPerJourney/{PNR}")]
        [HttpGet]
        public List<BookTicket> GetBookedTicketsAsPerJourney(Guid PNR)//New
        {
            return bookingRepository.GetBookedTicketsAsPerJourney(PNR);
        }
        [Route("GetAllBookedTickets")]
        [HttpGet]
        public List<BookTicket> GetAllBookedTickets(Guid PNR)//New
        {
            return bookingRepository.GetAllBookedTickets();
        }

        [Route("GetBookedFlightsHistory/{emailId}")]
        [HttpGet]
        public List<BookTicket> GetBookedFlightsHistory(string emailId)
        {
            return bookingRepository.GetBookedFlightsHistory(emailId);
        }

        [Route("CancelTicket/{PNR}")]
        [HttpDelete]
        public StatusCodeResult CancelTicket(Guid PNR)
        {
            if (bookingRepository.CancelTicket(PNR))
            {
                return Ok();
            }
            return new StatusCodeResult(404);
        }
        [Route("TicketStatus/{PNR}")]
        [HttpGet]
        public ActionResult TicketStatus(Guid PNR)
        {
            List<BookTicket> bs = bookingRepository.GetTickedStatus(PNR);
            if (bs is null || bs.Count==0)
            {
                return new StatusCodeResult(204);
            }
            return Ok(bs);
        }
    }
}
