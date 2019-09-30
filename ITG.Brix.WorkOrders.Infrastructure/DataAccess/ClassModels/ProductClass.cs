using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels
{
    public class ProductClass
    {
        public Guid Id { get; set; }

        public string ConfigurationCode { get; set; }
        public string ConfigurationDescription { get; set; }
        public string ConfigurationQuantity { get; set; }
        public string ConfigurationUnitType { get; set; }
        public string ConfigurationNetPerUnit { get; set; }
        public string ConfigurationNetPerUnitAlwaysDifferent { get; set; }
        public string ConfigurationGrossPerUnit { get; set; }


        public IEnumerable<string> Remarks { get; set; }
        public IEnumerable<string> SafetyRemarks { get; set; }
        public IEnumerable<string> Notes { get; set; }

        public string Code { get; set; }
        public string Customer { get; set; }
        public string Arrival { get; set; }
        public string Article { get; set; }
        public string ArticlePackagingCode { get; set; }
        public string Name { get; set; }
        public string Gtin { get; set; }
        public string ProductType { get; set; }
        public string MaterialType { get; set; }
        public string Color { get; set; }
        public string Shape { get; set; }
        public string Lotbatch { get; set; }
        public string Lotbatch2 { get; set; }
        public string ClientReference { get; set; }
        public string ClientReference2 { get; set; }
        public long? BestBeforeDate { get; set; }
        public long? DateFifo { get; set; }
        public string PalletNumber { get; set; }
        public string SsccNumber { get; set; }
        public string CustomsDocument { get; set; }
        public string StorageStatus { get; set; }
        public string Stackheight { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string OriginalContainer { get; set; }
        public int Quantity { get; set; }
        public string WeightNet { get; set; }
        public string WeightGross { get; set; }
    }
}
