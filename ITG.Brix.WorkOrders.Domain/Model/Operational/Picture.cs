using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Picture : ValueObject
    {
        public Operant Operant { get; private set; }
        public CreatedOn CreatedOn { get; private set; }
        public string Name { get; private set; }


        public Picture(Operant operant, CreatedOn createdOn, string name)
        {
            Guard.On(operant, Error.PictureOperantFieldShouldNotBeNull()).AgainstNull();
            Guard.On(createdOn, Error.PictureCreatedOnFieldShouldNotBeNull()).AgainstNull();
            Guard.On(name, Error.PictureNameFieldShouldNotBeEmpty()).AgainstNullOrWhiteSpace();

            Operant = operant;
            CreatedOn = createdOn;
            Name = name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Operant;
            yield return CreatedOn;
            yield return Name;
        }
    }
}
