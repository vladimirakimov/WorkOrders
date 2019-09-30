using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Label : ValueObject
    {
        private readonly string _value;

        public Label(string value)
        {
            Guard.On(value, Error.LabelValueFieldShouldNotBeEmpty()).AgainstNullOrWhiteSpace();

            _value = value;
        }

        public static string UnsetValue { get { return "-"; } }

        public static explicit operator Label(string value)
        {
            return new Label(value);
        }

        public static implicit operator string(Label label)
        {
            string result = label == null ? UnsetValue : label._value;
            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }
    }
}
