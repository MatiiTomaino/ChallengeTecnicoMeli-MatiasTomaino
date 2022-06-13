﻿using TraceIp.Exceptions;
using TraceIp.Helpers;
using TraceIp.Model;
using TraceIp.Services.Interface;

namespace TraceIp.Services.Implementation
{
    public class TraceIpService : ITraceIpService
    {
        private readonly IApiCountryService _apiCountryService;
        private readonly ICurrencyService _currencyService;
        private readonly IRedisService _redis;

        public TraceIpService(IApiCountryService apiCountryService,
            IRedisService redis,
            ICurrencyService currencyService)
        {
            _apiCountryService = apiCountryService;
            _currencyService = currencyService;
            _redis = redis;
        }

        private void AddRequestCache(string ip, ApiRestCountryResponse apiRestCountryResponse)
        {
            List<ApiRestCountryResponse> cacheList = _redis.GetRequestCache()?.ToList() ?? new List<ApiRestCountryResponse>();

            ApiRestCountryResponse? register = cacheList?.FirstOrDefault(p => p.Ip == ip) ?? null;

            if (register == null)
            {
                _redis.SetRequestCache(apiRestCountryResponse!);
            }
        }

        private void AddRequestSummary(ApiRestCountryResponse apiResponse, double distance)
        {
            RequestSummaryList requestSummaryList = _redis.GetRequestSummary() ?? new RequestSummaryList()
            {
                Items = new List<RequestSummary>()
            };
            RequestSummary? requestSummary = requestSummaryList.Items?.FirstOrDefault(p => p.CountryCode!.ToLower() == apiResponse.CountryCode!.ToLower()) ?? null;

            if (requestSummary == null)
            {
                requestSummary = new RequestSummary()
                {
                    CountryCode = apiResponse.CountryCode,
                    CountryName = apiResponse.CountryName,
                    Distance = distance,
                    RequestCount = 1
                };

                requestSummaryList.Items!.Add(requestSummary);
            }
            else
            {
                requestSummary.RequestCount++;
            }

            _redis.SetRequestSummary(requestSummaryList!);
        }

        private ApiRestCountryResponse? GetCountryCache(string ip)
        {
            return _redis.GetRequestCache()?.FirstOrDefault(p => p?.Ip == ip) ?? null;
        }
         
        public TraceIpResponse TraceIp(string ip)
        {
            DateTime? dateTimeRequest = DateTime.Now;
            ApiRestCountryResponse restCountryResponse = GetCountryCache(ip) ?? _apiCountryService.GetCountryInfoByIp(ip).Result;

            if (restCountryResponse != null && restCountryResponse.CountryCode != null)
            {
                Currency currency = restCountryResponse.Currencies!.FirstOrDefault()!;
                string? currencyCode = currency.Code;
                string? quotationCurrency = Math.Round(_currencyService.ConvertCurrencyToUSD(currencyCode!).Result, 2).ToString();
                double distance = Math.Round(DistanceHelper.CalculatedDistanceToBuenosAires((double)restCountryResponse.Latitude!, (double)restCountryResponse.Longitude!), 2);

                TraceIpResponse result = new TraceIpResponse()
                {
                    Name = restCountryResponse.CountryName,
                    IsoCode = restCountryResponse.CountryCode,
                    Languages = restCountryResponse.Location!.Languages,
                    DateTimeRequest = dateTimeRequest,
                    ActualTime = DateTimeHelper.ConvertTimeZoneListToActualDateTimeList(restCountryResponse.Timezones!).Select(p => p.Value.ToShortTimeString() + $" ({p.Key})").ToList(),
                    Ip = ip,
                    Currency = currency,
                    CurrencyAndQuotation = currencyCode + $" (1 {currencyCode} = {quotationCurrency} USD)",
                    EstimatedDistance = distance
                };

                AddRequestCache(ip, restCountryResponse);
                AddRequestSummary(restCountryResponse, distance);

                return result;
            }

            throw new BadRequestException();
        }

        public string GetAverageDistance()
        {
            List<RequestSummary>? requestSummaryList = _redis.GetRequestSummary()?.Items ?? null;

            if (requestSummaryList?.Any() ?? false)
            {
                int? requestCount = requestSummaryList!.Sum(p => p.RequestCount);
                double? totalDistance = 0;
                requestSummaryList.ForEach(p => totalDistance += p.RequestCount * p.Distance);

                return (Math.Round((decimal)(Convert.ToDecimal(totalDistance)/requestCount), 2)).ToString();
            }
            else
            {
                throw new WithoutRequestException();
            }
        }

        public StatsResponse GetClosestDistance()
        {
            List<RequestSummary>? requestSummaryList = _redis.GetRequestSummary()?.Items ?? null;

            if (requestSummaryList?.Any() ?? false)
            {
                RequestSummary item = requestSummaryList!.OrderBy(p => p.Distance)!.FirstOrDefault()!;

                return new StatsResponse
                {
                    CountryName = item!.CountryName,
                    Value = item.Distance
                };
            }
            else
            {
                throw new WithoutRequestException();
            }
        }

        public StatsResponse GetFurthestDistance()
        {
            List<RequestSummary>? requestSummaryList = _redis.GetRequestSummary()?.Items ?? null;

            if (requestSummaryList?.Any() ?? false)
            {
                RequestSummary item = requestSummaryList!.OrderBy(p => p.Distance)!.LastOrDefault()!;

                return new StatsResponse
                {
                    CountryName = item!.CountryName,
                    Value = item.Distance
                };
            }
            else
            {
                throw new WithoutRequestException();
            }
        }
    }
}
