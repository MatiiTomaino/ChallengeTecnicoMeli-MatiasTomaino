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

        public TraceIpResponse TraceIp(string ip)
        {
            DateTime? dateTimeRequest = DateTime.Now;

            ApiRestCountryResponse restCountryResponse = _apiCountryService.GetCountryInfoByIp(ip).Result;
            Currency currency = restCountryResponse.Currencies!.FirstOrDefault()!;
            string? currencyCode = currency.Code;
            string? quotationCurrency = "";

            return new TraceIpResponse()
            {
                Name = restCountryResponse.CountryName,
                IsoCode = restCountryResponse.CountryCode,
                Languages = restCountryResponse.Location!.Languages,
                DateTimeRequest = dateTimeRequest,
                ActualTime = DateTimeHelper.ConvertTimeZoneListToActualDateTimeList(restCountryResponse.Timezones!).Select(p => p.Value.ToShortTimeString() + $" ({p.Key})").ToList(),
                Ip = ip,
                Currency = currency,
                CurrencyAndQuotation = currencyCode + $" (1 {currencyCode} = {quotationCurrency} U$S)",
                EstimatedDistance = DistanceHelper.CalculatedDistanceToBuenosAires(restCountryResponse.Latitude, restCountryResponse.Longitude).ToString()
            };
        }
    }
}
