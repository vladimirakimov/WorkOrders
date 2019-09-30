using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Remark : ValueObject
    {
        public Operant Operant { get; private set; }
        public CreatedOn CreatedOn { get; private set; }
        public string Text { get; private set; }

        public Remark(Operant operant, CreatedOn createdOn, string text)
        {
            Guard.On(operant, Error.RemarkOperantFieldShouldNotBeNull()).AgainstNull();
            Guard.On(createdOn, Error.RemarkCreatedOnFieldShouldNotBeNull()).AgainstNull();
            Guard.On(text, Error.RemarkTextFieldShouldNotBeEmpty()).AgainstNullOrWhiteSpace();

            Operant = operant;
            CreatedOn = createdOn;
            Text = text;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Operant;
            yield return CreatedOn;
            yield return Text;
        }

    }
}
