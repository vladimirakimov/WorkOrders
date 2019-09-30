using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Location : ValueObject
    {
        public Warehouse Warehouse { get; private set; }
        public Gate Gate { get; private set; }
        public Row Row { get; private set; }
        public Position Position { get; private set; }

        public Location(Warehouse warehouse, Gate gate, Row row, Position position)
        {
            Guard.On(warehouse, Error.LocationWarehouseShouldNotBeNull()).AgainstNull();
            Guard.On(gate, Error.LocationGateShouldNotBeNull()).AgainstNull();
            Guard.On(row, Error.LocationRowShouldNotBeNull()).AgainstNull();
            Guard.On(position, Error.LocationPositionShouldNotBeNull()).AgainstNull();

            Warehouse = warehouse;
            Gate = gate;
            Row = row;
            Position = position;
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Warehouse;
            yield return Gate;
            yield return Row;
            yield return Position;
        }
    }
}
