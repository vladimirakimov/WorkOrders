using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Configuration : ValueObject
    {
        public string Code { get; private set; }
        public string Description { get; private set; }
        public string Quantity { get; private set; }
        public string UnitType { get; private set; }
        public string NetPerUnit { get; private set; }
        public string NetPerUnitAlwaysDifferent { get; private set; }
        public string GrossPerUnit { get; private set; }

        public Configuration(string code, string description, string quantity, string unitType, string netPerUnit, string netPerUnitAlwaysDifferent, string grossPerUnit)
        {
            Code = code;
            Description = description;
            Quantity = quantity;
            UnitType = unitType;
            NetPerUnit = netPerUnit;
            NetPerUnitAlwaysDifferent = netPerUnitAlwaysDifferent;
            GrossPerUnit = grossPerUnit;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
            yield return Description;
            yield return Quantity;
            yield return UnitType;
            yield return NetPerUnit;
            yield return NetPerUnitAlwaysDifferent;
            yield return GrossPerUnit;
        }
    }
}
