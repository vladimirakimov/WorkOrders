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
    public class ListProductItemQueryHandler : IRequestHandler<ListProductItemQuery, Result>
    {
        public async Task<Result> Handle(ListProductItemQuery request, CancellationToken cancellationToken)
        {
            await Task.Yield();

            var productItemModels = new List<ProductItemModel>();

            var productItems = Item.ListProductItem();
            foreach (var productItem in productItems)
            {
                productItemModels.Add(new ProductItemModel { Code = productItem.Key.Value, Item = productItem.Value });
            }

            var count = productItemModels.Count;
            var productItemsModel = new ProductItemsModel { Value = productItemModels, Count = count, NextLink = null };

            return Result.Ok(productItemsModel);
        }
    }
}
