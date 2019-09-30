using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Domain.Repositories
{
    public interface IPlatoOrderWriteRepository
    {
        Task CreateAsync(PlatoOrder platoOrder);
    }
}
