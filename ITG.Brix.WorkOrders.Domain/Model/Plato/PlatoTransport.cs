﻿using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class PlatoTransport
    {
        public string Source { get; set; }
        public string ID { get; set; }
        public string TransportNo { get; set; }
        public string RelationType { get; set; }
        public string OperationGroup { get; set; }
        public string Operation { get; set; }
        public string UnitPlanning { get; set; }
        public string TypePlanning { get; set; }
        public string Customer { get; set; }
        public string CustomerReference1 { get; set; }
        public string CustomerReference2 { get; set; }
        public string CustomerReference3 { get; set; }
        public string CustomerReference4 { get; set; }
        public string CustomerReference5 { get; set; }
        public string LoadingReference { get; set; }
        public string ARDReference1 { get; set; }
        public string ARDReference2 { get; set; }
        public string ARDReference3 { get; set; }
        public string ARDReference4 { get; set; }
        public string ARDReference5 { get; set; }
        public string ARDReference6 { get; set; }
        public string ARDReference7 { get; set; }
        public string ARDReference8 { get; set; }
        public string ARDReference9 { get; set; }
        public string ARDReference10 { get; set; }
        public string ProductionSite { get; set; }
        public string LoadingPlace { get; set; }
        public string DeliveryPlace { get; set; }
        public string BillOfLading { get; set; }
        public string BLNetWeight { get; set; }
        public string BLGrossWeight { get; set; }
        public string CertificateOfOrigin { get; set; }
        public string CarrierBooked { get; set; }
        public string CarrierArrived { get; set; }
        public string Kind { get; set; }
        public string TransportType { get; set; }
        public string DriverWaits { get; set; }
        public string Driver { get; set; }
        public string LicensePlateTruck { get; set; }
        public string LicensePlateTrailer { get; set; }
        public string Container { get; set; }
        public string Railcar { get; set; }
        public string Seal1 { get; set; }
        public string Seal2 { get; set; }
        public string Seal3 { get; set; }
        public string NetWeightWeighbridge { get; set; }
        public string GrossWeightWeighbridge { get; set; }
        public string FreeUntilOnTerminal { get; set; }
        public string FreeUntilOnTerminalCustomer { get; set; }
        public string ADR { get; set; }
        public string CustomsDocument { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentOffice { get; set; }
        public string DocumentDate { get; set; }
        public string ETA { get; set; }
        public string LTA { get; set; }
        public string Arrived { get; set; }
        public string Site { get; set; }
        public string OperationalDepartment { get; set; }
        public string DockingZone { get; set; }
        public string ContainerLocation { get; set; }
        public string ContainerStackLocation { get; set; }
        public string EorderPriority { get; set; }
        public PlatoDispatch Dispatch { get; set; }
        public string Zone { get; set; }
        public string ProductOverview { get; set; }
        public string LotbatchOverview { get; set; }
        public List<string> OperationalRemarks { get; set; }
        public List<PlatoExtraActivity> ExtraActivities { get; set; }
        public List<PlatoProductEntry> ProductEntries { get; set; }
    }
}
