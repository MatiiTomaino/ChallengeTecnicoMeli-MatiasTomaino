﻿using Newtonsoft.Json;

namespace TraceIp.Model
{
    public class Language
    {
        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("native")]
        public string? Native { get; set; }
    }

}
