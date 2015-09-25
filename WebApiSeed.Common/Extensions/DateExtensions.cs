namespace WebApiSeed.Common.Extensions
{
    using System;

    public static class DateExtensions
    {
        private static DateTime _baseDate = new DateTime(1970, 1, 1, 0, 0, 0);

        public static double ToEpoch(this DateTime date)
        {
            return Math.Truncate((date.Subtract(_baseDate)).TotalSeconds);
        }

        public static DateTime FromEpoch(this double seconds)
        {
            return _baseDate.AddSeconds(seconds);
        }
    }
}