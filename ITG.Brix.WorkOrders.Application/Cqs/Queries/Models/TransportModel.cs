namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Models
{
    public class TransportModel
    {
        public string Kind { get; set; }
        public string Type { get; set; }
        public DriverModel Driver { get; set; }
        public DeliveryModel Delivery { get; set; }
        public LoadingModel Loading { get; set; }
        public TruckModel Truck { get; set; }
        public ContainerModel Container { get; set; }
        public RailcarModel Railcar { get; set; }
        public ArdModel Ard { get; set; }
        public ArrivalModel Arrival { get; set; }
        public BillOfLadingModel BillOfLading { get; set; }
        public CarrierModel Carrier { get; set; }
        public WeighbridgeModel Weighbridge { get; set; }
        public SealModel Seal { get; set; }
        public string Adr { get; set; }
    }
}
