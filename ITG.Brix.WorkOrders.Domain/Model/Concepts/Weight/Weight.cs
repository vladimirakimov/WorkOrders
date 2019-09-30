using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;
using System.Globalization;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Weight : ValueObject
    {
        private readonly float _value;

        public Weight(float value)
        {
            Guard.On(value, Error.WeightValueFieldShouldBeGreaterOrEqualToZero()).AgainstLessThanZero();

            _value = value;
        }

        public static implicit operator string(Weight weight)
        {
            return weight?._value.ToString("F2", CultureInfo.InvariantCulture);
        }

        public static Weight operator /(Weight lhs, float rhs)
        {
            return new Weight(lhs._value / rhs);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }
    }
}
