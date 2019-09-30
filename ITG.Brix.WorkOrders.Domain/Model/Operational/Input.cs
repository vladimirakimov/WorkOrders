using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Input : ValueObject
    {
        public Input(Operant operant, CreatedOn createdOn, string value, string property)
        {
            Guard.On(operant, Error.InputOperantFieldShouldNotBeEmpty()).AgainstNull();
            Guard.On(createdOn, Error.InputCreatedOnFieldShouldNotBeEmpty()).AgainstNull();
            Guard.On(value, Error.InputValueFieldShouldNotBeEmpty()).AgainstNullOrWhiteSpace();
            Guard.On(property, Error.InputPropertyFieldShouldNotBeEmpty()).AgainstNullOrWhiteSpace();

            Operant = operant;
            CreatedOn = createdOn;
            Value = value;
            Property = property;
        }
        public Operant Operant { get; private set; }
        public CreatedOn CreatedOn { get; private set; }
        public string Value { get; private set; }
        public string Property { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Operant;
            yield return CreatedOn;
            yield return Value;
            yield return Property;
        }
    }
}
