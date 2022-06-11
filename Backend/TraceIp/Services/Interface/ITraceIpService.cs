using TraceIp.Model;

namespace TraceIp.Services.Interface
{
    public interface ITraceIpService
    {
        public TraceIpResponse TraceIp(string ip);
    }
}
