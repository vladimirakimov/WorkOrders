using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Quantity : ValueObject
    {
        private readonly int _value;

        public Quantity(int value)
        {
            Guard.On(value, Error.QuantityValueFieldShouldBeGreaterOrEqualToZero()).AgainstLessThanZero();

            _value = value;
        }

        public static implicit operator int(Quantity quantity)
        {
            Guard.On(quantity, Error.QuantityShouldNotBeNull()).AgainstNull();

            var result = quantity._value;

            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }
    }
}
