using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Row : ValueObject
    {
        private readonly string _label;

        public Row(Label label)
        {
            Guard.On(label, Error.RowLabelFieldShouldNotBeNull()).AgainstNull();

            _label = label;
        }

        public static implicit operator string(Row row)
        {
            string result = row == null ? Label.UnsetValue : row._label;
            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _label;
        }
    }
}
