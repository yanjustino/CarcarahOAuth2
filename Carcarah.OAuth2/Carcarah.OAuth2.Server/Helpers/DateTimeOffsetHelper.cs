using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.Helpers
{
    internal static class DateTimeOffsetHelper
    {
        internal static Func<DateTimeOffset> UtcNowFunc = () => DateTimeOffset.UtcNow;

        internal static DateTimeOffset UtcNow
        {
            get
            {
                return UtcNowFunc();
            }
        }

        internal static double NewExpirationDate(int seconds)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Math.Round((UtcNow.AddSeconds(seconds) - unixEpoch).TotalSeconds);
        }

    }
}
