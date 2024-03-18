using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletData.Dto
{
    public class LocationsResponse
    {
        public LocationsResponse()
        {
            Data = new List<LocationData>();
        }

        [JsonProperty("status")]
        public required string Status { get; set; }
        [JsonProperty("data")]
        public List<LocationData> Data { get; set; }
    }

    public class LocationData
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public required string Name { get; set; }
    }
}
