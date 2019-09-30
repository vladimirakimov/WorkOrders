using ITG.Brix.WorkOrders.API.Context.Services.Arrangements;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.API.Context.Services.Impl
{
    public class ApiResult : IApiResult
    {
        private readonly IValidationArrangement _validationArrangement;
        private readonly IOperationArrangement _operationArrangement;

        public ApiResult(IValidationArrangement validationArrangement,
                         IOperationArrangement operationArrangement)
        {
            _validationArrangement = validationArrangement ?? throw new ArgumentNullException(nameof(validationArrangement));
            _operationArrangement = operationArrangement ?? throw new ArgumentNullException(nameof(operationArrangement));
        }

        public async Task<IActionResult> Produce(ListWorkOrderRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(GetWorkOrderRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreatePlatoOrderRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateWorkOrderRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(UpdateWorkOrderRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(DeleteWorkOrderRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(ListPropertyRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(ListOrderItemRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(ListProductItemRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
    }
}
