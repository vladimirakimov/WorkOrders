
using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class FilterKey
    {
        public static readonly FilterKey None = new FilterKey("none");
        public static readonly FilterKey Source = new FilterKey("source");
        public static readonly FilterKey CustomerName = new FilterKey("customer");
        public static readonly FilterKey TransportNumber = new FilterKey("number");
        public static readonly FilterKey LoadingPlace = new FilterKey("loading-place");
        public static readonly FilterKey LoadingReference = new FilterKey("loading-reference");

        public static readonly FilterKey Site = new FilterKey("site");
        public static readonly FilterKey OperationalDepartment = new FilterKey("department");
        public static readonly FilterKey DockingZone = new FilterKey("dockingzone");

        public static readonly FilterKey LicensePlateTruck = new FilterKey("license-plate-truck");
        public static readonly FilterKey LicensePlateTrailer = new FilterKey("license-plate-trailer");

        public static readonly FilterKey ContainerNumber = new FilterKey("container-no");
        public static readonly FilterKey ContainerLocation = new FilterKey("container-loc");
        public static readonly FilterKey ContainerStackLocation = new FilterKey("container-st-loc");


        public static readonly FilterKey SourceOrderId = new FilterKey("src-order-entry");
        public static readonly FilterKey OperationType = new FilterKey("operation-type");
        public static readonly FilterKey OperationGroup = new FilterKey("operation-group");
        public static readonly FilterKey Operation = new FilterKey("operation");
        public static readonly FilterKey UnitPlanning = new FilterKey("unit-planning");
        public static readonly FilterKey TypePlanning = new FilterKey("type-planning");

        public static readonly FilterKey Reference1 = new FilterKey("ref1");
        public static readonly FilterKey Reference2 = new FilterKey("ref2");
        public static readonly FilterKey Reference3 = new FilterKey("ref3");
        public static readonly FilterKey Reference4 = new FilterKey("ref4");
        public static readonly FilterKey Reference5 = new FilterKey("ref5");

        public static readonly FilterKey ArdReference1 = new FilterKey("ard1");
        public static readonly FilterKey ArdReference2 = new FilterKey("ard2");
        public static readonly FilterKey ArdReference3 = new FilterKey("ard3");
        public static readonly FilterKey ArdReference4 = new FilterKey("ard4");
        public static readonly FilterKey ArdReference5 = new FilterKey("ard5");
        public static readonly FilterKey ArdReference6 = new FilterKey("ard6");
        public static readonly FilterKey ArdReference7 = new FilterKey("ard7");
        public static readonly FilterKey ArdReference8 = new FilterKey("ard8");
        public static readonly FilterKey ArdReference9 = new FilterKey("ard9");
        public static readonly FilterKey ArdReference10 = new FilterKey("ard10");

        public static readonly FilterKey ProductionSite = new FilterKey("prodsite");
        public static readonly FilterKey DeliveryPlace = new FilterKey("delivery-place");
        public static readonly FilterKey BillOfLading = new FilterKey("bol");
        public static readonly FilterKey BillOfLadingWeightNet = new FilterKey("bol-net");
        public static readonly FilterKey BillOfLadingWeightGross = new FilterKey("bol-gross");
        public static readonly FilterKey CertificateOfOrigin = new FilterKey("certorig");
        public static readonly FilterKey CarrierBooked = new FilterKey("carrier-booked");
        public static readonly FilterKey CarrierArrived = new FilterKey("carrier-arrived");
        public static readonly FilterKey TransportKind = new FilterKey("transport-kind");
        public static readonly FilterKey TransportType = new FilterKey("transport-type");
        public static readonly FilterKey DriverWait = new FilterKey("driver-wait");
        public static readonly FilterKey Driver = new FilterKey("driver");

        public static readonly FilterKey Railcar = new FilterKey("railcar");
        public static readonly FilterKey Seal1 = new FilterKey("seal1");
        public static readonly FilterKey Seal2 = new FilterKey("seal2");
        public static readonly FilterKey Seal3 = new FilterKey("seal3");
        public static readonly FilterKey WeighbridgeWeightNet = new FilterKey("weighbridge-net");
        public static readonly FilterKey WeighbridgeWeightGross = new FilterKey("weighbridge-gross");
        public static readonly FilterKey ContainerFreeUntilOnTerminal = new FilterKey("fuot");
        public static readonly FilterKey ContainerFreeUntilOnTerminalCustomerAgreement = new FilterKey("fuotcustomer");
        public static readonly FilterKey Adr = new FilterKey("adr");

        public static readonly FilterKey CustomsDocument = new FilterKey("customsdoc");
        public static readonly FilterKey CustomsDocumentNumber = new FilterKey("customsdoc-no");
        public static readonly FilterKey CustomsDocumentOffice = new FilterKey("customsdoc-office");
        public static readonly FilterKey CustomsDocumentDate = new FilterKey("customsdoc-date");

        public static readonly FilterKey ArrivalExpected = new FilterKey("arrival-expected");
        public static readonly FilterKey ArrivalArrived = new FilterKey("arrival-arrived");
        public static readonly FilterKey ArrivalLatest = new FilterKey("arrival-latest");

        public static readonly FilterKey LoadingDock = new FilterKey("loading-dock");
        public static readonly FilterKey EOrderPriority = new FilterKey("eo-priority");
        public static readonly FilterKey EOrderPriorityValue = new FilterKey("eo-priority-value");
        public static readonly FilterKey DispatchPriority = new FilterKey("dispatch-priority");
        public static readonly FilterKey DispatchTo = new FilterKey("dispatch-to");
        public static readonly FilterKey DispatchComment = new FilterKey("dispatch-comment");
        public static readonly FilterKey Zone = new FilterKey("zone");
        public static readonly FilterKey ProductOverview = new FilterKey("product-overview");
        public static readonly FilterKey LotbatchOverview = new FilterKey("lotbatch-overview");

        public static readonly FilterKey ProductId = new FilterKey("product-code");
        public static readonly FilterKey ProductCustomer = new FilterKey("product-customer");
        public static readonly FilterKey ArrivalNumber = new FilterKey("product-arrival");
        public static readonly FilterKey Article = new FilterKey("product-article");
        public static readonly FilterKey ArticlePackagingCode = new FilterKey("product-article-pkg-code");
        public static readonly FilterKey Product = new FilterKey("prdct");
        public static readonly FilterKey Gtin = new FilterKey("product-gtin");
        public static readonly FilterKey ProductType = new FilterKey("product-product-type");
        public static readonly FilterKey MaterialType = new FilterKey("product-material-type");
        public static readonly FilterKey Color = new FilterKey("product-color");
        public static readonly FilterKey Shape = new FilterKey("product-shape");
        public static readonly FilterKey ConfigurationName = new FilterKey("product-conf-code");
        public static readonly FilterKey ConfigurationDescription = new FilterKey("product-cfg-description");
        public static readonly FilterKey ConfigurationQuantity = new FilterKey("product-cfg-quantity");
        public static readonly FilterKey ConfigurationUnitType = new FilterKey("product-cfg-unit-type");
        public static readonly FilterKey ConfigurationNetPerUnit = new FilterKey("product-cfg-net-per-unit");
        public static readonly FilterKey ConfigurationNetPerUnitAlwaysDifferent = new FilterKey("product-cfg-net-per-unit-always-different");
        public static readonly FilterKey Lotbatch = new FilterKey("product-lotbatch");
        public static readonly FilterKey Lotbatch2 = new FilterKey("product-lotbatch2");
        public static readonly FilterKey ClientReference = new FilterKey("product-client-reference");
        public static readonly FilterKey ClientReference2 = new FilterKey("product-client-reference");
        public static readonly FilterKey BestBeforeDate = new FilterKey("product-best-before-date");
        public static readonly FilterKey DateFifo = new FilterKey("product-date-fifo");
        public static readonly FilterKey PalletNumber = new FilterKey("product-pallet-number");
        public static readonly FilterKey SsccNumber = new FilterKey("product-sscc-number");
        public static readonly FilterKey ProductCustomsDocument = new FilterKey("product-customs-document");
        public static readonly FilterKey StorageStatus = new FilterKey("product-storage-status");
        public static readonly FilterKey Stackheight = new FilterKey("product-stack-height");
        public static readonly FilterKey Length = new FilterKey("product-length");
        public static readonly FilterKey Width = new FilterKey("product-width");
        public static readonly FilterKey Height = new FilterKey("product-height");
        public static readonly FilterKey OriginalContainer = new FilterKey("product-original-container");
        public static readonly FilterKey IsPartial = new FilterKey("product-is-partial");
        public static readonly FilterKey IsMixed = new FilterKey("product-is-mixed");
        public static readonly FilterKey MixedId = new FilterKey("product-mixed-id");
        public static readonly FilterKey MixedPalletNumber = new FilterKey("product-mixed-pallet-number");
        public static readonly FilterKey Quantity = new FilterKey("product-qty");
        public static readonly FilterKey QuantityShu = new FilterKey("product-qty-shu");
        public static readonly FilterKey UnitNetWeight = new FilterKey("unit-weightnet");
        public static readonly FilterKey UnitGrossWeight = new FilterKey("unit-weightgross");
        public static readonly FilterKey ProductNetWeight = new FilterKey("product-weightnet");
        public static readonly FilterKey ProductGrossWeight = new FilterKey("product-weightgross");
        public static readonly FilterKey Warehouse = new FilterKey("product-location-warehouse");
        public static readonly FilterKey Gate = new FilterKey("product-location-gate");
        public static readonly FilterKey Row = new FilterKey("product-location-row");
        public static readonly FilterKey Position = new FilterKey("product-location-position");

        public FilterKey(string value)
        {
            Guard.On(value, Error.FilterKeyValueNullOrWhitespace()).AgainstNullOrWhiteSpace();
            Guard.On(value, Error.FilterKeyValueFormat()).AgainstLowercasedWithDashAndDigitsAllowed();

            Value = value;
        }

        public string Value { get; }

        public static IEnumerable<FilterKey> List()
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
