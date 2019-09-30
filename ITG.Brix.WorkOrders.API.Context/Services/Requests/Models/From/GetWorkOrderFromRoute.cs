using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From
{
    public class GetWorkOrderFromRoute
    {
        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
