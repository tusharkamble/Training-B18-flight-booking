using FlightServices.DbContextDAO;
using FlightServices.Interfaces;
using FlightServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FlightServices.Repositories
{
    public class AirlineRepository : IAirlineRepository
    {
        //private readonly List<Airline> lstAirlines;
        //private readonly List<FlightInventory> lstFlightInventory;
        private readonly AirlineDbContext _airlineDbContext;
        public AirlineRepository(AirlineDbContext dbContext)
        {
            //lstAirlines = new List<Airline>();
            //lstFlightInventory = new List<FlightInventory>();
            _airlineDbContext = dbContext;
        }
        public bool AddAirline(Airline airline)
        {
            //lstAirlines.Add(airline);
            try
            {
                Airline matchedAirline = _airlineDbContext.Airline.Where(a => a.Name.ToUpper() == airline.Name.ToUpper()).FirstOrDefault();
                if (matchedAirline == null)
                {
                    _airlineDbContext.Airline.Add(airline);
                    _airlineDbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Airline> GetAirlines()
        {
            return _airlineDbContext.Airline.ToList();
        }
      
        public bool AddFlightInventory(FlightInventory flightInventory)
        {
            //lstFlightInventory.Add(flightInventory);
            try
            {                
                _airlineDbContext.FlightInventory.Add(flightInventory);
                _airlineDbContext.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<FlightInventory> GetFlightInventory()
        {
            //return lstFlightInventory;
            return _airlineDbContext.FlightInventory.ToList();
        }

        public List<FlightInventory> SearchFlights(string from="", string to="", DateTime? startDateTime=null, DateTime? endDateTime = null)
        {
            //return lstFlightInventory.Where(f=>
            //    (string.IsNullOrEmpty(from) || from==f.FromPlace)
            //    && (string.IsNullOrEmpty(to) || to==f.ToPlace)
            //    && (startDateTime is null || startDateTime == f.StartDateTime)
            //    && (endDateTime is null || endDateTime==f.EndDateTime)
            //).ToList();
            return _airlineDbContext.FlightInventory.Where(f=>
                (string.IsNullOrEmpty(from) || from==f.FromPlace)
                && (string.IsNullOrEmpty(to) || to==f.ToPlace)
                && (startDateTime == null || startDateTime == f.StartDateTime)
                && (endDateTime == null || endDateTime==f.EndDateTime)
            ).ToList();
        }
        public List<FlightInventory> GetFilteredFlightInventory(FilterFlightInventory filterFlightInventory)
        {
            return _airlineDbContext.FlightInventory.Where(f =>
                (string.IsNullOrEmpty(filterFlightInventory.FromPlace) || filterFlightInventory.FromPlace == f.FromPlace)
                && (string.IsNullOrEmpty(filterFlightInventory.ToPlace) || filterFlightInventory.ToPlace == f.ToPlace)
                && (filterFlightInventory.OnwardJourneyDate == null || filterFlightInventory.OnwardJourneyDate <= f.StartDateTime)
                && (filterFlightInventory.ReturnJourneyDate == null || filterFlightInventory.ReturnJourneyDate <= f.EndDateTime)
            ).ToList();
        }

        public bool BlockUnblockAirline(Airline airline)
        {
            var dbAirline = _airlineDbContext.Airline.Where(a => a.Id == airline.Id).FirstOrDefault();
            if (dbAirline is null || dbAirline.IsBlocked==airline.IsBlocked)
            {
                return false;
            }
            else
            {
                dbAirline.IsBlocked = dbAirline.IsBlocked==null || dbAirline.IsBlocked==false? true:false;
                _airlineDbContext.Update(dbAirline);//Working
                _airlineDbContext.SaveChanges();
                return true;
            }
        }

        public List<string> GetAvailableLocations()
        {
            var lstFromPlace = _airlineDbContext.FlightInventory.Select(f => f.FromPlace).ToList();
            var lstToPlace = _airlineDbContext.FlightInventory.Select(f => f.ToPlace).ToList();
            var allLocations = lstFromPlace.Union(lstToPlace).Distinct().ToList();
            return allLocations;
        }
    }
}
