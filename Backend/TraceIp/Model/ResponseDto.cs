namespace TraceIp.Model
{
    public class ResponseDto
    {
        public string? Ip { get; set; }
        public string? RequestDate { get; set; }
        public string? RequestHour { get; set; }
        public string? Country { get; set; }
        public string? IsoCode { get; set; }
        public List<string>? Languages { get; set; }
        public string? Currency { get; set; } 
        public string? EstimatedDistance { get; set; }
    }
}
