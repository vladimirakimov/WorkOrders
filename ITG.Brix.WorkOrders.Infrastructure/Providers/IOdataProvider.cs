using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels;
using System;
using System.Linq.Expressions;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers
{
    public interface IOdataProvider
    {
        Expression<Func<WorkOrderClass, bool>> GetFilterPredicate(string filter);
    }
}
