namespace TraceIp.Exceptions
{
    public class WithoutRequestException : IOException
    {
        public WithoutRequestException()
            : base("no request has been made yet.")
        {
        }
    }
}
