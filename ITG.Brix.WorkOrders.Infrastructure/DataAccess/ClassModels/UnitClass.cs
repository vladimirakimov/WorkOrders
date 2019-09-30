using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels
{
    public class UnitClass
    {
        public Guid Id { get; set; }

        public string LocationWarehouse { get; set; }
        public string LocationGate { get; set; }
        public string LocationRow { get; set; }
        public string LocationPosition { get; set; }



        public string GroupKey { get; set; }
        public string GroupWeightNet { get; set; }
        public string GroupWeightGross { get; set; }


        public string MixedKey { get; set; }
        public string MixedPalletNumber { get; set; }

        public int Units { get; set; }
        public string UnitType { get; set; }
        public int Quantity { get; set; }

        public string WeightNet { get; set; }
        public string WeightGross { get; set; }


        public bool IsPartial { get; set; }
        public string PalletNumber { get; set; }
        public string SsccNumber { get; set; }

        public IEnumerable<ProductClass> Products { get; set; }
    }
}
