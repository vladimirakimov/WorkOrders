using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class CreatedOn : ValueObject
    {
        private static readonly DateTime _sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public readonly DateTime _value;

        public CreatedOn(DateTime value)
        {
            Guard.On(value, Error.CreatedOnValueFieldShouldBeValid()).AgainstValidUtc();

            _value = value;
        }

        public CreatedOn(long time)
        {
            _value = _sTime.AddSeconds(time);
        }

        public static implicit operator DateTime(CreatedOn createdOn)
        {
            Guard.On(createdOn, Error.CreatedOnShouldNotBeNull()).AgainstNull();

            var result = createdOn._value;

            return result;
        }

        public static implicit operator string(CreatedOn createdOn)
        {
            Guard.On(createdOn, Error.CreatedOnShouldNotBeNull()).AgainstNull();

            return string.Concat(createdOn._value.ToString("s"), "Z");
        }

        public static implicit operator long(CreatedOn createdOn)
        {
            var result = (long)(createdOn._value - _sTime).TotalSeconds;

            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }
    }
}
