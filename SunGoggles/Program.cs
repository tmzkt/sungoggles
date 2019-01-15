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
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.timeanddate.com/sun/usa/philadelphia?month=1&year=2019");

            var s = doc.DocumentNode.SelectNodes("//tr[@data-day]");

            MonthDayLength myobj = new MonthDayLength();
            myobj.Month = 1;
            myobj.Year = 2019;
            myobj.Location = "philadelphia";
            myobj.DayOfMonthToDayLength = new System.Collections.Generic.Dictionary<int, MonthDayLength.DayData>();
            foreach (HtmlNode node in s)
            {
                int dayOfMonth = Convert.ToInt32(node.Attributes["data-day"].Value);
                string dayLength = node.ChildNodes[3].InnerText;
                string difference = node.ChildNodes[4].InnerText;
                //Console.WriteLine($"Day {dayLength}, difference {difference}");
                myobj.DayOfMonthToDayLength.Add(dayOfMonth, new MonthDayLength.DayData(dayLength, difference));
            }

            MonthDayLength.DayData dayData = myobj.DayOfMonthToDayLength[DateTime.Now.Day];
            Console.WriteLine($"Day Length {dayData.DayLength}, difference {dayData.DayLengthDifference}");

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

            Console.ReadLine();
        }
    }
}
