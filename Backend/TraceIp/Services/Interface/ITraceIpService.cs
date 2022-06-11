using TraceIp.Model;

namespace TraceIp.Services.Interface
{
    public interface ITraceIpService
    {
        public ResponseTraceIpDto TraceIp(string ip);
    }
}
