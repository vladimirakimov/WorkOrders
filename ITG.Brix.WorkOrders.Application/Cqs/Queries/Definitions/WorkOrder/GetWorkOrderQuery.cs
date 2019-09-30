using ITG.Brix.WorkOrders.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions
{
    public class GetWorkOrderQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetWorkOrderQuery(Guid id)
        {
            Id = id;
        }
    }
}
