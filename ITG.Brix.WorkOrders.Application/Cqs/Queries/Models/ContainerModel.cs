namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Models
{
    public class ContainerModel
    {
        public string Number { get; set; }
        public string Location { get; set; }
        public string StackLocation { get; set; }

        public FreeUntilOnTerminalModel FreeUntilOnTerminal { get; set; }
    }
}
