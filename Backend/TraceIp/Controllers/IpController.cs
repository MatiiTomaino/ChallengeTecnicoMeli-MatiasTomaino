using Microsoft.AspNetCore.Mvc;

namespace TraceIp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpController : ControllerBase
    {
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
