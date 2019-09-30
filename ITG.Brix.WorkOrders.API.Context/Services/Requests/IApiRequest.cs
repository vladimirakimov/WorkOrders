using ITG.Brix.WorkOrders.API.Context.Bases;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests
{
    /// <summary>
    /// Request validation strategy.
    /// </summary>
    public interface IApiRequest
    {
        ValidationResult Validate<T>(T request);
    }
}
