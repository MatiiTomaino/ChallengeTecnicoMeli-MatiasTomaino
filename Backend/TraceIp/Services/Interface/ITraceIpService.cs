using TraceIp.Model;

namespace TraceIp.Services.Interface
{
    public interface ITraceIpService
    {
        public ResponseDto TraceIp(string ip);
    }
}
