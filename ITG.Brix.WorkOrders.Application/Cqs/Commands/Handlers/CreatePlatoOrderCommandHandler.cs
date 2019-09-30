using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Exceptions;
using ITG.Brix.WorkOrders.Application.Resources;
using ITG.Brix.WorkOrders.Application.Services;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Handlers
{
    public class CreatePlatoOrderCommandHandler : IRequestHandler<CreatePlatoOrderCommand, Result>
    {
        private readonly ILogAs _logAs;
        private readonly IPlatoOrderWriteRepository _platoOrderWriteRepository;
        private readonly IWorkOrderWriteRepository _workOrderWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;
        private readonly IPlatoOrderProvider _platoOrderProvider;
        private readonly IPlatoOrderChecker _platoOrderChecker;
        private readonly IDomainConverter _domainConverter;


        public CreatePlatoOrderCommandHandler(ILogAs logAs,
                                              IPlatoOrderWriteRepository platoOrderWriteRepository,
                                              IWorkOrderWriteRepository workOrderWriteRepository,
                                              IIdentifierProvider identifierProvider,
                                              IVersionProvider versionProvider,
                                              IPlatoOrderProvider platoOrderProvider,
                                              IPlatoOrderChecker platoOrderChecker,
                                              IDomainConverter domainConverter)
        {
            _logAs = logAs ?? throw Error.ArgumentNull(nameof(logAs));
            _platoOrderWriteRepository = platoOrderWriteRepository ?? throw Error.ArgumentNull(nameof(platoOrderWriteRepository));
            _workOrderWriteRepository = workOrderWriteRepository ?? throw Error.ArgumentNull(nameof(workOrderWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
            _platoOrderProvider = platoOrderProvider ?? throw Error.ArgumentNull(nameof(platoOrderProvider));
            _platoOrderChecker = platoOrderChecker ?? throw Error.ArgumentNull(nameof(platoOrderChecker));
            _domainConverter = domainConverter ?? throw Error.ArgumentNull(nameof(domainConverter));
        }

        public async Task<Result> Handle(CreatePlatoOrderCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var platoOrderId = _identifierProvider.Generate();
                await _platoOrderWriteRepository.CreateAsync(new PlatoOrder(platoOrderId, request.PlatoOrder));


                var platoOrderOverview = _platoOrderProvider.GetPlatoOrderOverview(request.PlatoOrder);

                //_platoOrderChecker.Check(platoOrderOverview);

                var order = _domainConverter.ToOrder(platoOrderOverview);

                Operational operational = new Operational(Status.Open);

                var workOrder = new WorkOrder.Builder()
                                              .WithId(Guid.NewGuid())
                                              .WithIsEditable(false)
                                              .WithCreatedOn(new CreatedOn(DateTime.UtcNow))
                                              .WithUserCreated("Plato")
                                              .WithOrder(order)
                                              .WithOperational(operational)
                                              .Build();

                workOrder.Version = _versionProvider.Generate();
                await _workOrderWriteRepository.CreateAsync(workOrder);

                result = Result.Ok(workOrder.Id, workOrder.Version);
            }
            catch (PlatoOrderOverviewCheckException ex)
            {
                _logAs.Error(CustomFailures.CreatePlatoOrderFailure, ex);
                result = Result.Fail(CustomFailures.CreatePlatoOrderFailure);
            }
            catch (Exception ex)
            {
                _logAs.Error(CustomFailures.CreatePlatoOrderFailure, ex);
                result = Result.Fail(CustomFailures.CreatePlatoOrderFailure);
            }
            return result;
        }
    }
}
