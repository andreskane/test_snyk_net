using System;

namespace ABI.API.Structure.Application.Extensions
{
    public static class DatesExtensions
    {
        public static DateTimeOffset ToOffset(this DateTimeOffset input, int offset = -3)
        {
            var zone = TimeSpan.FromHours(offset);

            return input.ToOffset(zone);
        }

        public static DateTimeOffset ToDateOffset(this string input, int offset = -3, string format = "yyyy-MM-dd")
        {
            var zone = TimeSpan.FromHours(offset).ToString("hh\\:mm");
            var sign = offset > 0 ? "+" : "-";
            var date = $"{input} {sign}{zone}";
            var parse = $"{format} zzz";

            return DateTimeOffset.ParseExact(date, parse, null);
        }
        public static DateTimeOffset ToDateOffset(this DateTime input, int offset = -3)
        {
            return new DateTimeOffset(input, TimeSpan.FromHours(offset));
        }

        public static DateTimeOffset ToDateOffsetUtc(this string input, int offset = -3, string format = "yyyy-MM-dd")
        {
            var parsed = input.ToDateOffset(offset, format);

            return parsed.ToUniversalTime();
        }

        public static DateTimeOffset ToDateOffsetUtc(this DateTime input, int offset = -3)
        {
            var dateOffset = input.ToDateOffset(offset);

            return dateOffset.ToUniversalTime();
        }

        public static DateTimeOffset Today(this DateTimeOffset input, int offset = 0)
        {
            return DateTimeOffset.UtcNow.Date.ToDateOffset(offset);
        }
    }
}
