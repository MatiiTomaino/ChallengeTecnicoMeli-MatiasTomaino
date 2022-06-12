using Newtonsoft.Json;

namespace TraceIp.Model
{
    public class CurrencyServicesResponse
    {
        [JsonProperty("result")]
        public string? Result { get; set; }

        [JsonProperty("documentation")]
        public Uri? Documentation { get; set; }

        [JsonProperty("terms_of_use")]
        public Uri? TermsOfUse { get; set; }

        [JsonProperty("time_last_update_unix")]
        public long TimeLastUpdateUnix { get; set; }

        [JsonProperty("time_last_update_utc")]
        public string? TimeLastUpdateUtc { get; set; }

        [JsonProperty("time_next_update_unix")]
        public long TimeNextUpdateUnix { get; set; }

        [JsonProperty("time_next_update_utc")]
        public string? TimeNextUpdateUtc { get; set; }

        [JsonProperty("base_code")]
        public string? BaseCode { get; set; }

        [JsonProperty("target_code")]
        public string? TargetCode { get; set; }

        [JsonProperty("conversion_rate")]
        public double ConversionRate { get; set; }
    }
}
