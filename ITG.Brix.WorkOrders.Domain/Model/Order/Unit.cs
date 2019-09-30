using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Unit : Entity
    {
        public Unit(Guid id, UnitType type) : base(id)
        {
            Type = type;
            Products = new List<Product>();
        }

        public UnitType Type { get; private set; }
        public Location Location { get; private set; }
        public Group Group { get; private set; }
        public Mixed Mixed { get; private set; }
        public Units Units { get; private set; }
        public bool IsMixed
        {
            get
            {
                return Mixed.Key != null;
            }
        }
        public bool IsPartial { get; private set; }
        public Quantity Quantity { get; private set; }
        public Weight WeightNet { get; private set; }
        public Weight WeightGross { get; private set; }
        public string PalletNumber { get; private set; }
        public string SsccNumber { get; private set; }
        public IEnumerable<Product> Products { get; private set; }

        public void SetLocation(Location location)
        {
            Guard.On(location, Error.UnitLocationShouldNotBeNull()).AgainstNull();

            Location = location;
        }
        public void SetGroup(Group group)
        {
            Guard.On(group, Error.UnitGroupShouldNotBeNull()).AgainstNull();

            Group = group;
        }

        public void SetMixed(Mixed mixed)
        {
            Guard.On(mixed, Error.UnitMixedShouldNotBeNull()).AgainstNull();

            Mixed = mixed;
        }

        public void SetUnits(Units units)
        {
            Guard.On(units, Error.UnitsShouldNotBeNull()).AgainstNull();

            Units = units;
        }

        public void SetIsPartial(bool isPartial)
        {
            IsPartial = isPartial;
        }

        public void SetQuantity(Quantity quantity)
        {
            Quantity = quantity;
        }

        public void SetWeightNet(Weight weightNet)
        {
            WeightNet = weightNet;
        }

        public void SetWeightGross(Weight weightGross)
        {
            WeightGross = weightGross;
        }

        public void SetPalletNumber(string palletNumber)
        {
            PalletNumber = palletNumber;
        }

        public void SetSsccNumber(string ssccNumber)
        {
            SsccNumber = ssccNumber;
        }

        public void SetProducts(IEnumerable<Product> products)
        {
            Products = products;
        }
    }
}
