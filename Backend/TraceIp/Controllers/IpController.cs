using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using TraceIp.Exceptions;
using TraceIp.Services.Interface;

namespace TraceIp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly ITraceIpService _traceIpService;

        public IpController(IConnectionMultiplexer redis,
            ITraceIpService traceIpService)
        {
            _redis = redis;
            _traceIpService = traceIpService;
        }

        [HttpGet("testRedisRead")]
        public async Task<IActionResult> TestRedis(string key)
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(key);
            return Ok(value.ToString());
        }


        [HttpGet("testRedisWrite")]
        public  IActionResult TestRedis(string key, string value)
        {
            var db = _redis.GetDatabase();
            if (db.StringSet(key, value))
            {
                return StatusCode(StatusCodes.Status404NotFound, "Save ok.");
            }

            return StatusCode(StatusCodes.Status404NotFound, "The key already exist.");
        }

        [HttpGet("traceIp")]
        public IActionResult TraceIp(string ip)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _traceIpService.TraceIp(ip));
            }
            catch (BadRequestException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
