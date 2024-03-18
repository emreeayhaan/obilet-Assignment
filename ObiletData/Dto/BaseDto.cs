using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletData.Dto
{
    public class BaseDto
    {
        [JsonProperty("device-session")]
        public  SessionDataDto DeviceSession { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; } = DateTime.Now;
        [JsonProperty("language")]
        public string Language { get; set; } = "tr-TR";
    }
}
