using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos
{
    public class HandledUnitDto
    {
        public string Id { get; set; }
        public string SourceUnitId { get; set; }
        public string OperantId { get; set; }
        public string OperantLogin { get; set; }
        public string HandledOn { get; set; }

        public string Warehouse { get; set; }
        public string Gate { get; set; }
        public string Row { get; set; }
        public string Position { get; set; }

        public string Units { get; set; }
        public string IsPartial { get; set; }
        public string IsMixed { get; set; }
        public string Quantity { get; set; }


        public string WeightNet { get; set; }
        public string WeightGross { get; set; }
        public string PalletNumber { get; set; }
        public string SsccNumber { get; set; }

        public IEnumerable<GoodDto> Products { get; set; }
    }
}
