using Newtonsoft.Json;
using StackExchange.Redis;
using TraceIp.Helpers;
using TraceIp.Model;
using TraceIp.Services.Interface;

namespace TraceIp.Services.Implementation
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<string> GetValueFromKey(string key)
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(key);
            return value.ToString();
        }

        public bool Save(string key, string value)
        {
            var db = _redis.GetDatabase();
            return db.StringSet(key, value);
        }

        public bool SetRequestCache(ApiRestCountryResponse requestSummaryList)
        {
            var db = _redis.GetDatabase();
            return db.SetAdd(ConstStringHelper.RequestCache, JsonConvert.SerializeObject(requestSummaryList));
        }

        public IEnumerable<ApiRestCountryResponse>? GetRequestCache()
        {
            var db = _redis.GetDatabase();
            var value = db.SetMembers(ConstStringHelper.RequestCache);
            if (value?.Any() ?? false)
            {
                IEnumerable<ApiRestCountryResponse>? result = value!.Select(p => JsonConvert.DeserializeObject<ApiRestCountryResponse>(p!));
                return result;
            }

            return null;
        }

        public RequestSummaryList? GetRequestSummary()
        {
            var db = _redis.GetDatabase();
            var value = db.StringGet(ConstStringHelper.RequestSummary);

            if (value.HasValue) 
            {
                return JsonConvert.DeserializeObject<RequestSummaryList>(value!.ToString()!);
            }

            return null;
        }

        public bool SetRequestSummary(RequestSummaryList requestSummary)
        {
            var db = _redis.GetDatabase();
            return db.StringSet(ConstStringHelper.RequestSummary, JsonConvert.SerializeObject(requestSummary));
        }
    }
}
