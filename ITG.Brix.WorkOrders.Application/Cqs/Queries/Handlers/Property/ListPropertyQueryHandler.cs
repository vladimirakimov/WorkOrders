using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Models;
using ITG.Brix.WorkOrders.Application.Exceptions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Handlers
{
    public class ListPropertyQueryHandler : IRequestHandler<ListPropertyQuery, Result>
    {
        private readonly IWorkOrderProvider _workOrderProvider;

        public ListPropertyQueryHandler(IWorkOrderProvider workOrderProvider)
        {
            _workOrderProvider = workOrderProvider ?? throw Error.ArgumentNull(nameof(workOrderProvider));
        }

        public async Task<Result> Handle(ListPropertyQuery request, CancellationToken cancellationToken)
        {
            await Task.Yield();
            var allProperties = new List<PropertyModel>();

            var workOrderPropertyTypePairs = _workOrderProvider.GetPropertyTypePairs();
            foreach (var pair in workOrderPropertyTypePairs)
            {
                allProperties.Add(new PropertyModel { Property = pair.Key, Type = pair.Value });
            }

            var count = allProperties.Count;
            var propertiesModel = new PropertiesModel { Value = allProperties, Count = count, NextLink = null };

            return Result.Ok(propertiesModel);
        }
    }
}
