using System;
using System.Collections.Generic;

namespace SunGoggles
{
    [Serializable]
    public class MonthDayLength
    {
        public int Month { get; set; }
        public int Year { get; set;  }
        public string Country { get; set; }
        public string City { get; set; }
        public Dictionary<int, DayData> DayOfMonthToDayLength { get; set; }

        [Serializable]
        public class DayData
        {
            public string Sunrise { get; }
            public string Sunset { get; }
            public string DayLength { get; }
            public string DayLengthDifference { get; }
            public DayData(string sunrise, string sunset, string dayLength, string dayLengthDifference)
            {
                Sunrise = sunrise;
                Sunset = sunset;
                DayLength = dayLength;
                DayLengthDifference = dayLengthDifference;
            }
        }
    }
}
