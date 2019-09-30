using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Domain
{
    public class OperationType : IComparable
    {
        public static readonly OperationType Inbound = new OperationType(2, "Inbound");
        public static readonly OperationType Outbound = new OperationType(3, "Outbound");
        public static readonly OperationType Manipulation = new OperationType(4, "Manipulation");


        public string Name { get; private set; }

        public int Id { get; private set; }

        protected OperationType() { }

        public OperationType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<OperationType> List()
        {
            return new[] { Inbound, Outbound, Manipulation };
        }

        public static OperationType Parse(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for OperationType: {string.Join(", ", List().Select(s => s.Name))}");
            }

            return state;
        }

        public int CompareTo(object obj) => Id.CompareTo(((OperationType)obj).Id);

        public override bool Equals(object obj)
        {
            var otherValue = obj as OperationType;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
