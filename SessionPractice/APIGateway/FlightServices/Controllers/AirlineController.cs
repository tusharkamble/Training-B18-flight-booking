using FlightServices.Interfaces;
using FlightServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private IAirlineRepository _airlineRepository;
        public AirlineController(IAirlineRepository airlineRepository)
        {
            _airlineRepository = airlineRepository;
        }
        
        [HttpPost]
        [Route("AddAirline")]
        public IActionResult AddAirline(Airline airline)
        {
            try
            {
                bool isAdded = _airlineRepository.AddAirline(airline);
                if (isAdded)
                {
                    return Ok();
                }
                return new ObjectResult(new { Message = "Airline already exists." }) { StatusCode = 400 };
            }
            catch (SqlException ex)
            {
                return new StatusCodeResult(500);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        
        [HttpGet]
        [Route("GetAirlines")]
        public IActionResult GetAirlines()
        {
            try
            {
                return Ok( _airlineRepository.GetAirlines());
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [Route("AddFlightInventory")]
        [HttpPost]
        public IActionResult AddFlightInventory([FromBody]FlightInventory flightInventory)
        {
            try
            {
                //return (new StatusCodeResult(400));
                //return StatusCode(400, "");
                if (flightInventory.StartDateTime > flightInventory.EndDateTime
                    || flightInventory.StartDateTime == flightInventory.EndDateTime
                    || flightInventory.StartDateTime<DateTime.Today)
                {
                    return new ObjectResult(new { Message = "Invalid Start Date Time & End Date Time." }) { StatusCode = 400 };
                }
                if (flightInventory.FromPlace == flightInventory.ToPlace)
                {
                    return new ObjectResult(new { Message = "From Place and To Place should not be same." }) { StatusCode = 400 };
                }
                return _airlineRepository.AddFlightInventory(flightInventory) 
                    ? Ok() : (new StatusCodeResult(400));
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        [Route("GetFlightInventory")]
        [HttpGet]
        public IActionResult GetFlightInventory()
        {
            try
            {
                return Ok(_airlineRepository.GetFlightInventory());
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        
        [Route("SearchFlights")]
        [HttpGet]
        public IActionResult SearchFlights(string from = "", string to = "", DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            try
            {
                return Ok(_airlineRepository.SearchFlights(from, to, startDateTime, endDateTime));
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        [Route("GetFilteredFlightInventory")]
        [HttpPost]
        public IActionResult GetFilteredFlightInventory(FilterFlightInventory filterFlightInventory)
        {
            try
            {
                return Ok(_airlineRepository.GetFilteredFlightInventory(filterFlightInventory));
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("BlockUnblockAirline")]
        public IActionResult BlockUnblockAirline(Airline airline)
        {
            try
            {
                bool isBlocked = _airlineRepository.BlockUnblockAirline(airline);
                if (isBlocked)
                {
                    return Ok(new { Message = "Operation successfull." });
                }
                return new ObjectResult(new { Message = "Bad Request." }) { StatusCode = 400 };
            }
            catch (SqlException ex)
            {
                return new StatusCodeResult(500);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        [Route("GetAvailableLocations")]
        public IActionResult GetAvailableLocations()
        {
            try
            {
                var lstLocations =_airlineRepository.GetAvailableLocations();
                return Ok(lstLocations);
            }
            catch (SqlException ex)
            {
                return new StatusCodeResult(500);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

    }
    
}
