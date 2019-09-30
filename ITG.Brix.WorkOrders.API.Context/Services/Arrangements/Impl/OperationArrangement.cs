using ITG.Brix.WorkOrders.API.Context.Services.Arrangements.Bases;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Mappers;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Responses;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.API.Context.Services.Arrangements.Impl
{
    public class OperationArrangement : IOperationArrangement
    {
        private readonly IMediator _mediator;
        private readonly IApiResponse _apiResponse;
        private readonly ICqsMapper _cqsMapper;

        public OperationArrangement(IMediator mediator,
                                    IApiResponse apiResponse,
                                    ICqsMapper cqsMapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _apiResponse = apiResponse ?? throw new ArgumentNullException(nameof(apiResponse));
            _cqsMapper = cqsMapper ?? throw new ArgumentNullException(nameof(cqsMapper));
        }

        public async Task<IActionResult> Process(ListWorkOrderRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.Unescape();

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<WorkOrdersModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(GetWorkOrderRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<WorkOrderModel>)result).Value, ((Result<WorkOrderModel>)result).Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreatePlatoOrderRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                  : _apiResponse.Created(string.Format("/api/workorders/create/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateWorkOrderRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/workorders/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(UpdateWorkOrderRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Updated(result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(DeleteWorkOrderRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Deleted();
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(ListPropertyRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var query = _cqsMapper.Map(request);
                var result = await _mediator.Send(query);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<PropertiesModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(ListOrderItemRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var query = _cqsMapper.Map(request);
                var result = await _mediator.Send(query);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<OrderItemsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(ListProductItemRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var query = _cqsMapper.Map(request);
                var result = await _mediator.Send(query);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<ProductItemsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
    }
}
