using System;
using System.Collections.Generic;

namespace SunGoggles
{
    [Serializable]
    public class SunriseSunsetTimes
    {
        public string Country { get; set; }
        public string City { get; set; }
        public Dictionary<DateTime, SunriseSunsetTime> DateToSunriseSunsetTime { get; set; }

        [Serializable]
        public class SunriseSunsetTime
        {
            public string Sunrise { get; }
            public string Sunset { get; }
            public string DayLength { get; }
            public string DayLengthDifference { get; }
            public SunriseSunsetTime(string sunrise, string sunset, string dayLength, string dayLengthDifference)
            {
                Sunrise = sunrise;
                Sunset = sunset;
                DayLength = dayLength;
                DayLengthDifference = dayLengthDifference;
            }
        }
    }
}
