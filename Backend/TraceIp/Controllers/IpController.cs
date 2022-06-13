using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraceIp.Exceptions;
using TraceIp.Services.Interface;

namespace TraceIp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpController : ControllerBase
    {
        private readonly IRedisService _redis;
        private readonly ITraceIpService _traceIpService;

        public IpController(IRedisService redis,
            ITraceIpService traceIpService)
        {
            _redis = redis;
            _traceIpService = traceIpService;
        }

        [HttpGet("testRedisRead")]
        public async Task<IActionResult> TestRedis(string key)
        {
            return Ok(await _redis.GetValueFromKey(key));
        }

        [HttpGet("testRedisSummaryRead")]
        public IActionResult TestRedisSummary()
        {
            return StatusCode(StatusCodes.Status202Accepted, JsonConvert.SerializeObject(_redis.GetRequestSummary()) ?? "Empty request.");
        }

        [HttpGet("testRedisCacheRead")]
        public IActionResult TestRedisCache()
        {
            return StatusCode(StatusCodes.Status202Accepted, JsonConvert.SerializeObject(_redis.GetRequestCache()) ?? "Empty request.");
        }

        [HttpGet("testRedisWrite")]
        public  IActionResult TestRedis(string key, string value)
        {
            if (_redis.Save(key, value))
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

        [HttpGet("GetFurthestDistance")]
        public IActionResult GetFurthestDistance()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _traceIpService.GetFurthestDistance());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetClosestDistance")]
        public IActionResult GetClosestDistance()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _traceIpService.GetClosestDistance());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAverageDistance")]
        public IActionResult GetAverageDistance()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _traceIpService.GetAverageDistance());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
