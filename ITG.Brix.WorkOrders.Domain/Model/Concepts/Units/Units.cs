using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Units : ValueObject
    {
        private readonly int _value;

        public Units(int value)
        {
            Guard.On(value, Error.UnitsValueFieldShouldBeGreaterOrEqualToZero()).AgainstLessThanZero();

            _value = value;
        }

        public static implicit operator int(Units units)
        {
            Guard.On(units, Error.UnitsShouldNotBeNull()).AgainstNull();

            var result = units._value;

            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }
    }
}
