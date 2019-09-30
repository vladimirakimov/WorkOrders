using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Models
{
    public class HandledUnitModel
    {
        public Guid Id { get; set; }
        public Guid SourceUnitId { get; set; }
        public OperantModel Operant { get; set; }
        public string HandledOn { get; set; }
        public LocationModel Location { get; set; }

        public int Units { get; set; }
        public bool IsPartial { get; set; }
        public bool IsMixed { get; set; }
        //public string Type { get; set; }

        public string WeightNet { get; set; }
        public string WeightGross { get; set; }

        public string PalletNumber { get; set; }
        public string SsccNumber { get; set; }

        public IEnumerable<GoodModel> Products { get; set; }
    }
}
