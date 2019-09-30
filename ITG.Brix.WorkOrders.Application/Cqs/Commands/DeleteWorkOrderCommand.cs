using ITG.Brix.WorkOrders.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions
{
    public class DeleteWorkOrderCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }

        public DeleteWorkOrderCommand(Guid id, int version)
        {
            Id = id;
            Version = version;
        }
    }
}
