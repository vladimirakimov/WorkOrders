using AutoMapper;
using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Models;
using ITG.Brix.WorkOrders.Application.Exceptions;
using ITG.Brix.WorkOrders.Application.Resources;
using ITG.Brix.WorkOrders.Application.Services;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using ITG.Brix.WorkOrders.Infrastructure.Orchestrators;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Handlers
{
    public class GetWorkOrderQueryHandler : IRequestHandler<GetWorkOrderQuery, Result>
    {
        private readonly ILogAs _logAs;
        private readonly IMapper _mapper;
        private readonly IOrchestrator _orchestrator;
        private readonly IWorkOrderReadRepository _workOrderReadRepository;
        private readonly IWorkOrderWriteRepository _workOrderWriteRepository;
        private readonly IPlatoOrderProvider _platoOrderProvider;
        private readonly IDomainConverter _domainConverter;


        public GetWorkOrderQueryHandler(ILogAs logAs,
                                        IMapper mapper,
                                        IOrchestrator orchestrator,
                                        IWorkOrderReadRepository workOrderReadRepository,
                                        IWorkOrderWriteRepository workOrderWriteRepository,
                                        IPlatoOrderProvider platoOrderProvider,
                                        IDomainConverter domainConverter)
        {
            _logAs = logAs ?? throw Error.ArgumentNull(nameof(logAs));
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _orchestrator = orchestrator ?? throw Error.ArgumentNull(nameof(orchestrator));
            _workOrderReadRepository = workOrderReadRepository ?? throw Error.ArgumentNull(nameof(workOrderReadRepository));
            _workOrderWriteRepository = workOrderWriteRepository ?? throw Error.ArgumentNull(nameof(workOrderWriteRepository));
            _platoOrderProvider = platoOrderProvider ?? throw Error.ArgumentNull(nameof(platoOrderProvider));
            _domainConverter = domainConverter ?? throw Error.ArgumentNull(nameof(domainConverter));
        }

        public async Task<Result> Handle(GetWorkOrderQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var workorder = await _workOrderReadRepository.GetAsync(request.Id);
                if (!"ecc".Equals(workorder.Order.Origin.Source, StringComparison.InvariantCultureIgnoreCase))
                {
                    var jsonBody = new
                    {
                        source = workorder.Order.Origin.Source,
                        relationType = workorder.Order.Operation.Type.Name,
                        transportNo = workorder.Order.Origin.EntryNumber,
                        operationGroup = workorder.Order.Operation.Group,
                        operation = workorder.Order.Operation.Name
                    };
                    var jsonBodyAsString = JsonConvert.SerializeObject(jsonBody);
                    var jsonPlatoOrderFull = await _orchestrator.GetOrder(jsonBodyAsString);
                    var platoOrderFull = _platoOrderProvider.GetPlatoOrderFull(jsonPlatoOrderFull);
                    workorder.Order = _domainConverter.ToOrder(platoOrderFull.Transport);

                    // example 
                    //var jsonOrder = JsonConvert.SerializeObject(workorder.Order);

                    await _workOrderWriteRepository.UpdateAsync(workorder);
                }

                var workOrderModel = _mapper.Map<WorkOrderModel>(workorder);
                result = Result.Ok(workOrderModel, workorder.Version);

            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                            new HandlerFault()
                                            {
                                                Code = HandlerFaultCode.NotFound.Name,
                                                Message = string.Format(HandlerFailures.NotFound, "WorkOrder"),
                                                Target = "id"
                                            }
                                        }
                );
            }
            catch (Exception ex) when (ex is BiztalkCallException || ex is PlatoCallException)
            {
                _logAs.Error(ex);
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                            new HandlerFault()
                                            {
                                                Code = HandlerFaultCode.UpstreamAccessBiztalk.Name,
                                                Message = HandlerFailures.UpstreamAccessBiztalk,
                                                Target = "request"
                                            }
                                        }
                );
            }
            catch (Exception ex)
            {
                _logAs.Error(CustomFailures.GetWorkOrderFailure, ex);
                result = Result.Fail(CustomFailures.GetWorkOrderFailure);
            }
            return result;
        }
    }
}
