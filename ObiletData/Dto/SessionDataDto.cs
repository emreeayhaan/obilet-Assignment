using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletData.Dto
{
    public class SessionDataDto
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }
    }
}
