using System;

namespace SunGoggles
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            TimeAndDateDataAccess timeAndDateDataAccess = new TimeAndDateDataAccess("usa", "philadelphia", now.Month, now.Year);
            MonthDayLength monthDayLength = timeAndDateDataAccess.GetDayLengthForMonth();
            MonthDayLength.DayData dayData = monthDayLength.DayOfMonthToDayLength[now.Day];
            Console.WriteLine($"day length {dayData.DayLength}, difference {dayData.DayLengthDifference}, sunrise {dayData.Sunrise}, sunset {dayData.Sunset}");
        }
    }
}
