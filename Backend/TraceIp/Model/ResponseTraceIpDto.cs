using Newtonsoft.Json;

namespace TraceIp.Model
{
    public class ResponseTraceIpDto
    {
        public string? Name { get; set; }
        public string? Ip { get; set; }
        public List<Language>? Languages { get; set; }
        public List<DateTime>? HourList { get; set; }
        public string? EstimatedDistance { get; set; }
        public Currency? Currency { get; set; }
    }
}
