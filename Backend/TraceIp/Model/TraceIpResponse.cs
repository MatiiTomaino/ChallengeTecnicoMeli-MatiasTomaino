using Newtonsoft.Json;

namespace TraceIp.Model
{
    public class TraceIpResponse
    {
        public string? Name { get; set; } 
        public string? IsoCode { get; set; }
        public List<Language>? Languages { get; set; }
        public List<string>? ActualTime { get; set; }
        public string? EstimatedDistance { get; set; }
        public Currency? Currency { get; set; }
        public string? Quotation { get; set; }
        public string? CurrencyAndQuotation { get; set; }
        public DateTime? DateTimeRequest { get; set; }
        public string? Ip { get; set; }
    }
}
