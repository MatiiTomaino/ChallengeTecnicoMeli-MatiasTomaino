using TraceIp.Helpers;

namespace TraceIpTest
{
    [TestClass]
    public class DateTimeHelperTest
    {
        //Test example from challenge
        [TestMethod]
        public void ConvertTimeZoneListToActualDateTimeList_Test()
        {
            DateTime datetime = DateTime.UtcNow;
            List<string> timezonesList = new List<string>()
            {
                "UTC",
                "UTC+01:00"
            };

            IDictionary<string, DateTime> result = DateTimeHelper.ConvertTimeZoneListToActualDateTimeList(timezonesList, datetime);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual("UTC", result.ElementAt(0).Key);
            Assert.AreEqual(datetime.Hour, result.ElementAt(0).Value.Hour);

            Assert.AreEqual("UTC+01:00", result.ElementAt(1).Key);
            Assert.AreEqual(datetime.AddHours(1).Hour, result.ElementAt(1).Value.Hour);
        }

        [TestMethod]
        public void ConvertTimeZoneListToActualDateTimeList_2_Test()
        {
            DateTime datetime = DateTime.UtcNow;
            List<string> timezonesList = new List<string>()
            {
                "UTC",
                "UTC+01:00",
                "UTC+03:00"
            };

            IDictionary<string, DateTime> result = DateTimeHelper.ConvertTimeZoneListToActualDateTimeList(timezonesList, datetime);

            Assert.AreEqual(3, result.Count);

            Assert.AreEqual("UTC", result.ElementAt(0).Key);
            Assert.AreEqual(datetime.Hour, result.ElementAt(0).Value.Hour);

            Assert.AreEqual("UTC+01:00", result.ElementAt(1).Key);
            Assert.AreEqual(datetime.AddHours(1).Hour, result.ElementAt(1).Value.Hour);

            Assert.AreEqual("UTC+03:00", result.ElementAt(2).Key);
            Assert.AreEqual(datetime.AddHours(3).Hour, result.ElementAt(2).Value.Hour);
        }


        [TestMethod]
        public void ConvertTimeZoneListToActualDateTimeList_Empty_Test()
        {
            DateTime datetime = DateTime.UtcNow;
            List<string> timezonesList = new List<string>();

            IDictionary<string, DateTime> result = DateTimeHelper.ConvertTimeZoneListToActualDateTimeList(timezonesList, datetime);

            Assert.AreEqual(0, result.Count);
        }
    }
}
