using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Gate : ValueObject
    {
        private readonly string _label;

        public Gate(Label label)
        {
            Guard.On(label, Error.GateLabelFieldShouldNotBeNull()).AgainstNull();

            _label = label;
        }

        public static implicit operator string(Gate gate)
        {
            string result = gate == null ? Label.UnsetValue : gate._label;
            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _label;
        }
    }
}
