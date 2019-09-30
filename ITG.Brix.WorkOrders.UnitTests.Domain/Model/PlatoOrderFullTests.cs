using ITG.Brix.WorkOrders.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model
{
    [TestClass]
    public class PlatoOrderFullTests
    {
        [TestMethod]
        public void CreateWorkOrderShouldSucceed()
        {
            // Arrange
            var order = new PlatoOrderFull()
            {
                Transport = new PlatoTransport()
                {
                    Source = "BKAL33+KBT T",
                    ID = "783584",
                    ExtraActivities = new List<PlatoExtraActivity>()
                {
                   new PlatoExtraActivity()
                   {
                       ID = "1",
                       Activity = "Activity 1",
                       Description = "Description 1",
                       IsExecuted = "true",
                       Quantity ="1 kg",
                       Remarks = "Remark 1"

                    },
                   new PlatoExtraActivity()
                   {
                       ID = "2",
                       Activity = "Activity 2",
                       Description = "Description 2",
                       IsExecuted = "true",
                       Quantity ="2 kg",
                       Remarks = "Remark 2"

                    },
                },
                    ProductEntries = new List<PlatoProductEntry>()
                {
                    new PlatoProductEntry()
                    {
                        EntryNo = "13712191",
                        Customer = "DBPLASTICS",
                        Arrival = "AEN000814",
                        Article = "418595",
                        ArticlePackagingCode = "55",
                        ProductCode = "3802BAK21",
                        Product = new PlatoProduct()
                        {
                            GTIN = null,
                            ProductType = "PE-HD",
                            MaterialType = "0",
                            Color = "BLACK",
                            Shape ="GRANULS"
                        },
                        Configuration = new PlatoConfiguration()
                        {
                            Code = "55BGEPE25KG-PRS4LS",
                            Description = "55 LOGO PE BAG(S) 25 KG ON PRS4 PALLET+SHRUNK",
                            Quantity = "55",
                            ConfigurationUnit = new PlatoConfigurationUnit()
                            {
                                UnitType = "BGEPE",
                                NetPerUnit = "25",
                                NetPerUnitAlwaysDifferent = "0"
                            }
                        },
                        LotBatch = "3101191",
                        LotBatch2 = null,
                        ClientReference = null,
                        BestBeforeDate=null,
                        DateFifo=null,
                        PalletNo=null,
                        SSCCNo=null,
                        CustomsDocument="COMMUNITY",
                        StorageStatus="STORAGE",
                        StackHeight="2",
                        Length="110",
                        Width="130",
                        Height="0",
                        OriginalContainer=null,
                        IsPartial="0",
                        IsMixed="0",
                        MixedID="0",
                        MixedPalletNo=null,
                        Quantity="550",
                        QuantitySHU="10",
                        NetWeight="13750",
                        GrossWeight="14140.5",
                        Location = new PlatoProductLocation()
                        {
                           Warehouse = "MAGAZIJN 1",
                           Gate = "K0L",
                           Row = "-",
                           Position = "-"
                        },
                        ProductRemarks = new List<string>(){"product remark1", "product remark 2"},
                        SafetyRemarks = new List<string>(){"safety remark1", "safety remark 2"},
                        Notes = new List<string>(){"note1", "note2"}
                    }
                }
                }
            };


            // Act
            string json = JsonConvert.SerializeObject(order, Formatting.Indented);

        }
    }
}
