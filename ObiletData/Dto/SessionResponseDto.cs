using Newtonsoft.Json;

namespace ObiletData.Dto
{
    public class SessionResponseDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("data")]
        public SessionDataDto Data { get; set; }
    }
}
