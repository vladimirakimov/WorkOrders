﻿using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From
{
    public class GetWorkOrderFromQuery
    {
        [FromQuery(Name = "api-version")]
        public string ApiVersion { get; set; }
    }
}
