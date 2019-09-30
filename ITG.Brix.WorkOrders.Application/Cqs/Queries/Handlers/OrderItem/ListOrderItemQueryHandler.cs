using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Models;
using ITG.Brix.WorkOrders.Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Handlers
{
    public class ListOrderItemQueryHandler : IRequestHandler<ListOrderItemQuery, Result>
    {
        public async Task<Result> Handle(ListOrderItemQuery request, CancellationToken cancellationToken)
        {
            await Task.Yield();

            var orderItemModels = new List<OrderItemModel>();

            var orderItems = Item.ListOrderItem();
            foreach (var orderItem in orderItems)
            {
                orderItemModels.Add(new OrderItemModel { Code = orderItem.Key.Value, Item = orderItem.Value });
            }

            var count = orderItemModels.Count;
            var orderItemsModel = new OrderItemsModel { Value = orderItemModels, Count = count, NextLink = null };

            return Result.Ok(orderItemsModel);
        }
    }
}
