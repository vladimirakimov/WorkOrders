using ITG.Brix.WorkOrders.API.Context.Services;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using ITG.Brix.WorkOrders.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.WorkOrders.API.Extensions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.API.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class WorkOrdersController : Controller
    {
        private readonly IApiResult _apiResult;

        public WorkOrdersController(IApiResult apiResult)
        {
            _apiResult = apiResult ?? throw new ArgumentNullException(nameof(apiResult));
        }

        [HttpGet]
        [ProducesResponseType(typeof(WorkOrdersModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] ListWorkOrderFromQuery query)
        {
            var request = new ListWorkOrderRequest(query);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkOrderModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetWorkOrderFromRoute route,
                                             [FromQuery] GetWorkOrderFromQuery query)
        {
            var request = new GetWorkOrderRequest(route, query);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromQuery] CreateWorkOrderFromQuery query,
                                                [FromBody] CreateWorkOrderFromBody body)
        {
            var request = new CreateWorkOrderRequest(query, body);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [Route("Create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create()
        {
            var bodyString = await Request.GetRawBodyStringAsync();

            var request = new CreatePlatoOrderRequest(bodyString);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> Update([FromRoute] UpdateWorkOrderFromRoute route,
                                                [FromQuery] UpdateWorkOrderFromQuery query,
                                                [FromHeader] UpdateWorkOrderFromHeader header)
        {
            var bodyAsString = await Request.GetRawBodyStringAsync();
            var body = new UpdateWorkOrderFromBody { Patch = bodyAsString };

            var request = new UpdateWorkOrderRequest(route, query, header, body);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> Delete([FromRoute] DeleteWorkOrderFromRoute route,
                                                [FromQuery] DeleteWorkOrderFromQuery query,
                                                [FromHeader] DeleteWorkOrderFromHeader header)
        {
            var request = new DeleteWorkOrderRequest(route, query, header);

            var result = await _apiResult.Produce(request);

            return result;
        }
    }
}
