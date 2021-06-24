using Newtonsoft.Json;

namespace SendPulse.API.Models
{
    public class EmailData : EmailBase
    {
        [JsonProperty("html")]
        public string HTML { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
