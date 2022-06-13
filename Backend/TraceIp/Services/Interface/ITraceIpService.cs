using TraceIp.Model;

namespace TraceIp.Services.Interface
{
    public interface ITraceIpService
    {
        /// <summary>
        /// Get country information from an ip
        /// </summary>
        /// <param name="ip">IP which information is to be searched</param>
        /// <returns></returns>
        public TraceIpResponse TraceIp(string ip);

        /// <summary>
        /// Get average distance from all requests.
        /// </summary>
        /// <returns></returns>
        public string GetAverageDistance();

        /// <summary>
        /// Get the closest distance from all request
        /// </summary>
        /// <returns></returns>
        public StatsResponse GetClosestDistance();

        /// <summary>
        /// Get the furthest distance from all request
        /// </summary>
        /// <returns></returns>
        public StatsResponse GetFurthestDistance();
    }
}
