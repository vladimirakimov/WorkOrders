using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From
{
    public class DeleteWorkOrderFromHeader
    {
        [FromHeader(Name = "If-Match")]
        public string IfMatch { get; set; }
    }
}
