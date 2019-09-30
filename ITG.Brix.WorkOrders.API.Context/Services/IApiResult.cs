using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.API.Context.Services
{
    public interface IApiResult
    {
        Task<IActionResult> Produce(ListWorkOrderRequest request);
        Task<IActionResult> Produce(GetWorkOrderRequest request);
        Task<IActionResult> Produce(CreateWorkOrderRequest request);
        Task<IActionResult> Produce(UpdateWorkOrderRequest request);
        Task<IActionResult> Produce(DeleteWorkOrderRequest request);

        Task<IActionResult> Produce(ListPropertyRequest request);
        Task<IActionResult> Produce(ListOrderItemRequest request);
        Task<IActionResult> Produce(ListProductItemRequest request);
        Task<IActionResult> Produce(CreatePlatoOrderRequest request);
    }
}
