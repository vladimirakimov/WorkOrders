using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels;

namespace ITG.Brix.WorkOrders.Infrastructure.Converters
{
    public interface IModelConverter
    {
        WorkOrder ToDomain(WorkOrderClass workOrderClass);
        WorkOrderClass ToClass(WorkOrder workOrder);
    }
}
