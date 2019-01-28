using System;
using System.Collections.Generic;
using System.Text;

namespace SunGoggles
{
    class TimeAndDateDataAccess
    {
        public string Country { get; }
        public string City { get; }
        public int Month { get; }
        public int Year { get; }

        public TimeAndDateDataAccess(string country, string city, int month, int year)
        {
            this.Country = country;
            this.City = city;
            this.Month = month;
            this.Year = year;
        }
    }
}
