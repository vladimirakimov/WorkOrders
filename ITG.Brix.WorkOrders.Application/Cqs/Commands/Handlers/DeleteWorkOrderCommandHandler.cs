using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Exceptions;
using ITG.Brix.WorkOrders.Application.Resources;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Handlers
{
    public class DeleteWorkOrderCommandHandler : IRequestHandler<DeleteWorkOrderCommand, Result>
    {
        private readonly ILogAs _logAs;
        private readonly IWorkOrderWriteRepository _workOrderWriteRepository;

        public DeleteWorkOrderCommandHandler(ILogAs logAs,
                                             IWorkOrderWriteRepository workOrderWriteRepository)
        {
            _logAs = logAs ?? throw Error.ArgumentNull(nameof(logAs));
            _workOrderWriteRepository = workOrderWriteRepository ?? throw Error.ArgumentNull(nameof(workOrderWriteRepository));
        }

        public async Task<Result> Handle(DeleteWorkOrderCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                await _workOrderWriteRepository.DeleteAsync(request.Id, request.Version);
                result = Result.Ok();
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "WorkOrder"),
                                            Target = "id"}
                                        }
                 );
            }
            catch (EntityVersionDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotMet.Name,
                                            Message = HandlerFailures.NotMet,
                                            Target = "version"}
                                        }
                );
            }
            catch (Exception ex)
            {
                _logAs.Error(CustomFailures.DeleteWorkOrderFailure, ex);
                result = Result.Fail(CustomFailures.DeleteWorkOrderFailure);
            }
            return result;
        }
    }
}
