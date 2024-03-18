using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletData.Dto
{
    public class JourneysRequestDto : BaseDto
    {
        [JsonProperty("data")]
        public required Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("origin-id")]
        public int OriginId { get; set; }

        [JsonProperty("destination-id")]
        public int DestinationId { get; set; }

        [JsonProperty("departure-date")]
        public required DateTime DepartureDate { get; set; }
    }
}
