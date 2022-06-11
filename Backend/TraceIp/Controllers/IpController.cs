using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace TraceIp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;

        public IpController(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        [HttpGet("testRedisRead")]
        public async Task<IActionResult> TestRedis()
        {
            var db = _redis.GetDatabase();
            var foo = await db.StringGetAsync("foo");
            return Ok(foo.ToString());
        }


        [HttpGet("testRedisWrite")]
        public async Task<IActionResult> TestRedis(string key, string value)
        {
            var db = _redis.GetDatabase();
            if (await db.SetAddAsync(key, value))
            {
                return StatusCode(StatusCodes.Status404NotFound, "Save ok.");
            }

            return StatusCode(StatusCodes.Status404NotFound, "The key already exist.");
        }

        [HttpGet("helloworld")]
        public IActionResult TraceIp()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, "Hello world with docker and redis!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        [HttpGet("traceIp")]
        public IActionResult TraceIp(string ip)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, "Hello world! " + ip);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }
    }
}
