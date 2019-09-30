using ITG.Brix.WorkOrders.Domain;

namespace ITG.Brix.WorkOrders.Application.Services
{
    public interface IPlatoOrderChecker
    {
        void Check(PlatoOrderOverview platoOrderOverview);
    }
}
