using FlightJourney.DTO_s;
using FlightJourney.Entities;
using FlightJourney.Repositories.Interfaces;
using System.Text.Json;

namespace FlightJourney.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FlightRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public List<Flight> GetFlights()
        {
            List<FlightDTO> flightDto_s = CallAPI("2").Result.ToList();
            return ConvertToFlights(flightDto_s);
        }

        private List<Flight> ConvertToFlights(List<FlightDTO> flightDto_s)
        {
            List <Flight> flights = new List<Flight>();

            foreach (var dto in flightDto_s)
            {
                flights.Add(new Flight(
                    new Transport(dto.flightCarrier, dto.flightNumber),
                    dto.departureStation,
                    dto.arrivalStation,
                    dto.price
                    ));
            }
            return flights;
        }

        private async Task<IEnumerable<FlightDTO>> CallAPI(string endpoint)
        {
            var httpClient = _httpClientFactory?.CreateClient("NewshoreAPI");
            var httpResponseMessage = await httpClient.GetAsync(endpoint);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                IEnumerable<FlightDTO>? flightDto_s = await JsonSerializer.DeserializeAsync
                                    <IEnumerable<FlightDTO>>(contentStream);
                return flightDto_s;
            }
            throw new Exception("Server does not return data");
        }
    }
}
