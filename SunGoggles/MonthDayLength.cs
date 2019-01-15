using System;
using System.Collections.Generic;
using System.Text;

namespace TimeAndDateScraper
{
    [Serializable]
    class MonthDayLength
    {
        public int Month { get; set; }
        public int Year { get; set;  }
        public string Location { get; set; }
        public Dictionary<int, string> DayOfMonthToDayLength { get; set; }
    }
}
