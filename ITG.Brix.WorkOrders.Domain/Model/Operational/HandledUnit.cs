using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class HandledUnit : Entity
    {
        private readonly GoodCollection _goods;

        public Operant Operant { get; private set; }
        public HandledOn HandledOn { get; private set; }
        public Unit SourceUnit { get; private set; }
        public Location Location { get; private set; }
        public Units Units { get; private set; }
        public bool IsPartial { get; private set; }
        public bool IsMixed { get; private set; }
        public UnitType Type { get; private set; }
        public Quantity Quantity { get; private set; }
        public Weight WeightNet { get; private set; }
        public Weight WeightGross { get; private set; }
        public string PalletNumber { get; private set; }
        public string SsccNumber { get; private set; }

        public IReadOnlyCollection<Good> Goods => _goods.AsReadOnly();

        public HandledUnit(Guid id, Unit sourceUnit) : base(id)
        {
            SourceUnit = sourceUnit;
            _goods = new GoodCollection();
        }
        public void SetOperant(Operant operant)
        {
            Guard.On(operant, Error.HandledUnitOperantShouldNotBeNull()).AgainstNull();

            Operant = operant;
        }

        public void SetHandledOn(HandledOn handledOn)
        {
            Guard.On(handledOn, Error.HandledUnitHandledOnShouldNotBeNull()).AgainstNull();

            HandledOn = handledOn;
        }

        public void SetLocation(Location location)
        {
            Guard.On(location, Error.HandledUnitLocationShouldNotBeNull()).AgainstNull();

            Location = location;
        }

        public void SetUnits(Units units)
        {
            Units = units;
        }

        public void SetIsPartial(bool isPartial)
        {
            IsPartial = isPartial;
        }

        public void SetIsMixed(bool isMixed)
        {
            IsMixed = isMixed;
        }

        public void SetType(UnitType type)
        {
            Type = type;
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

        public void AddGood(Good good)
        {
            Guard.On(good, Error.HandledUnitGoodShouldNotBeNull()).AgainstNull();

            _goods.Add(good);
        }
    }
}
