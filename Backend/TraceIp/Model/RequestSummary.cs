namespace TraceIp.Model
{
    public class RequestSummaryList
    {
        public List<RequestSummary>? Items { get; set; }
    }

    public class RequestSummary
    {
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public double Distance { get; set; }
        public int? RequestCount { get; set; }
    }
}
