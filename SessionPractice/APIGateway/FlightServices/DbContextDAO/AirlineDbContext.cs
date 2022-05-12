using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightServices.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightServices.DbContextDAO
{
    public class AirlineDbContext:DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options)
        {
            Console.WriteLine("Inside AirlineDbContext:DbContext");
        }
        public DbSet<Airline> Airline { get; set; }        
        public DbSet<FlightInventory> FlightInventory { get; set; }        
    }
}
