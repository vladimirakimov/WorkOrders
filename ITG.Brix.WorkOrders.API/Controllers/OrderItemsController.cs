﻿using ITG.Brix.WorkOrders.API.Context.Services;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using ITG.Brix.WorkOrders.API.Context.Services.Responses.Models.Errors;
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
    public class OrderItemsController : Controller
    {
        private readonly IApiResult _apiResult;

        public OrderItemsController(IApiResult apiResult)
        {
            _apiResult = apiResult ?? throw new ArgumentNullException(nameof(apiResult));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PropertiesModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] ListOrderItemFromQuery query)
        {
            var request = new ListOrderItemRequest(query);

            var result = await _apiResult.Produce(request);

            return result;
        }
    }
}
