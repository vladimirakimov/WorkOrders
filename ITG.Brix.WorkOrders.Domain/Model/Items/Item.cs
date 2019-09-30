using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Item
    {
        public static readonly Item Source = new Item(ItemKey.Source, "Source", new Path(FilterKey.Source, "OrderOriginSource"), new Category(GeneralGroup.Identification, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item CustomerName = new Item(ItemKey.CustomerName, "Customer", new Path(FilterKey.CustomerName, "OrderCustomerCode"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item TransportNumber = new Item(ItemKey.TransportNumber, "Transport number", new Path(FilterKey.TransportNumber, "OrderNumber"), new Category(GeneralGroup.Identification | GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item LoadingPlace = new Item(ItemKey.LoadingPlace, "Loading place", new Path(FilterKey.LoadingPlace, "OrderTransportLoadingPlace"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item LoadingReference = new Item(ItemKey.LoadingReference, "Loading reference", new Path(FilterKey.LoadingReference, "OrderTransportLoadingReference"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));

        public static readonly Item Site = new Item(ItemKey.Site, "Site", new Path(FilterKey.Site, "OrderOperationSite"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item OperationalDepartment = new Item(ItemKey.OperationalDepartment, "Operational department", new Path(FilterKey.OperationalDepartment, "OrderOperationOperationalDepartment"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item DockingZone = new Item(ItemKey.DockingZone, "Docking zone", new Path(FilterKey.DockingZone, "OrderOperationDockingZone"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));

        public static readonly Item LicensePlateTruck = new Item(ItemKey.LicensePlateTruck, "License Plate Truck", new Path(FilterKey.LicensePlateTruck, "OrderTransportTruckLicensePlateTruck"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item LicensePlateTrailer = new Item(ItemKey.LicensePlateTrailer, "License Plate Trailer", new Path(FilterKey.LicensePlateTrailer, "OrderTransportTruckLicensePlateTrailer"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));

        public static readonly Item ContainerNumber = new Item(ItemKey.ContainerNumber, "Container number", new Path(FilterKey.ContainerNumber, "OrderTransportContainerNumber"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ContainerLocation = new Item(ItemKey.ContainerLocation, "Container location", new Path(FilterKey.ContainerLocation, "OrderTransportContainerLocation"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ContainerStackLocation = new Item(ItemKey.ContainerStackLocation, "Container stack location", new Path(FilterKey.ContainerStackLocation, "OrderTransportContainerStackLocation"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));


        public static readonly Item SourceOrderId = new Item(ItemKey.SourceOrderId, "Source order ID", new Path(FilterKey.SourceOrderId, "OrderOriginEntryNumber"), new Category(GeneralGroup.Identification | GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item OperationType = new Item(ItemKey.OperationType, "Operation type", new Path(FilterKey.OperationType, "OrderOperationType"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item OperationGroup = new Item(ItemKey.OperationGroup, "Operation group", new Path(FilterKey.OperationGroup, "OrderOperationGroup"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Operation = new Item(ItemKey.Operation, "Operation", new Path(FilterKey.Operation, "OrderOperationName"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item UnitPlanning = new Item(ItemKey.UnitPlanning, "Unit planning", new Path(FilterKey.UnitPlanning, "OrderOperationUnitPlanning"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item TypePlanning = new Item(ItemKey.TypePlanning, "Type planning", new Path(FilterKey.TypePlanning, "OrderOperationTypePlanning"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));

        public static readonly Item Reference1 = new Item(ItemKey.Reference1, "Reference 1", new Path(FilterKey.Reference1, "OrderCustomerReference1"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Reference2 = new Item(ItemKey.Reference2, "Reference 2", new Path(FilterKey.Reference2, "OrderCustomerReference2"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Reference3 = new Item(ItemKey.Reference3, "Reference 3", new Path(FilterKey.Reference3, "OrderCustomerReference3"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Reference4 = new Item(ItemKey.Reference4, "Reference 4", new Path(FilterKey.Reference4, "OrderCustomerReference4"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Reference5 = new Item(ItemKey.Reference5, "Reference 5", new Path(FilterKey.Reference5, "OrderCustomerReference5"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));

        public static readonly Item ArdReference1 = new Item(ItemKey.ArdReference1, "ARD Reference 1", new Path(FilterKey.ArdReference1, "OrderTransportArdReference1"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArdReference2 = new Item(ItemKey.ArdReference2, "ARD Reference 2", new Path(FilterKey.ArdReference2, "OrderTransportArdReference2"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArdReference3 = new Item(ItemKey.ArdReference3, "ARD Reference 3", new Path(FilterKey.ArdReference3, "OrderTransportArdReference3"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArdReference4 = new Item(ItemKey.ArdReference4, "ARD Reference 4", new Path(FilterKey.ArdReference4, "OrderTransportArdReference4"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArdReference5 = new Item(ItemKey.ArdReference5, "ARD Reference 5", new Path(FilterKey.ArdReference5, "OrderTransportArdReference5"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArdReference6 = new Item(ItemKey.ArdReference6, "ARD Reference 6", new Path(FilterKey.ArdReference6, "OrderTransportArdReference6"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArdReference7 = new Item(ItemKey.ArdReference7, "ARD Reference 7", new Path(FilterKey.ArdReference7, "OrderTransportArdReference7"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArdReference8 = new Item(ItemKey.ArdReference8, "ARD Reference 8", new Path(FilterKey.ArdReference8, "OrderTransportArdReference8"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArdReference9 = new Item(ItemKey.ArdReference9, "ARD Reference 9", new Path(FilterKey.ArdReference9, "OrderTransportArdReference9"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArdReference10 = new Item(ItemKey.ArdReference10, "ARD Reference 10", new Path(FilterKey.ArdReference10, "OrderTransportArdReference10"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));

        public static readonly Item ProductionSite = new Item(ItemKey.ProductionSite, "Production site", new Path(FilterKey.ProductionSite, "OrderCustomerProductionSite"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item DeliveryPlace = new Item(ItemKey.DeliveryPlace, "Delivery place", new Path(FilterKey.DeliveryPlace, "OrderTransportDeliveryPlace"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item BillOfLading = new Item(ItemKey.BillOfLading, "Bill of lading", new Path(FilterKey.BillOfLading, "OrderTransportBillOfLadingNumber"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item BillOfLadingWeightNet = new Item(ItemKey.BillOfLadingWeightNet, "Net weight bill of lading", new Path(FilterKey.BillOfLadingWeightNet, "OrderTransportBillOfLadingWeightNet"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item BillOfLadingWeightGross = new Item(ItemKey.BillOfLadingWeightGross, "Gross weight bill of lading", new Path(FilterKey.BillOfLadingWeightGross, "OrderTransportBillOfLadingWeightGross"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item CertificateOfOrigin = new Item(ItemKey.CertificateOfOrigin, "Certificate of origin", new Path(FilterKey.CertificateOfOrigin, "OrderCustomsCertificateOfOrigin"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item CarrierBooked = new Item(ItemKey.CarrierBooked, "Carrier booked", new Path(FilterKey.CarrierBooked, "OrderTransportCarrierBooked"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item CarrierArrived = new Item(ItemKey.CarrierArrived, "Carrier arrived", new Path(FilterKey.CarrierArrived, "OrderTransportCarrierArrived"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item TransportKind = new Item(ItemKey.TransportKind, "Transport kind", new Path(FilterKey.TransportKind, "OrderTransportKind"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item TransportType = new Item(ItemKey.TransportType, "Transport type", new Path(FilterKey.TransportType, "OrderTransportType"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item DriverWait = new Item(ItemKey.DriverWait, "Driver waits", new Path(FilterKey.DriverWait, "OrderTransportDriverWait"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Driver = new Item(ItemKey.Driver, "Driver", new Path(FilterKey.Driver, "OrderTransportDriverName"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));

        public static readonly Item Railcar = new Item(ItemKey.Railcar, "Railcar", new Path(FilterKey.Railcar, "OrderTransportRailcarNumber"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Seal1 = new Item(ItemKey.Seal1, "Seal 1", new Path(FilterKey.Seal1, "OrderTransportSealSeal1"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Seal2 = new Item(ItemKey.Seal2, "Seal 2", new Path(FilterKey.Seal2, "OrderTransportSealSeal2"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Seal3 = new Item(ItemKey.Seal3, "Seal 3", new Path(FilterKey.Seal3, "OrderTransportSealSeal3"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item WeighbridgeWeightNet = new Item(ItemKey.WeighbridgeWeightNet, "Net weight weighbridge", new Path(FilterKey.WeighbridgeWeightNet, "OrderTransportWeighbridgeNet"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item WeighbridgeWeightGross = new Item(ItemKey.WeighbridgeWeightGross, "Gross weight weighbridge", new Path(FilterKey.WeighbridgeWeightGross, "OrderTransportWeighbridgeGross"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ContainerFreeUntilOnTerminal = new Item(ItemKey.ContainerFreeUntilOnTerminal, "Container free until on terminal", new Path(FilterKey.ContainerFreeUntilOnTerminal, "OrderTransportContainerFreeUntilOnTerminalShippingLine"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ContainerFreeUntilOnTerminalCustomerAgreement = new Item(ItemKey.ContainerFreeUntilOnTerminalCustomerAgreement, "Container free until on terminal (customer agreement)", new Path(FilterKey.ContainerFreeUntilOnTerminalCustomerAgreement, "OrderTransportContainerFreeUntilOnTerminalCustomer"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Adr = new Item(ItemKey.Adr, "ADR", new Path(FilterKey.Adr, "OrderTransportAdr"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));

        public static readonly Item CustomsDocument = new Item(ItemKey.CustomsDocument, "Customs document", new Path(FilterKey.CustomsDocument, "OrderCustomsDocumentName"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item CustomsDocumentNumber = new Item(ItemKey.CustomsDocumentNumber, "Customs document number", new Path(FilterKey.CustomsDocumentNumber, "OrderCustomsDocumentNumber"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item CustomsDocumentOffice = new Item(ItemKey.CustomsDocumentOffice, "Customs document office", new Path(FilterKey.CustomsDocumentOffice, "OrderCustomsDocumentOffice"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item CustomsDocumentDate = new Item(ItemKey.CustomsDocumentDate, "Customs document date", new Path(FilterKey.CustomsDocumentDate, "OrderCustomsDocumentDate"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));


        public static readonly Item ArrivalExpected = new Item(ItemKey.ArrivalExpected, "ETA", new Path(FilterKey.ArrivalExpected, "OrderTransportArrivalExpected"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArrivalArrived = new Item(ItemKey.ArrivalArrived, "Date arrived", new Path(FilterKey.ArrivalArrived, "OrderTransportArrivalArrived"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ArrivalLatest = new Item(ItemKey.ArrivalLatest, "LTA", new Path(FilterKey.ArrivalLatest, "OrderTransportArrivalLatest"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));

        public static readonly Item LoadingDock = new Item(ItemKey.LoadingDock, "Loading dock", new Path(FilterKey.LoadingDock, "OrderOperationLoadingDock"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item EOrderPriority = new Item(ItemKey.EOrderPriority, "eOrder priority", new Path(FilterKey.EOrderPriority, "OrderOperationPriorityCode"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item EOrderPriorityValue = new Item(ItemKey.EOrderPriorityValue, "eOrder priority value", new Path(FilterKey.EOrderPriorityValue, "OrderOperationPriorityValue"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item DispatchPriority = new Item(ItemKey.DispatchPriority, "Dispatch priority", new Path(FilterKey.DispatchPriority, "OrderOperationDispatchPriority"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item DispatchTo = new Item(ItemKey.DispatchTo, "Dispatch to", new Path(FilterKey.DispatchTo, "OrderOperationDispatchTo"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item DispatchComment = new Item(ItemKey.DispatchComment, "Dispatch comment", new Path(FilterKey.DispatchComment, "OrderOperationDispatchComment"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item Zone = new Item(ItemKey.Zone, "Zone", new Path(FilterKey.Zone, "OrderOperationZone"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item ProductOverview = new Item(ItemKey.ProductOverview, "Product overview", new Path(FilterKey.ProductOverview, "OrderOperationProductOverview"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));
        public static readonly Item LotbatchOverview = new Item(ItemKey.LotbatchOverview, "Lotbatch overview", new Path(FilterKey.LotbatchOverview, "OrderOperationLotbatchOverview"), new Category(GeneralGroup.Overview, TeamFilterGroup.None, ProductGroup.None));

        public static readonly Item ProductId = new Item(ItemKey.ProductId, "Product Id", new Path(FilterKey.ProductId, "Code"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ProductCustomer = new Item(ItemKey.ProductCustomer, "Product - Customer", new Path(FilterKey.ProductCustomer, "Customer"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ArrivalNumber = new Item(ItemKey.ArrivalNumber, "Arrival number", new Path(FilterKey.ArrivalNumber, "Arrival"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Article = new Item(ItemKey.Article, "Article", new Path(FilterKey.Article, "Arrival"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ArticlePackagingCode = new Item(ItemKey.ArticlePackagingCode, "Article packaging code", new Path(FilterKey.ArticlePackagingCode, "ArticlePackagingCode"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Product = new Item(ItemKey.Product, "Product", new Path(FilterKey.Product, "Name"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Gtin = new Item(ItemKey.Gtin, "Gtin", new Path(FilterKey.Gtin, "Gtin"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ProductType = new Item(ItemKey.ProductType, "Product type", new Path(FilterKey.ProductType, "ProductType"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item MaterialType = new Item(ItemKey.MaterialType, "Material type", new Path(FilterKey.MaterialType, "MaterialType"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Color = new Item(ItemKey.Color, "Color", new Path(FilterKey.Color, "Color"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Shape = new Item(ItemKey.Shape, "Shape", new Path(FilterKey.Shape, "Shape"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ConfigurationName = new Item(ItemKey.ConfigurationName, "Configuration name", new Path(FilterKey.ConfigurationName, "ConfigurationCode"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ConfigurationDescription = new Item(ItemKey.ConfigurationDescription, "Configuration description", new Path(FilterKey.ConfigurationDescription, "ConfigurationDescription"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ConfigurationQuantity = new Item(ItemKey.ConfigurationQuantity, "Configuration quantity", new Path(FilterKey.ConfigurationQuantity, "ConfigurationQuantity"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ConfigurationUnitType = new Item(ItemKey.ConfigurationUnitType, "Configuration unit type", new Path(FilterKey.ConfigurationUnitType, "ConfigurationUnitType"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ConfigurationNetPerUnit = new Item(ItemKey.ConfigurationNetPerUnit, "Configuration net per unit", new Path(FilterKey.ConfigurationNetPerUnit, "ConfigurationNetPerUnit"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ConfigurationNetPerUnitAlwaysDifferent = new Item(ItemKey.ConfigurationNetPerUnitAlwaysDifferent, "Configuration net per unit always different", new Path(FilterKey.ConfigurationNetPerUnitAlwaysDifferent, "ConfigurationNetPerUnitAlwaysDifferent"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));

        public static readonly Item Lotbatch = new Item(ItemKey.Lotbatch, "Lot-batch", new Path(FilterKey.Color, "Lotbatch"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Lotbatch2 = new Item(ItemKey.Lotbatch2, "Lot-batch 2", new Path(FilterKey.Lotbatch2, "Lotbatch2"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ClientReference = new Item(ItemKey.ClientReference, "Client reference", new Path(FilterKey.ClientReference, "ClientReference"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ClientReference2 = new Item(ItemKey.ClientReference2, "Client reference 2", new Path(FilterKey.ClientReference2, "Color"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item BestBeforeDate = new Item(ItemKey.BestBeforeDate, "Best before date", new Path(FilterKey.BestBeforeDate, "BestBeforeDate"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item DateFifo = new Item(ItemKey.DateFifo, "Date FIFO", new Path(FilterKey.DateFifo, "DateFifo"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item PalletNumber = new Item(ItemKey.PalletNumber, "Pallet number", new Path(FilterKey.PalletNumber, "PalletNumber"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item SsccNumber = new Item(ItemKey.SsccNumber, "SSCC number", new Path(FilterKey.SsccNumber, "SsccNumber"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ProductCustomsDocument = new Item(ItemKey.ProductCustomsDocument, "Product customs document", new Path(FilterKey.ProductCustomsDocument, "CustomsDocument"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item StorageStatus = new Item(ItemKey.StorageStatus, "Storage status", new Path(FilterKey.StorageStatus, "StorageStatus"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Stackheight = new Item(ItemKey.Stackheight, "Stack height", new Path(FilterKey.Stackheight, "Stackheight"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Length = new Item(ItemKey.Length, "Length", new Path(FilterKey.Length, "Length"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Width = new Item(ItemKey.Width, "Width", new Path(FilterKey.Width, "Width"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Height = new Item(ItemKey.Height, "Height", new Path(FilterKey.Height, "Height"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item OriginalContainer = new Item(ItemKey.OriginalContainer, "Original container", new Path(FilterKey.OriginalContainer, "Color"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item IsPartial = new Item(ItemKey.IsPartial, "Partial", new Path(FilterKey.IsPartial, "IsPartial"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item IsMixed = new Item(ItemKey.IsMixed, "Mixed", new Path(FilterKey.IsMixed, "IsMixed"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item MixedId = new Item(ItemKey.MixedId, "Mixed Id", new Path(FilterKey.MixedId, "MixedId"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item MixedPalletNumber = new Item(ItemKey.MixedPalletNumber, "Mixed pallet number", new Path(FilterKey.MixedPalletNumber, "Color"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Quantity = new Item(ItemKey.Quantity, "Quantity", new Path(FilterKey.Quantity, "Quantity"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item QuantityShu = new Item(ItemKey.QuantityShu, "Quantity SHU", new Path(FilterKey.QuantityShu, "QuantityShu"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item UnitNetWeight = new Item(ItemKey.UnitNetWeight, "Unit net weight", new Path(FilterKey.UnitNetWeight, "WeightNet"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item UnitGrossWeight = new Item(ItemKey.UnitGrossWeight, "Unit gross weight", new Path(FilterKey.UnitGrossWeight, "WeightGross"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ProductNetWeight = new Item(ItemKey.ProductNetWeight, "Product net weight", new Path(FilterKey.ProductNetWeight, "WeightNet"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item ProductGrossWeight = new Item(ItemKey.ProductGrossWeight, "Product gross weight", new Path(FilterKey.ProductGrossWeight, "WeightGross"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Warehouse = new Item(ItemKey.Warehouse, "Warehouse", new Path(FilterKey.Warehouse, "Warehouse"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Gate = new Item(ItemKey.Gate, "Gate", new Path(FilterKey.Gate, "Gate"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Row = new Item(ItemKey.Row, "Row", new Path(FilterKey.Row, "Row"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));
        public static readonly Item Position = new Item(ItemKey.Position, "Position", new Path(FilterKey.Position, "Position"), new Category(GeneralGroup.Execution, TeamFilterGroup.None, ProductGroup.On));



        public Item(ItemKey key, string value, Path path, Category category)
        {
            Key = key;
            Value = value;
            Path = path;
            Category = category;
        }

        public ItemKey Key { get; }
        public string Value { get; }
        public Path Path { get; }
        public Category Category { get; }

        public static IEnumerable<Item> ListOrderItem()
        {
            var result = List().Where(x => x.Category.General.HasFlag(GeneralGroup.Identification) || x.Category.General.HasFlag(GeneralGroup.Overview))
                               .OrderBy(o => o.Value)
                               .ToList();

            return result;
        }

        public static IEnumerable<Item> ListProductItem()
        {
            var result = List().Where(x => x.Category.Product.HasFlag(ProductGroup.On))
                               .OrderBy(o => o.Value)
                               .ToList();

            return result;
        }



        public static IDictionary<string, string> DictionaryOrderItemPath()
        {
            var items = ListOrderItem();
            var result = new Dictionary<string, string>();
            foreach (var item in items)
            {
                result.Add(item.Path.FilterKey.Value, item.Path.Property);
            }
            return result;
        }

        public static IEnumerable<Item> List()
        {
            return new[] {
                Source,
                CustomerName,
                TransportNumber,
                LoadingPlace,
                LoadingReference,
                Site,
                OperationalDepartment,
                DockingZone,
                LicensePlateTruck,
                LicensePlateTrailer,
                ContainerNumber,
                ContainerLocation,
                ContainerStackLocation,
                SourceOrderId,
                OperationType,
                OperationGroup,
                Operation,
                UnitPlanning,
                TypePlanning,
                Reference1,
                Reference2,
                Reference3,
                Reference4,
                Reference5,
                ArdReference1,
                ArdReference2,
                ArdReference3,
                ArdReference4,
                ArdReference5,
                ArdReference6,
                ArdReference7,
                ArdReference8,
                ArdReference9,
                ArdReference10,
                ProductionSite,
                DeliveryPlace,
                BillOfLading,
                BillOfLadingWeightNet,
                BillOfLadingWeightGross,
                CertificateOfOrigin,
                CarrierBooked,
                CarrierArrived,
                TransportKind,
                TransportType,
                DriverWait,
                Driver,
                Railcar,
                Seal1,
                Seal2,
                Seal3,
                WeighbridgeWeightNet,
                WeighbridgeWeightGross,
                ContainerFreeUntilOnTerminal,
                ContainerFreeUntilOnTerminalCustomerAgreement,
                Adr,
                CustomsDocument,
                CustomsDocumentNumber,
                CustomsDocumentOffice,
                CustomsDocumentDate,
                ArrivalExpected,
                ArrivalArrived,
                ArrivalLatest,
                LoadingDock,
                EOrderPriority,
                EOrderPriorityValue,
                DispatchPriority,
                DispatchTo,
                DispatchComment,
                Zone,
                ProductOverview,
                LotbatchOverview,

                ProductId,
                ProductCustomer,
                ArrivalNumber,
                Article,
                ArticlePackagingCode,
                Product,
                Gtin,
                ProductType,
                MaterialType,
                Color,
                Shape,
                ConfigurationName,
                ConfigurationDescription,
                ConfigurationQuantity,
                ConfigurationUnitType,
                ConfigurationNetPerUnit,
                ConfigurationNetPerUnitAlwaysDifferent,
                Lotbatch,
                Lotbatch2,
                ClientReference,
                ClientReference2,
                BestBeforeDate,
                DateFifo,
                PalletNumber,
                SsccNumber,
                ProductCustomsDocument,
                StorageStatus,
                Stackheight,
                Length,
                Width,
                Height,
                OriginalContainer,
                IsPartial,
                IsMixed,
                MixedId,
                MixedPalletNumber,
                Quantity,
                QuantityShu,
                UnitNetWeight,
                UnitGrossWeight,
                ProductNetWeight,
                ProductGrossWeight,
                Warehouse,
                Gate,
                Row ,
                Position
            };
        }
    }
}
