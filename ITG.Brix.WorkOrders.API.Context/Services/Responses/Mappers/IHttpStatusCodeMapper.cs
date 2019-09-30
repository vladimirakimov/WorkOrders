using ITG.Brix.WorkOrders.API.Context.Bases;
using System.Net;

namespace ITG.Brix.WorkOrders.API.Context.Services.Responses.Mappers
{
    public interface IHttpStatusCodeMapper
    {
        HttpStatusCode Map(ValidationResult validationResult);
    }
}
