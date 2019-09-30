using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ITG.Brix.WorkOrders.Domain
{
    public sealed class HandledUnitCollection
    {
        private readonly IList<HandledUnit> _handledUnits;

        public HandledUnitCollection()
        {
            _handledUnits = new List<HandledUnit>();
        }

        public IReadOnlyCollection<HandledUnit> AsReadOnly()
        {
            IReadOnlyCollection<HandledUnit> result = new ReadOnlyCollection<HandledUnit>(_handledUnits);
            return result;
        }

        public void Add(HandledUnit handledUnit)
        {
            if (!_handledUnits.Contains(handledUnit))
            {
                _handledUnits.Add(handledUnit);
            }
        }

        public void Remove(HandledUnit handledUnit)
        {
            if (_handledUnits.Contains(handledUnit))
            {
                _handledUnits.Remove(handledUnit);
            }
        }

        public void RemoveLast()
        {
            if (_handledUnits.Any())
            {
                _handledUnits.RemoveAt(_handledUnits.Count - 1);
            }
        }

        public void Clear()
        {
            _handledUnits.Clear();
        }
    }
}
