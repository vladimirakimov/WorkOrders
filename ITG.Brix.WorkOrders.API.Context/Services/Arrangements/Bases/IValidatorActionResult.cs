using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.WorkOrders.API.Context.Services.Arrangements.Bases
{
    public interface IValidatorActionResult
    {
        IActionResult Result { get; }
    }
}
