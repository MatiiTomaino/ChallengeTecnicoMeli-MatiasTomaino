using TraceIp.Model;

namespace TraceIp.Services.Interface
{
    public interface IApiCountryService
    {
        /// <summary>
        /// Get information of countries from externals api's.
        /// </summary>
        /// <param name="ip">Value of ip to look information</param>
        /// <returns></returns>
        public Task<ApiRestCountryResponse> GetCountryInfoByIp(string ip);
    }
}
