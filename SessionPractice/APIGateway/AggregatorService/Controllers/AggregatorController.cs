using AggregatorService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AggregatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AggregatorController : Controller
    {
        [Route("get-aggregated-Flight-details")]
        [HttpGet]
        public IActionResult GetAggregatedFlightDetails()
        {
            try
            {
                //Setting TLS 1.2 protocol
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //Fetch the JSON string from URL.                        
                List<FlightInventory> lstFlightInventory = new List<FlightInventory>();
                List<Airline> lstAirlines = new List<Airline>();
                //string apiUrl1 = "http://localhost:9000/api/airline/GetAirlines";
                //string apiUrl2 = "http://localhost:9000/api/airline/GetFlightInventory";
                string apiUrl1 = "http://localhost:9006/api/airline/GetAirlines";
                string apiUrl2 = "http://localhost:9006/api/airline/GetFlightInventory";

                HttpClient client = new HttpClient();
                HttpResponseMessage response1 = client.GetAsync(apiUrl1).Result;
                HttpResponseMessage response2 = client.GetAsync(apiUrl2).Result;
                if (response1.IsSuccessStatusCode)
                {
                    lstAirlines = JsonConvert.DeserializeObject<List<Airline>>(response1.Content.ReadAsStringAsync().Result);
                }
                if (response2.IsSuccessStatusCode)
                {
                    lstFlightInventory = JsonConvert.DeserializeObject<List<FlightInventory>>(response2.Content.ReadAsStringAsync().Result);
                }
                if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
                {
                    var aggResult = from f in lstFlightInventory
                                    join a in lstAirlines
                                    on f.AirlineID equals a.Id
                                    select new
                                    {
                                        FlightInventoryId = f.FlightInventoryId,
                                        FlightNumber = f.FlightNumber,
                                        AirlineID = f.AirlineID,
                                        AirlineName = a.Name,
                                        FromPlace = f.FromPlace,
                                        ToPlace = f.ToPlace,
                                        StartDateTime = f.StartDateTime,
                                        EndDateTime = f.EndDateTime,
                                        ScheduleDays = f.ScheduleDays,
                                        InstrumentUsed = f.InstrumentUsed,
                                        BussinesSteatsCount = f.BussinesSteatsCount,
                                        NonBussinesSeatCount = f.NonBussinesSeatCount,
                                        TicketCost = f.TicketCost,
                                        NoofRows = f.NoofRows,
                                        MealType = f.MealType
                                    };
                    return Json(aggResult);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }

            return new StatusCodeResult(204);
        }
    }

}
