using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TwitterApp.Extensions
{
    public static class StringExtension
    {
        public static string ToPossibleDateFormat(this string date)
        {
            var splittedDate = date.Split(' ');
            var day = splittedDate[2].ToDayWithSuffix();
            var month = splittedDate[1];
            var year = splittedDate[5];
            var time = splittedDate[3].Split(':');
            var hour = int.Parse(time[0]);
            var formattedTime = hour > 12 ? $"{hour}.{time[1]}pm" : $"{hour}.{time[1]}am";
            return $"{day} {month} {year} at {formattedTime}";
        }

        public static string WithProperFormatting(this string text) => Regex.Replace(
            Regex.Replace(text,
                @"(http|https|ftp|)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?([a-zA-Z0-9\-\?\,\'\/\+&%\$#_]+)",
                "<a href='$&'>$&</a>"),
                @"(^|[^@\w])@(\w{1,15})\b",
                " <a href='https://twitter.com/$2'>@$2</a>");
        
        private static string ToDayWithSuffix(this string num)
        {
            if (num.EndsWith("11")) return num + "th";
            if (num.EndsWith("12")) return num + "th";
            if (num.EndsWith("13")) return num + "th";
            if (num.EndsWith("1")) return num + "st";
            if (num.EndsWith("2")) return num + "nd";
            if (num.EndsWith("3")) return num + "rd";
            return num + "th";
        }
    }
}