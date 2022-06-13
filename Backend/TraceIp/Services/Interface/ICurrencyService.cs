namespace TraceIp.Services.Interface
{
    public interface ICurrencyService
    {
        /// <summary>
        /// Convert 1 unit of some currency to USD
        /// </summary>
        /// <param name="currencyCode">Code of currency to convert</param>
        /// <returns></returns>
        public Task<double> ConvertCurrencyToUSD(string currencyCode);
    }
}
