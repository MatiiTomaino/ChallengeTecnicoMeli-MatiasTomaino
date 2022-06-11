using TraceIp.Model;

namespace TraceIp.Services.Interface
{
    public interface IApiCountryService
    {
        public Task<ApiRestCountryResponse> GetCountryInfoByIp(string ip);
    }
}
