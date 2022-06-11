using Newtonsoft.Json;
using TraceIp.Model;
using TraceIp.Services.Interface;

namespace TraceIp.Services.Implementation
{
    public class ApiCountryService : IApiCountryService
    {
        private string GetIpApiUrl(string ip)
        {
            return "http://api.ipapi.com/api/" + ip + "?access_key=ad7ca2d83938e58c3ef8ab0d821c4805";
        }

        private string? GetRestCountriesApiUrl(string countryCode)
        {
            return $"https://restcountries.com/v2/alpha/{countryCode}?fields=currencies,timezones";
        }

        public async Task<ApiRestCountryResponse> GetCountryInfoByIp(string ip)
        {
            ApiRestCountryResponse? countryResponse = new ApiRestCountryResponse();
            ApiRestCountryResponseBase? countryResponseBase = new ApiRestCountryResponseBase();

            using (var httpClient = new HttpClient())
            {
                string? urlIp = GetIpApiUrl(ip);

                using (var response = await httpClient.GetAsync(urlIp))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    countryResponse = JsonConvert.DeserializeObject<ApiRestCountryResponse>(apiResponse);
                }

                string? urlCountry = GetRestCountriesApiUrl(countryResponse!.CountryCode!);

                using (var response = await httpClient.GetAsync(urlCountry))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    countryResponseBase = JsonConvert.DeserializeObject<ApiRestCountryResponseBase>(apiResponse);
                }

                countryResponse.Currencies = countryResponseBase!.Currencies;
                countryResponse.Timezones = countryResponseBase!.Timezones;
            }

            return countryResponse;
        }
    }
}
