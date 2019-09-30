using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Group : ValueObject
    {
        public Group(string key, Weight weightNet, Weight weightGross)
        {
            Key = key;
            WeightNet = weightNet;
            WeightGross = weightGross;
        }

        public string Key { get; private set; }
        public Weight WeightNet { get; private set; }
        public Weight WeightGross { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Key;
            yield return WeightNet;
            yield return WeightGross;
        }
    }
}
