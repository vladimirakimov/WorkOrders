using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain.Diagnostics;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class ItemKey
    {
        public static readonly ItemKey Source = new ItemKey("#source");
        public static readonly ItemKey CustomerName = new ItemKey("#customer");
        public static readonly ItemKey TransportNumber = new ItemKey("#transport_number");
        public static readonly ItemKey LoadingPlace = new ItemKey("#loading_place");
        public static readonly ItemKey LoadingReference = new ItemKey("#loading_reference");

        public static readonly ItemKey Site = new ItemKey("#site");
        public static readonly ItemKey OperationalDepartment = new ItemKey("#operational_department");
        public static readonly ItemKey DockingZone = new ItemKey("#dockingzone");

        public static readonly ItemKey LicensePlateTruck = new ItemKey("#license_plate_truck");
        public static readonly ItemKey LicensePlateTrailer = new ItemKey("#license_plate_trailer");

        public static readonly ItemKey ContainerNumber = new ItemKey("#container_number");
        public static readonly ItemKey ContainerLocation = new ItemKey("#container_location");
        public static readonly ItemKey ContainerStackLocation = new ItemKey("#container_stacklocation");


        public static readonly ItemKey SourceOrderId = new ItemKey("#source_order_id");
        public static readonly ItemKey OperationType = new ItemKey("#operation_type");
        public static readonly ItemKey OperationGroup = new ItemKey("#operation_group");
        public static readonly ItemKey Operation = new ItemKey("#operation");
        public static readonly ItemKey UnitPlanning = new ItemKey("#unit_planning");
        public static readonly ItemKey TypePlanning = new ItemKey("#type_planning");

        public static readonly ItemKey Reference1 = new ItemKey("#reference1");
        public static readonly ItemKey Reference2 = new ItemKey("#reference2");
        public static readonly ItemKey Reference3 = new ItemKey("#reference3");
        public static readonly ItemKey Reference4 = new ItemKey("#reference4");
        public static readonly ItemKey Reference5 = new ItemKey("#reference5");

        public static readonly ItemKey ArdReference1 = new ItemKey("#ard_reference1");
        public static readonly ItemKey ArdReference2 = new ItemKey("#ard_reference2");
        public static readonly ItemKey ArdReference3 = new ItemKey("#ard_reference3");
        public static readonly ItemKey ArdReference4 = new ItemKey("#ard_reference4");
        public static readonly ItemKey ArdReference5 = new ItemKey("#ard_reference5");
        public static readonly ItemKey ArdReference6 = new ItemKey("#ard_reference6");
        public static readonly ItemKey ArdReference7 = new ItemKey("#ard_reference7");
        public static readonly ItemKey ArdReference8 = new ItemKey("#ard_reference8");
        public static readonly ItemKey ArdReference9 = new ItemKey("#ard_reference9");
        public static readonly ItemKey ArdReference10 = new ItemKey("#ard_reference10");

        public static readonly ItemKey ProductionSite = new ItemKey("#production_site");
        public static readonly ItemKey DeliveryPlace = new ItemKey("#delivery_place");
        public static readonly ItemKey BillOfLading = new ItemKey("#bill_of_lading");
        public static readonly ItemKey BillOfLadingWeightNet = new ItemKey("#bill_of_lading_weight_net");
        public static readonly ItemKey BillOfLadingWeightGross = new ItemKey("#bill_of_lading_weight_gross");
        public static readonly ItemKey CertificateOfOrigin = new ItemKey("#certificate_of_origin");
        public static readonly ItemKey CarrierBooked = new ItemKey("#carrier_booked");
        public static readonly ItemKey CarrierArrived = new ItemKey("#carrier_arrived");
        public static readonly ItemKey TransportKind = new ItemKey("#transport_kind");
        public static readonly ItemKey TransportType = new ItemKey("#transport_type");
        public static readonly ItemKey DriverWait = new ItemKey("#driver_wait");
        public static readonly ItemKey Driver = new ItemKey("#driver");

        public static readonly ItemKey Railcar = new ItemKey("#railcar");
        public static readonly ItemKey Seal1 = new ItemKey("#seal1");
        public static readonly ItemKey Seal2 = new ItemKey("#seal2");
        public static readonly ItemKey Seal3 = new ItemKey("#seal3");
        public static readonly ItemKey WeighbridgeWeightNet = new ItemKey("#weighbridge_weight_net");
        public static readonly ItemKey WeighbridgeWeightGross = new ItemKey("#weighbridge_weight_gross");
        public static readonly ItemKey ContainerFreeUntilOnTerminal = new ItemKey("#container_free_until_on_terminal");
        public static readonly ItemKey ContainerFreeUntilOnTerminalCustomerAgreement = new ItemKey("#container_free_until_on_terminal_customer_agreement");
        public static readonly ItemKey Adr = new ItemKey("#adr");

        public static readonly ItemKey CustomsDocument = new ItemKey("#customs_document");
        public static readonly ItemKey CustomsDocumentNumber = new ItemKey("#customs_document_number");
        public static readonly ItemKey CustomsDocumentOffice = new ItemKey("#customs_document_office");
        public static readonly ItemKey CustomsDocumentDate = new ItemKey("#customs_document_date");

        public static readonly ItemKey ArrivalExpected = new ItemKey("#arrival_expected");
        public static readonly ItemKey ArrivalArrived = new ItemKey("#arrival_arrived");
        public static readonly ItemKey ArrivalLatest = new ItemKey("#arrival_latest");

        public static readonly ItemKey LoadingDock = new ItemKey("#loading_dock");
        public static readonly ItemKey EOrderPriority = new ItemKey("#eorder_priority");
        public static readonly ItemKey EOrderPriorityValue = new ItemKey("#eorder_priority_value");
        public static readonly ItemKey DispatchPriority = new ItemKey("#dispatch_priority");
        public static readonly ItemKey DispatchTo = new ItemKey("#dispatch_to");
        public static readonly ItemKey DispatchComment = new ItemKey("#dispatch_comment");
        public static readonly ItemKey Zone = new ItemKey("#zone");
        public static readonly ItemKey ProductOverview = new ItemKey("#product_overview");
        public static readonly ItemKey LotbatchOverview = new ItemKey("#lotbatch_overview");


        public static readonly ItemKey ProductId = new ItemKey("#product_code");
        public static readonly ItemKey ProductCustomer = new ItemKey("#product_customer");
        public static readonly ItemKey ArrivalNumber = new ItemKey("#product_arrival");
        public static readonly ItemKey Article = new ItemKey("#product_article");
        public static readonly ItemKey ArticlePackagingCode = new ItemKey("#product_article_pkg_code");
        public static readonly ItemKey Product = new ItemKey("#product_product");
        public static readonly ItemKey Gtin = new ItemKey("#product_gtin");
        public static readonly ItemKey ProductType = new ItemKey("#product_product_type");
        public static readonly ItemKey MaterialType = new ItemKey("#product_material_type");
        public static readonly ItemKey Color = new ItemKey("#product_color");
        public static readonly ItemKey Shape = new ItemKey("#product_shape");
        public static readonly ItemKey ConfigurationName = new ItemKey("#product_configuration_code");
        public static readonly ItemKey ConfigurationDescription = new ItemKey("#product_configuration_description");
        public static readonly ItemKey ConfigurationQuantity = new ItemKey("#product_configuration_quantity");
        public static readonly ItemKey ConfigurationUnitType = new ItemKey("#product_configuration_unit_type");
        public static readonly ItemKey ConfigurationNetPerUnit = new ItemKey("#product_configuration_net_per_unit");
        public static readonly ItemKey ConfigurationNetPerUnitAlwaysDifferent = new ItemKey("#product_configuration_net_per_unit_always_different");
        public static readonly ItemKey Lotbatch = new ItemKey("#product_lotbatch");
        public static readonly ItemKey Lotbatch2 = new ItemKey("#product_lotbatch_2");
        public static readonly ItemKey ClientReference = new ItemKey("#product_client_reference");
        public static readonly ItemKey ClientReference2 = new ItemKey("#product_client_reference_2");
        public static readonly ItemKey BestBeforeDate = new ItemKey("#product_best_before_date");
        public static readonly ItemKey DateFifo = new ItemKey("#product_date_fifo");
        public static readonly ItemKey PalletNumber = new ItemKey("#product_pallet_number");
        public static readonly ItemKey SsccNumber = new ItemKey("#product_sscc_number");
        public static readonly ItemKey ProductCustomsDocument = new ItemKey("#product_customs_document");
        public static readonly ItemKey StorageStatus = new ItemKey("#product_storage_status");
        public static readonly ItemKey Stackheight = new ItemKey("#product_stack_height");
        public static readonly ItemKey Length = new ItemKey("#product_length");
        public static readonly ItemKey Width = new ItemKey("#product_width");
        public static readonly ItemKey Height = new ItemKey("#product_height");
        public static readonly ItemKey OriginalContainer = new ItemKey("#product_original_container");
        public static readonly ItemKey IsPartial = new ItemKey("#product_is_partial");
        public static readonly ItemKey IsMixed = new ItemKey("#product_is_mixed");
        public static readonly ItemKey MixedId = new ItemKey("#product_mixed_id");
        public static readonly ItemKey MixedPalletNumber = new ItemKey("#product_mixed_pallet_number");
        public static readonly ItemKey Quantity = new ItemKey("#product_quantity");
        public static readonly ItemKey QuantityShu = new ItemKey("#product_quantity_shu");
        public static readonly ItemKey UnitNetWeight = new ItemKey("#unit_weight_net");
        public static readonly ItemKey UnitGrossWeight = new ItemKey("#unit_weight_gross");
        public static readonly ItemKey ProductNetWeight = new ItemKey("#product_weight_net");
        public static readonly ItemKey ProductGrossWeight = new ItemKey("#product_weight_gross");
        public static readonly ItemKey Warehouse = new ItemKey("#unit_location_warehouse");
        public static readonly ItemKey Gate = new ItemKey("#unit_location_gate");
        public static readonly ItemKey Row = new ItemKey("#unit_location_row");
        public static readonly ItemKey Position = new ItemKey("#unit_location_position");


        public ItemKey(string value)
        {
            Guard.On(value, Error.ItemValueNullOrWhitespace()).AgainstNullOrWhiteSpace();
            Guard.On(value, Error.ItemValueFormat()).AgainstLowercasedWithUnderscoreAndDigitsAllowed();

            Value = value;
        }

        public string Value { get; }

        public static IEnumerable<ItemKey> List()
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
