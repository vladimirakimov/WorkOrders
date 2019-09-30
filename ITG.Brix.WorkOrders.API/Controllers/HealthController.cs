using Microsoft.AspNetCore.Mvc;
using System.Fabric;

namespace ITG.Brix.WorkOrders.API.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly StatelessServiceContext context;

        public HealthController(StatelessServiceContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Check()
        {
            return Ok(new
            {
                service = context.ServiceName,
                version = context.CodePackageActivationContext.CodePackageVersion,
                status = "Alive and kicking"
            });
        }
    }
}
