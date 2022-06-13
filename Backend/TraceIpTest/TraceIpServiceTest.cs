using Moq;
using TraceIp.Exceptions;
using TraceIp.Model;
using TraceIp.Services.Implementation;
using TraceIp.Services.Interface;

namespace TraceIpTest
{
    [TestClass]
    public class TraceIpServiceTest
    {
        private RequestSummaryList summaryList = new RequestSummaryList()
        {
            Items = new List<RequestSummary>()
                {
                    new RequestSummary()
                    {
                        CountryCode = "ES",
                        CountryName = "Spain",
                        Distance = 1234.25,
                        RequestCount = 1
                    },
                    new RequestSummary()
                    {
                        CountryCode = "AR",
                        CountryName = "Argentine",
                        Distance = 500,
                        RequestCount = 1
                    }
                }
        };

        private static Currency currency = new Currency()
        {
            Code = "ARS",
            Name = "Argentine peso",
            Symbol = "$"
        };

        private static Language language = new Language()
        {
            Code = "ES",
            Name = "Spanish",
            Native = "Español"
        };

        private ApiRestCountryResponse apiRestCountryResponseArgentina = new ApiRestCountryResponse()
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
        };

        //Test example from challenge
        [TestMethod]
        public void TraceIp_InfoFromApi_Test()
        {
            var traceIpServiceMock = new Mock<ITraceIpService>();
            var apiCountryServiceMock = new Mock<IApiCountryService>();
            var currencyServiceMock = new Mock<ICurrencyService>();
            var redisServiceMock = new Mock<IRedisService>();

            apiCountryServiceMock.Setup(p => p.GetCountryInfoByIp("20.20.20.20"))
                .Returns(Task.FromResult(apiRestCountryResponseArgentina));

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

        [TestMethod]
        public void TraceIp_InfoFromCache_Test()
        {
            var traceIpServiceMock = new Mock<ITraceIpService>();
            var apiCountryServiceMock = new Mock<IApiCountryService>();
            var currencyServiceMock = new Mock<ICurrencyService>();
            var redisServiceMock = new Mock<IRedisService>();

            apiCountryServiceMock.Setup(p => p.GetCountryInfoByIp("20.20.20.20"))
                .Returns(Task.FromResult(apiRestCountryResponseArgentina));

            redisServiceMock.Setup(p => p.GetRequestCache())
                .Returns(new List<ApiRestCountryResponse>()
                {
                    new ApiRestCountryResponse()
                    {
                        CountryCode = "AR",
                        Location = new Location()
                        {
                            Languages = new List<Language>()
                            {
                                language
                            }
                        },
                        CountryName = "Brasil",
                        City = "Rio de janeiro",
                        Ip = "30.30.30.30",
                        Latitude = 40,
                        Longitude = -4,
                        Timezones = new List<string>()
                        {
                            "UTC-02:00"
                        },
                        Currencies = new List<Currency>()
                        {
                            currency
                        }
                    }
                }.AsEnumerable());

            currencyServiceMock.Setup(p => p.ConvertCurrencyToUSD("ARS")).Returns(Task.FromResult(0.20));

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

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void TraceIp_BadRequestException_Test()
        {
            var traceIpServiceMock = new Mock<ITraceIpService>();
            var apiCountryServiceMock = new Mock<IApiCountryService>();
            var currencyServiceMock = new Mock<ICurrencyService>();
            var redisServiceMock = new Mock<IRedisService>();

            apiCountryServiceMock.Setup(p => p.GetCountryInfoByIp("20.20.20.20"))
                .Returns<ApiRestCountryResponse>(null);

            redisServiceMock.Setup(p => p.GetRequestCache())
                .Returns<ApiRestCountryResponse>(null);

            currencyServiceMock.Setup(p => p.ConvertCurrencyToUSD("ARS")).Returns(Task.FromResult(0.20));

            TraceIpService traceIpService = new TraceIpService(apiCountryServiceMock.Object, redisServiceMock.Object, currencyServiceMock.Object);

            traceIpService.TraceIp("sarasa");
        }

        [TestMethod]
        public void GetFurthestDistance_Test()
        {
            var traceIpServiceMock = new Mock<ITraceIpService>();
            var apiCountryServiceMock = new Mock<IApiCountryService>();
            var currencyServiceMock = new Mock<ICurrencyService>();
            var redisServiceMock = new Mock<IRedisService>();

            redisServiceMock.Setup(p => p.GetRequestSummary()).Returns(summaryList);

            TraceIpService traceIpService = new TraceIpService(apiCountryServiceMock.Object, redisServiceMock.Object, currencyServiceMock.Object);

            var result = traceIpService.GetFurthestDistance();

            Assert.AreEqual("Spain", result.CountryName);
            Assert.AreEqual(1234.25, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(WithoutRequestException))]
        public void GetFurthestDistance_WithoutRequestException_Test()
        {
            var traceIpServiceMock = new Mock<ITraceIpService>();
            var apiCountryServiceMock = new Mock<IApiCountryService>();
            var currencyServiceMock = new Mock<ICurrencyService>();
            var redisServiceMock = new Mock<IRedisService>();

            redisServiceMock.Setup(p => p.GetRequestSummary()).Returns<RequestSummaryList?>(null);

            TraceIpService traceIpService = new TraceIpService(apiCountryServiceMock.Object, redisServiceMock.Object, currencyServiceMock.Object);

            var result = traceIpService.GetFurthestDistance();
        }

        [TestMethod]
        public void GetClosestDistance_Test()
        {
            var traceIpServiceMock = new Mock<ITraceIpService>();
            var apiCountryServiceMock = new Mock<IApiCountryService>();
            var currencyServiceMock = new Mock<ICurrencyService>();
            var redisServiceMock = new Mock<IRedisService>();

            redisServiceMock.Setup(p => p.GetRequestSummary()).Returns(summaryList);

            TraceIpService traceIpService = new TraceIpService(apiCountryServiceMock.Object, redisServiceMock.Object, currencyServiceMock.Object);

            var result = traceIpService.GetClosestDistance();

            Assert.AreEqual("Argentine", result.CountryName);
            Assert.AreEqual(500, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(WithoutRequestException))]
        public void GetClosestDistance_WithoutRequestException_Test()
        {
            var traceIpServiceMock = new Mock<ITraceIpService>();
            var apiCountryServiceMock = new Mock<IApiCountryService>();
            var currencyServiceMock = new Mock<ICurrencyService>();
            var redisServiceMock = new Mock<IRedisService>();

            redisServiceMock.Setup(p => p.GetRequestSummary()).Returns<RequestSummaryList?>(null);

            TraceIpService traceIpService = new TraceIpService(apiCountryServiceMock.Object, redisServiceMock.Object, currencyServiceMock.Object);

            var result = traceIpService.GetClosestDistance();
        }

        [TestMethod]
        public void GetAverageDistance_Test()
        {
            var traceIpServiceMock = new Mock<ITraceIpService>();
            var apiCountryServiceMock = new Mock<IApiCountryService>();
            var currencyServiceMock = new Mock<ICurrencyService>();
            var redisServiceMock = new Mock<IRedisService>();

            redisServiceMock.Setup(p => p.GetRequestSummary()).Returns(summaryList);

            TraceIpService traceIpService = new TraceIpService(apiCountryServiceMock.Object, redisServiceMock.Object, currencyServiceMock.Object);

            var result = traceIpService.GetAverageDistance();

            Assert.AreEqual("867.12", result);
        }

        [TestMethod]
        [ExpectedException(typeof(WithoutRequestException))]
        public void GGetAverageDistance_WithoutRequestException_Test()
        {
            var traceIpServiceMock = new Mock<ITraceIpService>();
            var apiCountryServiceMock = new Mock<IApiCountryService>();
            var currencyServiceMock = new Mock<ICurrencyService>();
            var redisServiceMock = new Mock<IRedisService>();

            redisServiceMock.Setup(p => p.GetRequestSummary()).Returns<RequestSummaryList?>(null);

            TraceIpService traceIpService = new TraceIpService(apiCountryServiceMock.Object, redisServiceMock.Object, currencyServiceMock.Object);

            var result = traceIpService.GetAverageDistance();
        }
    }
}
