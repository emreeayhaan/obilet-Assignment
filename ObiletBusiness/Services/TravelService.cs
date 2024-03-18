using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ObiletBusiness.Interfaces;
using ObiletData.Dto;

namespace ObiletBusiness.Services
{
    public class TravelService : ITravelService
    {
        private readonly Options _enviroment;
        private readonly IRedisCacheManager _cacheBusiness;
        private readonly ICustomHttpClient _httpClient;

        public TravelService(IOptions<Options> enviroment, IRedisCacheManager cacheBusiness, ICustomHttpClient httpclient)
        {
            _cacheBusiness = cacheBusiness;
            _httpClient = httpclient;
            _enviroment = enviroment.Value;
        }
        public async Task<LocationsResponse> GetBusLocations(LocationRequestDto request)
        {
            try
            {
                //string ApiUrl = Environment.GetEnvironmentVariable("ApiUri") + Environment.GetEnvironmentVariable("GetLocations");
                string ApiUrl = $"{_enviroment.ApiUri}{_enviroment.GetLocations}";
                //string token = Environment.GetEnvironmentVariable("Token");

                HttpResponseMessage httpResponse = await _httpClient.SendPostRequest(request, ApiUrl, _enviroment.Token);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return new LocationsResponse() { Status = "Fail" };
                }

                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<LocationsResponse>(responseContent);
                if (response != null)
                {
                    response.Data = response.Data.Take(10).ToList();
                }
                return response ?? new LocationsResponse() { Status = "Fail" };
            }
            catch
            {
                return new LocationsResponse() { Status = "Fail" };
            }
        }

        public async Task<JourneysResponseDto> GetBusJourneys(JourneysRequestDto request)
        {
            try
            {
                await Task.WhenAll(
                   _cacheBusiness.Set("originId", request.Data.OriginId),
                   _cacheBusiness.Set("destinationId", request.Data.DestinationId),
                   _cacheBusiness.Set("selectedDate", request.Data.DepartureDate)
               );

                //string ApiUrl = Environment.GetEnvironmentVariable("ApiUri") + Environment.GetEnvironmentVariable("GetJourneys");

                string ApiUrl = $"{_enviroment.ApiUri}{_enviroment.GetJourneys}";
                //string token = Environment.GetEnvironmentVariable("Token");

                HttpResponseMessage httpResponse = await _httpClient.SendPostRequest(request, ApiUrl, _enviroment.Token);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return new JourneysResponseDto() { Status = "false" };
                }

                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<JourneysResponseDto>(responseContent);
                if (response != null)
                {
                    response.Data = response.Data.OrderBy(i => i.Journey.Departure).ToList();
                }
                return response ?? new JourneysResponseDto() { Status = "Fail" };
            }
            catch
            {
                return new JourneysResponseDto() { Status = "Fail" };
            }
        }
    }
}
