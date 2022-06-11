using TraceIp.Helpers;
using TraceIp.Model;
using TraceIp.Services.Interface;

namespace TraceIp.Services.Implementation
{
    public class TraceIpService : ITraceIpService
    {
        private readonly IApiCountryService _apiCountryService;

        public TraceIpService(IApiCountryService apiCountryService)
        {
            _apiCountryService = apiCountryService;
        }

        public ResponseTraceIpDto TraceIp(string ip)
        {
            ApiRestCountryResponse restCountryResponse = _apiCountryService.GetCountryInfoByIp(ip).Result;

            return new ResponseTraceIpDto()
            {
                Name = restCountryResponse.CountryName,
                Ip = ip,
                Languages = restCountryResponse.Location!.Languages,
                Currency = restCountryResponse.Currencies!.FirstOrDefault(),
                EstimatedDistance = DistanceHelper.CalculatedDistanceToBuenosAires(restCountryResponse.Latitude, restCountryResponse.Longitude).ToString()
            };
        }
    }
}
