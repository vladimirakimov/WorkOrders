namespace ITG.Brix.WorkOrders.Domain
{
    public class Container
    {
        public Container()
        {
            FreeUntilOnTerminal = new FreeUntilOnTerminal();
        }

        public string Number { get; set; }
        public string Location { get; set; }
        public string StackLocation { get; set; }

        public FreeUntilOnTerminal FreeUntilOnTerminal { get; set; }
    }
}
