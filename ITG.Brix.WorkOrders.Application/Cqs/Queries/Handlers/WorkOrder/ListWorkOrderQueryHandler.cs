using AutoMapper;
using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Models;
using ITG.Brix.WorkOrders.Application.Exceptions;
using ITG.Brix.WorkOrders.Application.Extensions;
using ITG.Brix.WorkOrders.Application.Resources;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Handlers
{
    public class ListWorkOrderQueryHandler : IRequestHandler<ListWorkOrderQuery, Result>
    {
        private readonly ILogAs _logAs;
        private readonly IMapper _mapper;
        private readonly IWorkOrderReadRepository _workOrderReadRepository;
        private readonly IFilterProvider _filterProvider;

        public ListWorkOrderQueryHandler(ILogAs logAs,
                                         IMapper mapper,
                                         IWorkOrderReadRepository workOrderReadRepository,
                                         IFilterProvider filterProvider)
        {
            _logAs = logAs ?? throw Error.ArgumentNull(nameof(logAs));
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _workOrderReadRepository = workOrderReadRepository ?? throw Error.ArgumentNull(nameof(workOrderReadRepository));
            _filterProvider = filterProvider ?? throw Error.ArgumentNull(nameof(filterProvider));
        }

        public async Task<Result> Handle(ListWorkOrderQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var filter = _filterProvider.Replace(request.Filter, Item.DictionaryOrderItemPath());
                int? skip = request.Skip.ToNullableInt();
                int? limit = request.Top.ToNullableInt();
                var workOrderDomains = await _workOrderReadRepository.ListAsync(filter, skip, limit);
                var workOrderModels = _mapper.Map<IEnumerable<WorkOrderModel>>(workOrderDomains);
                var count = workOrderModels.Count();
                var workOrdersModels = new WorkOrdersModel { Value = workOrderModels, Count = count, NextLink = null };

                result = Result.Ok(workOrdersModels);
            }
            catch (FilterODataException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.InvalidQueryFilter.Name,
                                            Message = HandlerFailures.InvalidQueryFilter,
                                            Target = "$filter"}
                                        }
                );
            }
            catch (Exception ex)
            {
                _logAs.Error(CustomFailures.ListWorkOrderFailure, ex);
                result = Result.Fail(CustomFailures.ListWorkOrderFailure);
            }

            return result;
        }
    }
}
