using ITG.Brix.WorkOrders.API.Context.Bases;
using ITG.Brix.WorkOrders.API.Context.Services.Responses.Models.Errors;

namespace ITG.Brix.WorkOrders.API.Context.Services.Responses.Mappers
{
    public interface IErrorMapper
    {
        ResponseError Map(ValidationResult validationResult);
    }
}
