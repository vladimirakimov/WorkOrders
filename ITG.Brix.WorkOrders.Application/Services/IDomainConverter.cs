using ITG.Brix.WorkOrders.Domain;

namespace ITG.Brix.WorkOrders.Application.Services
{
    public interface IDomainConverter
    {
        Order ToOrder(PlatoOrderOverview platoOrderOverview);
        Order ToOrder(PlatoTransport platoTransport);
    }
}
