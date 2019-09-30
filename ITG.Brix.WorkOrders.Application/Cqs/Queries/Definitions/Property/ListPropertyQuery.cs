using ITG.Brix.WorkOrders.Application.Bases;
using MediatR;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions
{
    public class ListPropertyQuery : IRequest<Result>
    {
        public string Filter { get; private set; }
        public string Top { get; private set; }
        public string Skip { get; private set; }

        public ListPropertyQuery(string filter, string top, string skip)
        {
            Filter = filter;
            Top = top;
            Skip = skip;
        }
    }
}
