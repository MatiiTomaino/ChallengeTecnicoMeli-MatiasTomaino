using Newtonsoft.Json;

namespace TraceIp.Model
{
    /// <summary>
    /// response´s model from https://restcountries.com/v3.1/alpha/{Country_Code}?fields=currencies,timezones
    /// </summary>
    public class ApiRestCountryResponseBase
    {
        [JsonProperty("currencies")]
        public List<Currency>? Currencies { get; set; }

        [JsonProperty("timezones")]
        public List<string>? Timezones { get; set; }
    }


    /// <summary>
    /// response´s model from http://api.ipapi.com/api/
    /// </summary>
    public class ApiRestCountryResponse : ApiRestCountryResponseBase
    {
        [JsonProperty("ip")]
        public string? Ip { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("continent_code")]
        public string? ContinentCode { get; set; }

        [JsonProperty("continent_name")]
        public string? ContinentName { get; set; }

        [JsonProperty("country_code")]
        public string? CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string? CountryName { get; set; }

        [JsonProperty("region_code")]
        public string? RegionCode { get; set; }

        [JsonProperty("region_name")]
        public string? RegionName { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("zip")]
        public string? Zip { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("location")]
        public Location? Location { get; set; }
    }

    public class Location
    {
        [JsonProperty("geoname_id")]
        public long GeonameId { get; set; }

        [JsonProperty("capital")]
        public string? Capital { get; set; }

        [JsonProperty("languages")]
        public List<Language>? Languages { get; set; }

        [JsonProperty("country_flag")]
        public Uri? CountryFlag { get; set; }

        [JsonProperty("country_flag_emoji")]
        public string? CountryFlagEmoji { get; set; }

        [JsonProperty("country_flag_emoji_unicode")]
        public string? CountryFlagEmojiUnicode { get; set; }

        [JsonProperty("calling_code")]
        public long CallingCode { get; set; }

        [JsonProperty("is_eu")]
        public bool IsEu { get; set; }
    }
}
