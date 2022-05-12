using BookingServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServices.DbContextDAO
{
    public class BookingDbContext:DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
            Console.WriteLine("Inside BookingDbContext:DbContext");
        }
        public DbSet<Discount> Discounts { get; set; }
        //public DbSet<BookingSave> BookingSaves { get; set; }
        public DbSet<BookTicket> Tickets { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
    }
}
