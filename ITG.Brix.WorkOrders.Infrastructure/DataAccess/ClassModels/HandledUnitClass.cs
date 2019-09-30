using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels
{
    public class HandledUnitClass
    {
        public Guid Id { get; set; }

        public Guid SourceUnitId { get; set; }

        public Guid OperantId { get; set; }
        public string OperantLogin { get; set; }

        public long HandledOn { get; set; }

        public string LocationWarehouse { get; set; }
        public string LocationGate { get; set; }
        public string LocationRow { get; set; }
        public string LocationPosition { get; set; }


        public int Units { get; set; }
        public string UnitType { get; set; }
        public int Quantity { get; set; }

        public string WeightNet { get; set; }
        public string WeightGross { get; set; }


        public bool IsPartial { get; set; }
        public bool IsMixed { get; set; }
        public string PalletNumber { get; set; }
        public string SsccNumber { get; set; }

        public IEnumerable<GoodClass> Products { get; set; }
    }
}
