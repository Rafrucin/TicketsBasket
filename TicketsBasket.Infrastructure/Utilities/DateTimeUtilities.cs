using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsBasket.Infrastructure.Utilities
{
    public class DateTimeUtilities
    {
        public static string GetPassedTime(DateTime currentDate, DateTime originalDate)
        {
            var timespan = currentDate.Subtract(originalDate);
            int difference = Convert.ToInt32(timespan.TotalSeconds);

            int second = 1;
            int minute = 60 * second;
            int hour = 60 * minute;
            int day = 24 * hour;
            int month = 30 * day;
            int year = 12 * month;

            if (difference < minute)
                return difference == 1 ? "second ago" : $"{difference} seconds ago";            
            
            if (difference < hour)
                return timespan.Minutes == 1 ? "minute ago" : $"{timespan.Minutes} minutes ago";
           
            if (difference < day)
                return timespan.Hours == 1 ? "hour ago" : $"{timespan.Hours} hours ago";
            
            if (difference < month)
                return timespan.Days == 1 ? "day ago" : $"{timespan.Days} days ago";
            
            if (difference < year)
                return Convert.ToInt32(timespan.Days/30) == 1 ? "month ago" : $"{Convert.ToInt32(timespan.Days/30)} months ago";

            int years = Convert.ToInt32(timespan.TotalDays / 365);

            return years == 1 ? "year ago" : $"{years} years ago";



        }

    }
}
