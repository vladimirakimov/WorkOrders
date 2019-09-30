using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Warehouse : ValueObject
    {
        private readonly string _label;

        public Warehouse(Label label)
        {
            Guard.On(label, Error.WarehouseLabelFieldShouldNotBeNull()).AgainstNull();

            _label = label;
        }

        public static implicit operator string(Warehouse warehouse)
        {
            string result = warehouse == null ? Label.UnsetValue : warehouse._label;
            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _label;
        }
    }
}
