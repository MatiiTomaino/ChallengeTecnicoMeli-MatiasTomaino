using TraceIp.Model;

namespace TraceIp.Services.Interface
{
    public interface IRedisService
    {
        public bool Save(string key, string value);
        public Task<string> GetValueFromKey(string key);
        public bool SetRequestCache(ApiRestCountryResponse requestSummary);
        public IEnumerable<ApiRestCountryResponse>? GetRequestCache();
        public RequestSummaryList? GetRequestSummary();
        public bool SetRequestSummary(RequestSummaryList requestSummary);
    }
}
