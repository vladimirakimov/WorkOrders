using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ITG.Brix.WorkOrders.Domain
{
    public sealed class GoodCollection
    {
        private readonly IList<Good> _goods;

        public GoodCollection()
        {
            _goods = new List<Good>();
        }

        public IReadOnlyCollection<Good> AsReadOnly()
        {
            IReadOnlyCollection<Good> result = new ReadOnlyCollection<Good>(_goods);
            return result;
        }

        public void Add(Good good)
        {
            if (!_goods.Contains(good))
            {
                _goods.Add(good);
            }
        }

        public void Remove(Good good)
        {
            if (_goods.Contains(good))
            {
                _goods.Remove(good);
            }
        }


        public void Clear()
        {
            _goods.Clear();
        }
    }
}
