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

        public MonthDayLength GetDayLengthForMonth()
        {
            throw new NotImplementedException();
        }

        public MonthDayLength GetDayLengthForMonthWithNoCache()
        {
            HtmlDocument document = new HtmlWeb().Load($"${URL}/${Country}/${City}?month=${Month}&year=${Year}");
            var dayNodes = document.DocumentNode.SelectNodes("//tr[@data-day]");

            MonthDayLength monthDayLength = new MonthDayLength();
            monthDayLength.Month = Month;
            monthDayLength.Year = Year;
            monthDayLength.Location = City; // TODO split location into country and city
            monthDayLength.DayOfMonthToDayLength = new Dictionary<int, MonthDayLength.DayData>();
            foreach (HtmlNode node in dayNodes)
            {
                int dayOfMonth = Convert.ToInt32(node.Attributes["data-day"].Value);
                string sunrise = node.ChildNodes[1].InnerText;
                string sunset = node.ChildNodes[2].InnerText;
                string dayLength = node.ChildNodes[3].InnerText;
                string difference = node.ChildNodes[4].InnerText;
                monthDayLength.DayOfMonthToDayLength.Add(dayOfMonth, new MonthDayLength.DayData(sunrise, sunset, dayLength, difference));
            }

            return monthDayLength;
        }
    }
}
