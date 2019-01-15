using System;
using System.Collections.Generic;
using System.Text;

namespace TimeAndDateScraper
{
    [Serializable]
    public class MonthDayLength
    {
        public int Month { get; set; }
        public int Year { get; set;  }
        public string Location { get; set; }
        public Dictionary<int, DayData> DayOfMonthToDayLength { get; set; }

        [Serializable]
        public class DayData
        {
            public string DayLength { get; }
            public string DayLengthDifference { get; }
            public DayData(string dayLength, string dayLengthDifference)
            {
                this.DayLength = dayLength;
                this.DayLengthDifference = dayLengthDifference;
            }
        }
    }
}
