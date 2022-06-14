namespace TraceIp.Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Convert a string's list of timezones in a dictionary with the actual time in the timezone
        /// </summary>
        /// <param name="timezones">string's list of timezones</param>
        /// <param name="dateTimeRequest">request's datetime</param>
        /// <returns></returns>
        public static IDictionary<string, DateTime> ConvertTimeZoneListToActualDateTimeList(List<string> timezones, DateTime dateTimeRequest)
        {
            Console.WriteLine("Start convert timezones");
            Dictionary<string, DateTime> datetimeDictionary = new Dictionary<string, DateTime>();

            if (timezones?.Any() ?? false)
            {
                foreach (string timezone in timezones)
                {
                    if (timezone != null)
                    {
                        // In case you don't know what the key is:
                        List<TimeZoneInfo> allTimeZones = TimeZoneInfo.GetSystemTimeZones().ToList();

                        TimeZoneInfo? timeZoneInfo = allTimeZones.Where(p => p.DisplayName.Split("(")[1].Split(")")[0] == timezone).FirstOrDefault();

                        if (timeZoneInfo != null)
                        {
                            DateTime actualDatetime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimeRequest, timeZoneInfo!.Id);

                            datetimeDictionary.Add(timezone!, actualDatetime);
                        }
                    }
                };
            }

            Console.WriteLine("End convert timezones");
            return datetimeDictionary;
        }
    }
}
