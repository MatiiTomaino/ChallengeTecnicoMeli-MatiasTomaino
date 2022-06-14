using Newtonsoft.Json;
using TraceIp.Model;
using TraceIp.Services.Interface;

namespace TraceIp.Services.Implementation
{
    public class CurrencyService : ICurrencyService
    {
        public async Task<double> ConvertCurrencyToUSD(string currencyCode)
        {
            Console.WriteLine("Start Api Currency");
            CurrencyServicesResponse? currencyServiceResponse = new CurrencyServicesResponse();

            using (var httpClient = new HttpClient())
            {
                string? urlIp = $"https://v6.exchangerate-api.com/v6/de7f504251c8e63dcf45dea7/pair/{currencyCode}/USD";

                using (var response = await httpClient.GetAsync(urlIp))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    currencyServiceResponse = JsonConvert.DeserializeObject<CurrencyServicesResponse>(apiResponse);
                }

                Console.WriteLine("End Api Currency");
                return currencyServiceResponse!.ConversionRate;
            }
        }

    }
}
