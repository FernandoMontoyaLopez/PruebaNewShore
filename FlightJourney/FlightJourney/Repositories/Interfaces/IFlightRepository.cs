using FlightJourney.Entities;

namespace FlightJourney.Repositories.Interfaces
{
        public interface IFlightRepository {
        List<Flight> GetFlights();
        }
}
