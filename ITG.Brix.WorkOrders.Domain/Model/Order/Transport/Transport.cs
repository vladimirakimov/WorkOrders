namespace ITG.Brix.WorkOrders.Domain
{
    public class Transport
    {
        public Transport()
        {
            Driver = new Driver();
            Delivery = new Delivery();
            Loading = new Loading();
            Truck = new Truck();
            Container = new Container();
            Railcar = new Railcar();
            Ard = new Ard();
            Arrival = new Arrival();
            BillOfLading = new BillOfLading();
            Carrier = new Carrier();
            Weighbridge = new Weighbridge();
            Seal = new Seal();
        }
        public string Kind { get; set; }
        public string Type { get; set; }
        public Driver Driver { get; set; }
        public Delivery Delivery { get; set; }
        public Loading Loading { get; set; }
        public Truck Truck { get; set; }
        public Container Container { get; set; }
        public Railcar Railcar { get; set; }
        public Ard Ard { get; set; }
        public Arrival Arrival { get; set; }
        public BillOfLading BillOfLading { get; set; }
        public Carrier Carrier { get; set; }
        public Weighbridge Weighbridge { get; set; }
        public Seal Seal { get; set; }
        public string Adr { get; set; }
    }
}
