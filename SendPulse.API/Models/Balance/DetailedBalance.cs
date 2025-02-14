﻿using Newtonsoft.Json;

namespace SendPulse.API.Models
{
    public class DetailedBalance
    {
        [JsonProperty("balance")]
        public UserBalance Balance { get; set; }

        [JsonProperty("email")]
        public EmailBalance Email { get; set; }

        [JsonProperty("smtp")]
        public SMTPBalance SMTP { get; set; }

        [JsonProperty("push")]
        public PushBalance Push { get; set; }
    }
}
