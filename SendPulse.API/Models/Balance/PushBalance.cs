using Newtonsoft.Json;
using System;

namespace SendPulse.API.Models
{
    public class PushBalance
    {
        [JsonProperty("tariff_name")]
        public string TariffName { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("auto_renew")]
        public bool AutoRenew { get; set; }
    }
}
