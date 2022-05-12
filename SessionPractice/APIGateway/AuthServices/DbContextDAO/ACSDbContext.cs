using AuthServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServices.DbContextDAO
{
    public class ACSDbContext : DbContext
    {
        public ACSDbContext(DbContextOptions<ACSDbContext> options) : base(options)
        {
            Console.WriteLine("Inside ACSDbContext:DbContext");
        }
        public DbSet<User> UserACS { get; set; }
    }
}

