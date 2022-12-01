using System;

namespace Helios.Util.Extensions
{
    public static class LongExtensions
    {
        /// <summary>
        /// Converts double to datetime class
        /// </summary>
        public static DateTime ToDateTime(this long value)
        {
            return DateTimeOffset.FromUnixTimeSeconds(value).UtcDateTime;
        }
    }
}
