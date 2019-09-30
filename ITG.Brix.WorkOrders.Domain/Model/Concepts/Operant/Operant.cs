using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Operant : ValueObject
    {
        public Guid Id { get; private set; }
        public Login Login { get; private set; }

        public Operant(Guid id, Login login)
        {
            Guard.On(id, Error.OperantIdFieldShouldNotBeDefaultGuid()).AgainstEmptyGuid();
            Guard.On(login, Error.OperantLoginFieldShouldNotBeNull()).AgainstNull();

            Id = id;
            Login = login;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Login;
        }
    }
}
