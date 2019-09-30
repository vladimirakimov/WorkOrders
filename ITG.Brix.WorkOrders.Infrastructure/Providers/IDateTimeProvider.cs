using System;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers
{
    public interface IDateTimeProvider
    {
        DateTime ParseUtc(string utc);
        bool CanParseUtc(string utc);
        /// <summary>
        /// Parse string in format 2019-01-30T06:48:50Z into Utc DateTime
        /// </summary>
        /// <param name="utc">String representation of datetime in UTC ISO 8601</param>
        /// <returns></returns>
        DateTime? Parse(string utc);

        bool CheckFormat(string utc);
    }
}
