using ITG.Brix.WorkOrders.API.Context.Bases;
using ITG.Brix.WorkOrders.Application.Bases;
using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.WorkOrders.API.Context.Services.Responses
{
    public interface IApiResponse
    {
        IActionResult Fail(ValidationResult validationResult);
        IActionResult Fail(Result result);
        IActionResult Ok(object value);
        IActionResult Ok(object value, string eTagValue);
        IActionResult Created(string uri, string eTagValue);
        IActionResult Updated(string eTagValue);
        IActionResult Deleted();
        IActionResult Error();
    }
}
