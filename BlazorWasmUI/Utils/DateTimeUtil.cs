using System;

namespace BlazorWasmUI.Utils
{
    public static class DateTimeUtil
    {
        public static string GetStringWithDateTimeOffsetFormatFromDate(DateTime? dateTime)
        {
            if (dateTime == null)
                return null;
            string day = dateTime.Value.Day.ToString().PadLeft(2, '0');
            string month = dateTime.Value.Month.ToString().PadLeft(2, '0');
            string year = dateTime.Value.Year.ToString();
            string hour = dateTime.Value.Hour.ToString().PadLeft(2, '0');
            string minute = dateTime.Value.Minute.ToString().PadLeft(2, '0');
            string second = dateTime.Value.Second.ToString().PadLeft(2, '0');
            return year + "-" + month + "-" + day + "T" + hour + ":" + minute + ":" + second + "Z";
        }
    }
}
