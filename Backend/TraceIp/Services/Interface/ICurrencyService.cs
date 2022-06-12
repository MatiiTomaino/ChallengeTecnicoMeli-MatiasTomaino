namespace TraceIp.Services.Interface
{
    public interface ICurrencyService
    {
        public Task<double> ConvertCurrencyToUSD(string currencyCode);
    }
}
