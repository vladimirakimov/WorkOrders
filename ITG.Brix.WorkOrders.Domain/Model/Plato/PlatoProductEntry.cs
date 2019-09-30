using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class PlatoProductEntry
    {
        public string EntryNo { get; set; }
        public string Customer { get; set; }
        public string Arrival { get; set; }
        public string Article { get; set; }
        public string ArticlePackagingCode { get; set; }
        public string ProductCode { get; set; }
        public PlatoProduct Product { get; set; }
        public PlatoConfiguration Configuration { get; set; }
        public string LotBatch { get; set; }
        public string LotBatch2 { get; set; }
        public string ClientReference { get; set; }
        public string BestBeforeDate { get; set; }
        public string DateFifo { get; set; }
        public string PalletNo { get; set; }
        public string SSCCNo { get; set; }
        public string CustomsDocument { get; set; }
        public string StorageStatus { get; set; }
        public string StackHeight { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string OriginalContainer { get; set; }
        public string IsPartial { get; set; }
        public string IsMixed { get; set; }
        public string MixedID { get; set; }
        public string MixedPalletNo { get; set; }
        public string Quantity { get; set; }
        public string QuantitySHU { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public PlatoProductLocation Location { get; set; }
        public List<string> ProductRemarks { get; set; }
        public List<string> SafetyRemarks { get; set; }
        public List<string> Notes { get; set; }
    }
}
