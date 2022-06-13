using Microsoft.Extensions.DependencyInjection;
using Moq;
using TraceIp.Model;
using TraceIp.Services.Implementation;
using TraceIp.Services.Interface;

namespace TraceIpTest
{
    [TestClass]
    public class TraceIpServiceTest
    {
        //Test example from challenge
        [TestMethod]
        public void TraceIp_Example_Test()
        {
            var traceIpServiceMock = new Mock<ITraceIpService>();
            var apiCountryServiceMock = new Mock<IApiCountryService>();
            var currencyServiceMock = new Mock<ICurrencyService>();
            var redisServiceMock = new Mock<IRedisService>();

            Currency currency = new Currency()
            {
                Code = "ARS",
                Name = "Argentine peso",
                Symbol = "$"
            };

            Language language = new Language()
            {
                Code = "ES",
                Name = "Spanish",
                Native = "Español"
            };

            apiCountryServiceMock.Setup(p => p.GetCountryInfoByIp("20.20.20.20"))
                .Returns(Task.FromResult(new ApiRestCountryResponse()
                {
                    CountryCode = "AR",
                    Location = new Location()
                    {
                        Languages = new List<Language>()
                        {
                            language
                        }
                    },
                    CountryName = "Argentina",
                    City = "Buenos Aires",
                    Ip = "20.20.20.20",
                    Latitude = 40,
                    Longitude = -4,
                    Timezones = new List<string>()
                    {
                        "UTC-03:00"
                    },
                    Currencies = new List<Currency>()
                    {
                        currency
                    }
                }));

            currencyServiceMock.Setup(p => p.ConvertCurrencyToUSD("ARS")).Returns(Task.FromResult(0.20));
            redisServiceMock.Setup(p => p.GetRequestCache()).Returns<IEnumerable<ApiRestCountryResponse>?>(null);

            TraceIpService traceIpService = new TraceIpService(apiCountryServiceMock.Object, redisServiceMock.Object, currencyServiceMock.Object);

            var result = traceIpService.TraceIp("20.20.20.20");

            Assert.AreEqual("Argentina", result.Name);
            Assert.AreEqual("AR", result.IsoCode);
            Assert.AreEqual("20.20.20.20", result.Ip);
            Assert.AreEqual("ARS (1 ARS = 0.2 USD)", result.CurrencyAndQuotation);
            Assert.AreEqual(10274.09, result.EstimatedDistance);
            Assert.AreEqual("AR", result.IsoCode);
            Assert.AreEqual("ARS", result.Currency.Code);
            Assert.AreEqual("$", result.Currency.Symbol);
            Assert.AreEqual("Argentine peso", result.Currency.Name);
            Assert.AreEqual("ES", result.Languages.FirstOrDefault().Code);
            Assert.AreEqual("Spanish", result.Languages.FirstOrDefault().Name);
            Assert.AreEqual("Español", result.Languages.FirstOrDefault().Native);
        }
    }
}
