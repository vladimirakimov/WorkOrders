using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Mixed : ValueObject
    {
        public Mixed(string key, string palletNumber)
        {
            Key = key;
            PalletNumber = palletNumber;
        }

        public string Key { get; set; }
        public string PalletNumber { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Key;
            yield return PalletNumber;
        }
    }
}
