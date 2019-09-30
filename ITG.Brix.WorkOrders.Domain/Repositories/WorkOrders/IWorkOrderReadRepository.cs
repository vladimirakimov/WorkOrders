using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Domain.Repositories
{
    public interface IWorkOrderReadRepository : IRepository<WorkOrder>
    {
        Task<IList<WorkOrder>> ListAsync(string filter, int? skip, int? limit);
        Task<WorkOrder> GetAsync(Guid id);
    }
}
