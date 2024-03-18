using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletData.Dto
{
    public class JourneysResponseDto
    {
        public JourneysResponseDto()
        {
            Data = new List<JourneyData>();
        }
        [JsonProperty("status")]
        public required string Status { get; set; }
        [JsonProperty("data")]
        public List<JourneyData> Data { get; set; }
    }
    public class JourneyData
    {
        [JsonProperty("journey")]
        public Journey Journey { get; set; }
    }
    public class Journey
    {
        [JsonProperty("origin")]
        public string Origin { get; set; }
        [JsonProperty("destination")]
        public string Destination { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("internet-price")]
        public string Price { get; set; }
        [JsonProperty("arrival")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime Arrival { get; set; }
        [JsonProperty("departure")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime Departure { get; set; }
    }
}
