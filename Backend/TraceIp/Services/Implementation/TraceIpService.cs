using TraceIp.Helpers;
using TraceIp.Model;
using TraceIp.Services.Interface;

namespace TraceIp.Services.Implementation
{
    public class TraceIpService : ITraceIpService
    {
        private readonly IApiCountryService _apiCountryService;
        private readonly ICurrencyService _currencyService;

        public TraceIpService(IApiCountryService apiCountryService,
            ICurrencyService currencyService)
        {
            _apiCountryService = apiCountryService;
            _currencyService = currencyService;
        }

        public TraceIpResponse TraceIp(string ip)
        {
            DateTime? dateTimeRequest = DateTime.Now;

            ApiRestCountryResponse restCountryResponse = _apiCountryService.GetCountryInfoByIp(ip).Result;

            if (restCountryResponse != null && restCountryResponse.CountryCode != null)
            {
                Currency currency = restCountryResponse.Currencies!.FirstOrDefault()!;
                string? currencyCode = currency.Code;
                string? quotationCurrency = Math.Round(_currencyService.ConvertCurrencyToUSD(currencyCode!).Result, 2).ToString();

                return new TraceIpResponse()
                {
                    Name = restCountryResponse.CountryName,
                    IsoCode = restCountryResponse.CountryCode,
                    Languages = restCountryResponse.Location!.Languages,
                    DateTimeRequest = dateTimeRequest,
                    ActualTime = DateTimeHelper.ConvertTimeZoneListToActualDateTimeList(restCountryResponse.Timezones!).Select(p => p.Value.ToShortTimeString() + $" ({p.Key})").ToList(),
                    Ip = ip,
                    Currency = currency,
                    CurrencyAndQuotation = currencyCode + $" (1 {currencyCode} = {quotationCurrency} USD)",
                    EstimatedDistance = DistanceHelper.CalculatedDistanceToBuenosAires((double)restCountryResponse.Latitude!, (double)restCountryResponse.Longitude!).ToString()
                };
            }

            throw new Exception("The request fail.");
        }
    }
}
