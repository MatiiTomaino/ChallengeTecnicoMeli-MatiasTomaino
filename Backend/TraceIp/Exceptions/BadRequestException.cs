namespace TraceIp.Exceptions
{
    public class BadRequestException : IOException
    {
        public BadRequestException()
            : base("Ip value not exists.")
        {
        }
    }
}
