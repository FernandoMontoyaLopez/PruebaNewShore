using FlightJourney.Entities;
using FlightJourney.Services;
using FlightJourney.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightJourney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JourneyController : ControllerBase
    {

        private readonly ILogger<JourneyController>? _logger;
        private readonly IJourneyService? _journeyService;

        public JourneyController(ILogger<JourneyController> logger, IJourneyService journeyService)
        {
            try
            {
                _logger = logger;
                _journeyService = journeyService;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "500 - Server Error");
            }
        }

        [HttpGet(Name = "/calculate")]
        public IActionResult Calculate(string origin, string destination, byte flightsLimit)
        {
            try
            {
                return StatusCode(200, _journeyService?.GetJourney(origin, destination, flightsLimit));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "500 - Server Error");
                return StatusCode(500);
            }
}
    }
}