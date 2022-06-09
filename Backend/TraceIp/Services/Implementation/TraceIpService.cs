using TraceIp.Model;
using TraceIp.Services.Interface;

namespace TraceIp.Services.Implementation
{
    public class TraceIpService : ITraceIpService
    {
        public ResponseDto TraceIp(string ip)
        {
            return new ResponseDto()
            {

            };
        }
    }
}
