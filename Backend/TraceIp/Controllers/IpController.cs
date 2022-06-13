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

        /// <summary>
        /// Get country information from an ip
        /// </summary>
        /// <param name="ip">IP which information is to be searched</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the furthest distance from all request
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFurthestDistance")]
        public IActionResult GetFurthestDistance()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _traceIpService.GetFurthestDistance());
            }
            catch (WithoutRequestException ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get the closest distance from all request
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetClosestDistance")]
        public IActionResult GetClosestDistance()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _traceIpService.GetClosestDistance());
            }
            catch (WithoutRequestException ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get average distance from all requests.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAverageDistance")]
        public IActionResult GetAverageDistance()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _traceIpService.GetAverageDistance());
            }
            catch (WithoutRequestException ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
