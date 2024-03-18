using ObiletData.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletBusiness.Interfaces
{
    public interface ITravelService
    {
        Task<LocationsResponse> GetBusLocations(LocationRequestDto request);
        Task<JourneysResponseDto> GetBusJourneys(JourneysRequestDto request);
    }
}
