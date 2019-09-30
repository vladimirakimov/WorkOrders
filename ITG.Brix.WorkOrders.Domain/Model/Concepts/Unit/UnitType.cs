using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Domain
{
    public class UnitType
    {
        public static readonly UnitType Single = new UnitType(1, "Single");
        public static readonly UnitType Multiple = new UnitType(2, "Multiple");
        public static readonly UnitType Weight = new UnitType(3, "Weight");

        public string Name { get; private set; }

        public int Id { get; private set; }

        protected UnitType() { }

        public UnitType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<UnitType> List()
        {
            return new[] { Single, Multiple, Weight };
        }

        public static UnitType Parse(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for UnitType: {string.Join(", ", List().Select(s => s.Name))}");
            }

            return state;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as UnitType;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
