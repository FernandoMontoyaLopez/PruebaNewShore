using FlightJourney.Entities;
using FlightJourney.Repositories.Interfaces;
using FlightJourney.Services.Interfaces;
using System.Collections.Generic;

namespace FlightJourney.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IFlightRepository _flightRepo;

        public JourneyService(IFlightRepository flightRepo)
        {
            _flightRepo = flightRepo;
        }

        public Journey GetJourney(string origin, string destination, byte flightsLimit)
        {
            List<Flight> flights = _flightRepo.GetFlights();
            List<Flight> route = CalculateRoute(flights, origin, destination, flightsLimit);
            double price = CalculatePrice(route);
            return new Journey(route, origin, destination, price);
        }

        private double CalculatePrice(List<Flight> route)
        {
            double price = 0;
            if (route != null)
            {
               foreach (var flight in route)
                {
                    price += flight.Price;
                }
            }
            return price;
        }

        private List<Flight>? RecursiveRoute(List<Flight> flights, List<Flight> route, String finalDestination, byte flightsLimit)
        {
            if (flightsLimit > 0)
            {
                Flight last = route[route.Count - 1];
                List<Flight> destinations = flights.FindAll(flight => last.Destination == flight.Origin && flight.Destination != last.Origin);
                flights.RemoveAll(flight => last.Destination == flight.Origin && flight.Destination != last.Origin);
                foreach (var flight in destinations) {
                    if (flight.Destination == finalDestination)
                    {
                        route.Add(flight);   
                        return route;
                    }
                    List<Flight> branch = route.GetRange(0, route.Count);
                    RecursiveRoute(flights, branch, finalDestination, --flightsLimit);
                }
            }
            return null;
        }
   
        private List<Flight>? CalculateRoute(List<Flight> flights, String origin, String destination, byte flightsLimit)
        {        
            List<Flight> roots = flights.FindAll(flight => flight.Origin == origin);
            flights.RemoveAll(flight => flight.Origin == origin);
            foreach (var root in roots)
            {               
                List<Flight> route = new List<Flight>();
                route.Add(root);
                if (root.Destination == destination)
                {
                    return route;
                }
                route = RecursiveRoute(flights, route, destination, --flightsLimit);
                if (route != null)
                {
                    return route;
                }
            }            
            return null;
        }
    }
}
