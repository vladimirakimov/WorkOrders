using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Position : ValueObject
    {
        private readonly string _label;

        public Position(Label label)
        {
            Guard.On(label, Error.PositionLabelFieldShouldNotBeNull()).AgainstNull();

            _label = label;
        }

        public static implicit operator string(Position position)
        {
            string result = position == null ? Label.UnsetValue : position._label;
            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _label;
        }
    }
}
