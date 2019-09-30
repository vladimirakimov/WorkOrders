using ITG.Brix.WorkOrders.API.Context.Bases;
using ITG.Brix.WorkOrders.API.Context.Constants;
using System.Net;

namespace ITG.Brix.WorkOrders.API.Context.Services.Responses.Mappers.Impl
{
    public class HttpStatusCodeMapper : IHttpStatusCodeMapper
    {
        public HttpStatusCode Map(ValidationResult validationResult)
        {
            switch (validationResult.ServiceError.Code)
            {
                case Consts.Failure.ServiceErrorCode.UnsupportedMediaType:
                    return HttpStatusCode.UnsupportedMediaType;
                case Consts.Failure.ServiceErrorCode.InvalidQueryParameterValue:
                case Consts.Failure.ServiceErrorCode.InvalidHeaderValue:
                case Consts.Failure.ServiceErrorCode.InvalidRequestBodyValue:
                case Consts.Failure.ServiceErrorCode.InvalidInput:
                case Consts.Failure.ServiceErrorCode.MissingRequiredQueryParameter:
                case Consts.Failure.ServiceErrorCode.MissingRequiredHeader:
                case Consts.Failure.ServiceErrorCode.MissingRequiredRequestBody:
                    return HttpStatusCode.BadRequest;
                case Consts.Failure.ServiceErrorCode.ResourceNotFound:
                    return HttpStatusCode.NotFound;
                case Consts.Failure.ServiceErrorCode.ResourceAlreadyExists:
                    return HttpStatusCode.Conflict;
                case Consts.Failure.ServiceErrorCode.ConditionNotMet:
                    return HttpStatusCode.PreconditionFailed;
                case Consts.Failure.ServiceErrorCode.BadGateway:
                    return HttpStatusCode.BadGateway;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
