namespace ITG.Brix.WorkOrders.Infrastructure.Orchestrations
{
    public interface IBiztalkOrchestration
    {
        void Acknowledge(string content);
    }
}
