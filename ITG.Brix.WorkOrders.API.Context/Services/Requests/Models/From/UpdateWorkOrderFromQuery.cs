using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From
{
    public class UpdateWorkOrderFromQuery
    {
        [FromQuery(Name = "api-version")]
        public string ApiVersion { get; set; }
    }
}
