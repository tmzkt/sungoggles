using HtmlAgilityPack;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TimeAndDateScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument doc = new HtmlWeb().Load("https://www.timeanddate.com/sun/usa/philadelphia?month=1&year=2019");

            var s = doc.DocumentNode.SelectNodes("//tr[@data-day]");

            MonthDayLength myobj = new MonthDayLength();
            myobj.Month = 1;
            myobj.Year = 2019;
            myobj.Location = "philadelphia";
            myobj.DayOfMonthToDayLength = new System.Collections.Generic.Dictionary<int, MonthDayLength.DayData>();
            foreach (HtmlNode node in s)
            {
                int dayOfMonth = Convert.ToInt32(node.Attributes["data-day"].Value);
                string sunrise = node.ChildNodes[1].InnerText;
                string sunset = node.ChildNodes[2].InnerText;
                string dayLength = node.ChildNodes[3].InnerText;
                string difference = node.ChildNodes[4].InnerText;
                myobj.DayOfMonthToDayLength.Add(dayOfMonth, new MonthDayLength.DayData(sunrise, sunset, dayLength, difference));
            }

            MonthDayLength.DayData dayData = myobj.DayOfMonthToDayLength[DateTime.Now.Day];
            Console.WriteLine($"day length {dayData.DayLength}, difference {dayData.DayLengthDifference}, sunrise {dayData.Sunrise}, sunset {dayData.Sunset}");

            Stream stream = null;
            try
            {
                stream = new FileStream($"cache{myobj.Year}-{myobj.Month}", FileMode.Create);
                new BinaryFormatter().Serialize(stream, myobj);
            }
            finally
            {
                stream?.Close();
            }
        }
    }
}
