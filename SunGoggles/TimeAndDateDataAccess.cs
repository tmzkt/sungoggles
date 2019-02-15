using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunGoggles
{
    class TimeAndDateDataAccess
    {
        private const string URL = "https://www.timeanddate.com/sun";
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

        public SunriseSunsetTimesForMonth GetSunriseSunsetTimesForMonth()
        {
            FileSerializer fileSerializer = new FileSerializer();

            SunriseSunsetTimesForMonth cachedData = (SunriseSunsetTimesForMonth)fileSerializer.Load(AppDomain.CurrentDomain.BaseDirectory + "sungoggles.cache");
            if (cachedData == null || !(cachedData.Month == Month && cachedData.Year == Year))
            {
                cachedData = GetSunriseSunsetTimesForMonthWithNoCache();
                fileSerializer.Save(AppDomain.CurrentDomain.BaseDirectory + "sungoggles.cache", cachedData);
            }

            return cachedData;
        }

        public SunriseSunsetTimesForMonth GetSunriseSunsetTimesForMonthWithNoCache()
        {
            HtmlDocument document = new HtmlWeb().Load($"{URL}/{Country}/{City}?month={Month}&year={Year}");
            var dayNodes = document.DocumentNode.SelectNodes("//tr[@data-day]");

            SunriseSunsetTimesForMonth sunriseSunsetTimes = new SunriseSunsetTimesForMonth();
            sunriseSunsetTimes.Month = Month;
            sunriseSunsetTimes.Year = Year;
            sunriseSunsetTimes.Country = Country;
            sunriseSunsetTimes.City = City;
            sunriseSunsetTimes.DayOfMonthToSunriseSunsetTime = new Dictionary<int, SunriseSunsetTimesForMonth.SunriseSunsetTime>();
            foreach (HtmlNode node in dayNodes)
            {
                int dayOfMonth = Convert.ToInt32(node.Attributes["data-day"].Value);
                string sunrise = node.ChildNodes[1].InnerText;
                string sunset = node.ChildNodes[2].InnerText;
                string dayLength = node.ChildNodes[3].InnerText;
                string difference = node.ChildNodes[4].InnerText;
                sunriseSunsetTimes.DayOfMonthToSunriseSunsetTime.Add(dayOfMonth, new SunriseSunsetTimesForMonth.SunriseSunsetTime(sunrise, sunset, dayLength, difference));
            }

            return sunriseSunsetTimes;
        }
    }
}
