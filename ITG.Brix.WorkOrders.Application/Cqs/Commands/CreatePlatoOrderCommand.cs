using ITG.Brix.WorkOrders.Application.Bases;
using MediatR;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands
{
    public class CreatePlatoOrderCommand : IRequest<Result>
    {
        public string PlatoOrder { get; private set; }
        public CreatePlatoOrderCommand(string platoOrder)
        {
            PlatoOrder = platoOrder;
        }
    }
}
