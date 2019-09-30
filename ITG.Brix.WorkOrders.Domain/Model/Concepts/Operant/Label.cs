using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Login : ValueObject
    {
        private readonly string _value;

        public Login(string value)
        {
            Guard.On(value, Error.LoginValueFieldShouldNotBeEmpty()).AgainstNullOrWhiteSpace();

            _value = value;
        }

        public static string UnsetLogin { get { return "unset"; } }

        public static explicit operator Login(string value)
        {
            return new Login(value);
        }

        public static implicit operator string(Login login)
        {
            string result = login == null ? UnsetLogin : login._value;
            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }
    }
}
