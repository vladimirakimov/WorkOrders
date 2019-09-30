using ITG.Brix.WorkOrders.API.Context.Services.Arrangements.Bases;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.API.Context.Services.Arrangements
{
    public interface IOperationArrangement
    {
        Task<IActionResult> Process(CreatePlatoOrderRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListWorkOrderRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(GetWorkOrderRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateWorkOrderRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(UpdateWorkOrderRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(DeleteWorkOrderRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListPropertyRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListOrderItemRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListProductItemRequest request, IValidatorActionResult validatorActionResult);
    }
}
