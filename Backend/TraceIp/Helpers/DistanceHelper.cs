namespace TraceIp.Helpers
{
    public static class DistanceHelper
    {
        private static double latitudeBuenosAires = -34;
        private static double longitudeBuenosAires = -64;

        public static double CalculatedDistanceToBuenosAires(double latitudeCountry, double longitudeCountry)
        {
            Console.WriteLine("Start calculate distance to BsAs");
            double baseRad = Math.PI * latitudeBuenosAires / 180;
            double targetRad = Math.PI * latitudeCountry / 180;
            double theta = longitudeBuenosAires - longitudeCountry;
            double thetaRad = Math.PI * theta / 180;

            double? dist = Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) * Math.Cos(targetRad) * Math.Cos(thetaRad);

            if (dist != null)
            {
                dist = Math.Acos(dist.Value);

                if (!Double.IsNaN(dist.Value))
                {
                    dist = dist * 180 / Math.PI;
                    dist = dist * 60 * 1.1515;

                    Console.WriteLine("End calculate distance to BsAs");
                    return dist.Value * 1.609344; //To convert to KM
                }
            }

            Console.WriteLine("End calculate distance to BsAs");
            return 0;
        }
    }
}
