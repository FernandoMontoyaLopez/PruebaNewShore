using FlightJourney.Entities;

namespace FlightJourney.Services.Interfaces
{
    public interface IJourneyService
    {
        Journey GetJourney(string origin, string destination, byte FlightsLimit);
    }
}
