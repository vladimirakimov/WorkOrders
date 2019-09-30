using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using ITG.Brix.WorkOrders.Infrastructure.Orchestrations;
using ITG.Brix.WorkOrders.Infrastructure.RestApis;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Infrastructure.Orchestrators.Impl
{
    public class Orchestrator : IOrchestrator
    {
        private readonly IBiztalkRestApi _biztalkRestApi;
        private readonly IBiztalkOrchestration _biztalkOrchestration;

        public Orchestrator(IBiztalkRestApi biztalkRestApi,
                            IBiztalkOrchestration biztalkOrchestration)
        {
            _biztalkRestApi = biztalkRestApi ?? throw Error.ArgumentNull(nameof(biztalkRestApi));
            _biztalkOrchestration = biztalkOrchestration ?? throw Error.ArgumentNull(nameof(biztalkOrchestration));
        }
        public async Task<string> GetOrder(string jsonBody)
        {
            var jsonPlatoOrderFull = await _biztalkRestApi.GetOrder(jsonBody);
            _biztalkOrchestration.Acknowledge(jsonPlatoOrderFull);

            return jsonPlatoOrderFull;
        }
    }
}
