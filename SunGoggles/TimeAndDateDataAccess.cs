using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunGoggles
{
    class TimeAndDateDataAccess
    {
        private const string URL = "https://www.timeanddate.com/sun";
        private static readonly string CacheFilePath = AppDomain.CurrentDomain.BaseDirectory + "sungoggles.cache";
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

        public SunriseSunsetTimes GetSunriseSunsetTimesForMonth()
        {
            FileSerializer fileSerializer = new FileSerializer();

            SunriseSunsetTimes cachedData = (SunriseSunsetTimes)fileSerializer.Load(CacheFilePath);
            if (cachedData == null || !(cachedData.Country == Country && cachedData.City == City 
                && cachedData.DateToSunriseSunsetTime.Keys.Count > 0 && cachedData.DateToSunriseSunsetTime.ContainsKey(new DateTime(Year, Month, 1)))) // TODO redesign
            {
                cachedData = GetSunriseSunsetTimesForMonthWithNoCache();
                fileSerializer.Save(CacheFilePath, cachedData);
            }

            return cachedData;
        }

        public SunriseSunsetTimes GetSunriseSunsetTimesForMonthWithNoCache()
        {
            HtmlDocument document = new HtmlWeb().Load($"{URL}/{Country}/{City}?month={Month}&year={Year}");
            var dayNodes = document.DocumentNode.SelectNodes("//tr[@data-day]");

            SunriseSunsetTimes sunriseSunsetTimes = new SunriseSunsetTimes();
            sunriseSunsetTimes.Country = Country;
            sunriseSunsetTimes.City = City;
            sunriseSunsetTimes.DateToSunriseSunsetTime = new Dictionary<DateTime, SunriseSunsetTimes.SunriseSunsetTime>();
            foreach (HtmlNode node in dayNodes)
            {
                int dayOfMonth = Convert.ToInt32(node.Attributes["data-day"].Value);
                DateTime date = new DateTime(Year, Month, dayOfMonth);
                string sunrise = node.ChildNodes[1].InnerText;
                string sunset = node.ChildNodes[2].InnerText;
                string dayLength = node.ChildNodes[3].InnerText;
                string difference = node.ChildNodes[4].InnerText;
                sunriseSunsetTimes.DateToSunriseSunsetTime.Add(date, new SunriseSunsetTimes.SunriseSunsetTime(sunrise, sunset, dayLength, difference));
            }

            return sunriseSunsetTimes;
        }
    }
}
