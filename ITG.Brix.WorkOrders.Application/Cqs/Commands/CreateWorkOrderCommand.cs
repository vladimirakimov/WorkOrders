using ITG.Brix.WorkOrders.Application.Bases;
using MediatR;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions
{
    public class CreateWorkOrderCommand : IRequest<Result>
    {
        public string UserCreated { get; private set; }
        public string Site { get; private set; }
        public string Operation { get; private set; }
        public string Department { get; private set; }

        public CreateWorkOrderCommand(string userCreated, string site, string operation, string department)
        {
            UserCreated = userCreated;
            Site = site;
            Operation = operation;
            Department = department;
        }
    }
}
