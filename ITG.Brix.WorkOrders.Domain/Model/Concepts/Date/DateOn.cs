using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class DateOn : ValueObject
    {
        private static readonly DateTime _sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private readonly DateTime _value;

        public DateOn(DateTime value)
        {
            Guard.On(value, Error.DateOnValueFieldShouldBeValid()).AgainstValidUtc();

            _value = value;
        }

        public DateOn(long time)
        {
            _value = _sTime.AddSeconds(time);
        }

        public static implicit operator string(DateOn dateOn)
        {
            return dateOn == null ? null : string.Concat(dateOn._value.ToString("s"), "Z");
        }

        public static implicit operator long?(DateOn dateOn)
        {
            long? result;
            if (dateOn == null)
            {
                result = (long?)null;
            }
            else
            {
                result = (long)(dateOn._value - _sTime).TotalSeconds;
            }
            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }
    }
}
