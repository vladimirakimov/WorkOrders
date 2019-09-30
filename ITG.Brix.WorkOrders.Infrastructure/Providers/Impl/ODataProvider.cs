using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using StringToExpression.LanguageDefinitions;
using System;
using System.Linq.Expressions;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers.Impl
{
    public class OdataProvider : IOdataProvider
    {
        public Expression<Func<WorkOrderClass, bool>> GetFilterPredicate(string filter)
        {
            Expression<Func<WorkOrderClass, bool>> result = null;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var language = new ODataFilterLanguage();
                try
                {
                    result = language.Parse<WorkOrderClass>(filter);
                }
                catch (Exception exception)
                {
                    throw new FilterODataException(exception);
                }
            }

            return result;
        }
    }
}
