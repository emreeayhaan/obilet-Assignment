using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletData.Dto
{
    public class LocationRequestDto : BaseDto
    {
        [JsonProperty("data")]
        public string? Data { get; set; }
    }
}
