using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServices.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
