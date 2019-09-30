using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.Application.Cqs.Commands;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Mappers
{
    public interface ICqsMapper
    {
        CreatePlatoOrderCommand Map(CreatePlatoOrderRequest request);
        ListPropertyQuery Map(ListPropertyRequest request);
        ListOrderItemQuery Map(ListOrderItemRequest request);
        ListProductItemQuery Map(ListProductItemRequest request);
        ListWorkOrderQuery Map(ListWorkOrderRequest request);
        GetWorkOrderQuery Map(GetWorkOrderRequest request);
        DeleteWorkOrderCommand Map(DeleteWorkOrderRequest request);
        CreateWorkOrderCommand Map(CreateWorkOrderRequest request);
        UpdateWorkOrderCommand Map(UpdateWorkOrderRequest request);
    }
}
