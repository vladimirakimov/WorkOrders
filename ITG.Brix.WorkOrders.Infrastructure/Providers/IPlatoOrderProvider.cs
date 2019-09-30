using ITG.Brix.WorkOrders.Domain;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers
{
    public interface IPlatoOrderProvider
    {
        PlatoOrderOverview GetPlatoOrderOverview(string jsonPlatoOrderOverview);
        PlatoOrderFull GetPlatoOrderFull(string jsonPlatoOrderFull);
    }
}
