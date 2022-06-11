using TraceIp.Helpers;

namespace TraceIpTest
{
    [TestClass]
    public class DistanceHelperTest
    {
        //Test example from challenge
        [TestMethod]
        public void MeasureDistanceExampleToBuenosAiresInKm_Test()
        {
            Assert.AreEqual(10274, Math.Round(DistanceHelper.CalculatedDistanceToBuenosAires(40, -4), 0));
        }

        //Test if latitude and longitude from response is Buenos aires. The distance should be Zero.
        [TestMethod]
        public void MeasureDistanceBuenosAiresToBuenosAiresInKm_Test()
        {
            Assert.AreEqual(0, Math.Round(DistanceHelper.CalculatedDistanceToBuenosAires(-34, -64), 0));
        }
    }
}