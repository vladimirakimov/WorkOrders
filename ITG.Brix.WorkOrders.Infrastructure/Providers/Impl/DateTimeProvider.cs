using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Infrastructure.Diagnostics;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using System;
using System.Globalization;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers.Impl
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime ParseUtc(string utc)
        {
            DateTime result;
            try
            {
                var formats = new[] { "yyyy-MM-ddTHH:mm:ssZ" };
                var parseResult = DateTime.TryParseExact(
                                                utc,
                                                formats,
                                                CultureInfo.InvariantCulture,
                                                DateTimeStyles.RoundtripKind,
                                                out DateTime value
                                            );

                Guard.On(parseResult, Error.UtcFormat(utc)).AgainstFalse();

                result = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            }
            catch
            {
                result = DateTime.UtcNow;
            }
            //var formats = new[] { "yyyy-MM-ddTHH:mm:ssZ" };
            //var parseResult = DateTime.TryParseExact(
            //                                utc,
            //                                formats,
            //                                CultureInfo.InvariantCulture,
            //                                DateTimeStyles.RoundtripKind,
            //                                out DateTime value
            //                            );

            //Guard.On(parseResult, Error.UtcFormat(utc)).AgainstFalse();

            //var result = DateTime.SpecifyKind(value, DateTimeKind.Utc);

            return result;
        }

        public bool CanParseUtc(string utc)
        {
            bool result;

            try
            {
                ParseUtc(utc);
                result = true;
            }
            catch (UtcFormatException)
            {
                result = false;
            }

            return result;
        }

        public DateTime? Parse(string utc)
        {
            if (string.IsNullOrWhiteSpace(utc))
            {
                return null;
            }
            try
            {
                var res = DateTime.Parse(utc, null, DateTimeStyles.RoundtripKind);
                var result = DateTime.SpecifyKind(res, DateTimeKind.Utc);

                return result;
            }
            catch
            {
                return null;
            }
        }


        public bool CheckFormat(string utc)
        {
            if (string.IsNullOrWhiteSpace(utc))
            {
                throw new Exception("Must not apply on empty value.");
            }


            var formats = new[] { "yyyy-MM-ddThh:mm:ssZ" };
            var result = DateTime.TryParseExact(
                                            utc,
                                            formats,
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.RoundtripKind,
                                            out DateTime value
                                        );

            return result;
        }
    }
}
