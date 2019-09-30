using System;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Domain.Repositories
{
    public interface IWorkOrderWriteRepository : IRepository<WorkOrder>
    {
        Task CreateAsync(WorkOrder workOrder);
        Task UpdateAsync(WorkOrder workOrder);
        Task DeleteAsync(Guid id, int version);
    }
}
