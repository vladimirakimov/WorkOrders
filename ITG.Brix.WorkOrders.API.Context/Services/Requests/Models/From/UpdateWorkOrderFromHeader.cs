using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From
{
    public class UpdateWorkOrderFromHeader
    {
        [FromHeader(Name = "If-Match")]
        public string IfMatch { get; set; }

        [FromHeader(Name = "Content-Type")]
        public string ContentType { get; set; }
    }
}
