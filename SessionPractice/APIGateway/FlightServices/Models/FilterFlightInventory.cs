using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightServices.Models
{
    public class FilterFlightInventory
    {
        public string FromPlace { get; set; } = "";
        public string ToPlace { get; set; } = "";
        public DateTime? OnwardJourneyDate { get; set; }
        public DateTime? ReturnJourneyDate { get; set; }
        public string MealType { get; set; } = "";
    }
}
