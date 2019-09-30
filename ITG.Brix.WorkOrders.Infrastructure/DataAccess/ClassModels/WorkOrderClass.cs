using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels
{
    public class WorkOrderClass
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string UserCreated { get; set; }

        public long CreatedOn { get; set; }

        #region Order

        public string OrderNumber { get; set; }

        #region Order -> Origin

        public string OrderOriginSource { get; set; }
        public string OrderOriginEntryNumber { get; set; }

        #endregion

        #region Order -> Customer

        public string OrderCustomerCode { get; set; }
        public string OrderCustomerProductionSite { get; set; }
        public string OrderCustomerReference1 { get; set; }
        public string OrderCustomerReference2 { get; set; }
        public string OrderCustomerReference3 { get; set; }
        public string OrderCustomerReference4 { get; set; }
        public string OrderCustomerReference5 { get; set; }

        #endregion

        #region Order -> Customs

        public string OrderCustomsCertificateOfOrigin { get; set; }
        public string OrderCustomsDocumentName { get; set; }
        public string OrderCustomsDocumentNumber { get; set; }
        public string OrderCustomsDocumentOffice { get; set; }
        public long? OrderCustomsDocumentDate { get; set; }

        #endregion

        #region Order -> Transport

        public string OrderTransportKind { get; set; }
        public string OrderTransportType { get; set; }
        public string OrderTransportDriverName { get; set; }
        public string OrderTransportDriverWait { get; set; }
        public long? OrderTransportDeliveryEta { get; set; }
        public long? OrderTransportDeliveryLta { get; set; }
        public string OrderTransportDeliveryPlace { get; set; }
        public string OrderTransportDeliveryReference { get; set; }
        public long? OrderTransportLoadingEta { get; set; }
        public long? OrderTransportLoadingLta { get; set; }
        public string OrderTransportLoadingPlace { get; set; }
        public string OrderTransportLoadingReference { get; set; }
        public string OrderTransportTruckLicensePlateTruck { get; set; }
        public string OrderTransportTruckLicensePlateTrailer { get; set; }
        public string OrderTransportContainerNumber { get; set; }
        public string OrderTransportContainerLocation { get; set; }
        public string OrderTransportContainerStackLocation { get; set; }
        public string OrderTransportContainerFreeUntilOnTerminalShippingLine { get; set; }
        public string OrderTransportContainerFreeUntilOnTerminalCustomer { get; set; }
        public string OrderTransportRailcarNumber { get; set; }
        public string OrderTransportArdReference1 { get; set; }
        public string OrderTransportArdReference2 { get; set; }
        public string OrderTransportArdReference3 { get; set; }
        public string OrderTransportArdReference4 { get; set; }
        public string OrderTransportArdReference5 { get; set; }
        public string OrderTransportArdReference6 { get; set; }
        public string OrderTransportArdReference7 { get; set; }
        public string OrderTransportArdReference8 { get; set; }
        public string OrderTransportArdReference9 { get; set; }
        public string OrderTransportArdReference10 { get; set; }
        public long? OrderTransportArrivalExpected { get; set; }
        public long? OrderTransportArrivalLatest { get; set; }
        public long? OrderTransportArrivalArrived { get; set; }
        public string OrderTransportBillOfLadingNumber { get; set; }
        public string OrderTransportBillOfLadingWeightNet { get; set; }
        public string OrderTransportBillOfLadingWeightGross { get; set; }
        public string OrderTransportCarrierBooked { get; set; }
        public string OrderTransportCarrierArrived { get; set; }
        public string OrderTransportWeighbridgeNet { get; set; }
        public string OrderTransportWeighbridgeGross { get; set; }
        public string OrderTransportSealSeal1 { get; set; }
        public string OrderTransportSealSeal2 { get; set; }
        public string OrderTransportSealSeal3 { get; set; }
        public string OrderTransportAdr { get; set; }

        #endregion

        #region Order -> Units

        public IEnumerable<UnitClass> OrderUnits { get; set; }

        #endregion

        #region Order -> Operation

        public string OrderOperationPriorityCode { get; set; }
        public string OrderOperationPriorityValue { get; set; }

        public string OrderOperationDispatchPriority { get; set; }
        public string OrderOperationDispatchTo { get; set; }
        public string OrderOperationDispatchComment { get; set; }

        public IEnumerable<ExtraActivityClass> OrderOperationExtraActivities { get; set; }

        public string OrderOperationType { get; set; }
        public string OrderOperationName { get; set; }
        public string OrderOperationGroup { get; set; }
        public string OrderOperationUnitPlanning { get; set; }
        public string OrderOperationTypePlanning { get; set; }
        public string OrderOperationSite { get; set; }
        public string OrderOperationZone { get; set; }
        public string OrderOperationOperationalDepartment { get; set; }
        public IEnumerable<string> OrderOperationOperationalRemarks { get; set; }
        public string OrderOperationDockingZone { get; set; }
        public string OrderOperationLoadingDock { get; set; }
        public string OrderOperationProductOverview { get; set; }
        public string OrderOperationLotbatchOverview { get; set; }

        #endregion

        #endregion

        #region Operational

        public string OperationalOperant { get; set; }

        public string OperationalStatus { get; set; }

        public long? OperationalStartedOn { get; set; }
        public long? OperationalStopedOn { get; set; }

        public IEnumerable<HandledUnitClass> OperationalUnits { get; set; }

        public IEnumerable<RemarkClass> OperationalRemarks { get; set; }

        public IEnumerable<PictureClass> OperationalPictures { get; set; }

        public IEnumerable<InputClass> OperationalInputs { get; set; }

        #endregion
    }
}
