using System;

namespace SunGoggles
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            TimeAndDateDataAccess timeAndDateDataAccess = new TimeAndDateDataAccess("usa", "philadelphia", now.Month, now.Year);
            SunriseSunsetTimes sunriseSunsetTimes = timeAndDateDataAccess.GetSunriseSunsetTimesForMonth();
            SunriseSunsetTimes.SunriseSunsetTime dayData = sunriseSunsetTimes.DateToSunriseSunsetTime[now.Date];
            Console.WriteLine($"day length {dayData.DayLength}, difference {dayData.DayLengthDifference}, sunrise {dayData.Sunrise}, sunset {dayData.Sunset}");
        }
    }
}
