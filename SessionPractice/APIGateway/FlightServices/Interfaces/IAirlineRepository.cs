using FlightServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightServices.Interfaces
{
    public interface IAirlineRepository
    {
        bool AddAirline(Airline airline);
        List<Airline> GetAirlines();
        bool AddFlightInventory(FlightInventory flightInventory);
        List<FlightInventory> GetFlightInventory();
        List<FlightInventory> SearchFlights(string from = "", string to = "", DateTime? startDateTime = null, DateTime? endDateTime = null);
        bool BlockUnblockAirline(Airline airline);
        List<FlightInventory> GetFilteredFlightInventory(FilterFlightInventory filterFlightInventory);
        List<string> GetAvailableLocations();

    }
}
