using ITG.Brix.Diagnostics.Guards;
using System;
using System.Text.RegularExpressions;

namespace ITG.Brix.WorkOrders.Domain.Diagnostics
{
    internal static partial class GuardExtensions
    {
        #region T

        internal static Guard<T> AgainstNull<T>(this Guard<T> guard)
            where T : class
        {
            if (guard.Value == null)
            {
                throw guard.Exception;
            }

            return guard;
        }

        #endregion

        #region string

        internal static Guard<string> AgainstNullOrWhiteSpace(this Guard<string> guard)
        {
            if (string.IsNullOrWhiteSpace(guard.Value))
            {
                throw guard.Exception;
            }

            return guard;
        }

        internal static Guard<string> AgainstLowercasedWithUnderscoreAndDigitsAllowed(this Guard<string> guard)
        {
            if (!new Regex(@"^(#)+[0-9a-z_]+$").IsMatch(guard.Value))
            {
                throw guard.Exception;
            }

            return guard;
        }

        internal static Guard<string> AgainstLowercasedWithDashAndDigitsAllowed(this Guard<string> guard)
        {
            if (!new Regex(@"^[0-9a-z\-]+$").IsMatch(guard.Value))
            {
                throw guard.Exception;
            }

            return guard;
        }

        #endregion

        #region guid

        internal static Guard<Guid> AgainstEmptyGuid(this Guard<Guid> guard)
        {
            if (Guid.Empty == guard.Value)
            {
                throw guard.Exception;
            }

            return guard;
        }

        #endregion

        #region int

        internal static Guard<int> AgainstLessThanZero(this Guard<int> guard)
        {
            if (guard.Value < 0)
            {
                throw guard.Exception;
            }

            return guard;
        }

        #endregion

        #region float

        internal static Guard<float> AgainstLessThanZero(this Guard<float> guard)
        {
            if (guard.Value < 0)
            {
                throw guard.Exception;
            }

            return guard;
        }

        #endregion

        #region datetime

        internal static Guard<DateTime> AgainstValidUtc(this Guard<DateTime> guard)
        {
            if (guard.Value.Kind != DateTimeKind.Utc || guard.Value < new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            {
                throw guard.Exception;
            }

            return guard;
        }

        #endregion
    }
}
