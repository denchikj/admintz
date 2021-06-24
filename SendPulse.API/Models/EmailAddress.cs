using Newtonsoft.Json;

namespace SendPulse.API.Models
{
    public class EmailAddress
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Address { get; set; }
    }
}
