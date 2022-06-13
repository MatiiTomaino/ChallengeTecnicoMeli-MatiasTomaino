using TraceIp.Model;

namespace TraceIp.Services.Interface
{
    public interface IRedisService
    {
        /// <summary>
        /// Save information
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public bool Save(string key, string value);

        /// <summary>
        /// Get Value from the key
        /// </summary>
        /// <param name="key">Key to look</param>
        /// <returns></returns>
        public Task<string> GetValueFromKey(string key);

        /// <summary>
        /// Save the request in redis
        /// </summary>
        /// <param name="requestSummary">datatype ApiRestCountryResponse</param>
        /// <returns></returns>
        public bool SetRequestCache(ApiRestCountryResponse requestSummary);

        /// <summary>
        /// Get the request in cache 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiRestCountryResponse>? GetRequestCache();

        /// <summary>
        /// Get a summary of requests by countries
        /// </summary>
        /// <returns></returns>
        public RequestSummaryList? GetRequestSummary();


        /// <summary>
        /// Save a request summary from any country
        /// </summary>
        /// <param name="requestSummary"></param>
        /// <returns></returns>
        public bool SetRequestSummary(RequestSummaryList requestSummary);
    }
}
