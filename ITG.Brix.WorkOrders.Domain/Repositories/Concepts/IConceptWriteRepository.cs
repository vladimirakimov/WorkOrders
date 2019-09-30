using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Domain.Repositories
{
    public interface IConceptWriteRepository
    {
        Task CreateAsync(Concept concept);
    }
}
