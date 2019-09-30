using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Infrastructure.Orchestrators
{
    public interface IOrchestrator
    {
        Task<string> GetOrder(string jsonBody);
    }
}
