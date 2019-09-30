using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class HandledOn : ValueObject
    {
        private static readonly DateTime _sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public readonly DateTime _value;

        public HandledOn(DateTime value)
        {
            Guard.On(value, Error.HandledOnValueFieldShouldBeValid()).AgainstValidUtc();

            _value = value;
        }

        public HandledOn(long time)
        {
            _value = _sTime.AddSeconds(time);
        }

        public static implicit operator DateTime(HandledOn handledOn)
        {
            Guard.On(handledOn, Error.HandledOnShouldNotBeNull()).AgainstNull();

            var result = handledOn._value;

            return result;
        }

        public static implicit operator string(HandledOn handledOn)
        {
            Guard.On(handledOn, Error.HandledOnShouldNotBeNull()).AgainstNull();

            return string.Concat(handledOn._value.ToString("s"), "Z");
        }

        public static implicit operator long(HandledOn handledOn)
        {
            var result = (long)(handledOn._value - _sTime).TotalSeconds;

            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }
    }
}
