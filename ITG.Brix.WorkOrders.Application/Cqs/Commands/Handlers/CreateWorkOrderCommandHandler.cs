using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Exceptions;
using ITG.Brix.WorkOrders.Application.Resources;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Handlers
{
    public class CreateWorkOrderCommandHandler : IRequestHandler<CreateWorkOrderCommand, Result>
    {
        private readonly ILogAs _logAs;
        private readonly IWorkOrderWriteRepository _workOrderWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateWorkOrderCommandHandler(ILogAs logAs,
                                             IWorkOrderWriteRepository workOrderWriteRepository,
                                             IIdentifierProvider identifierProvider,
                                             IVersionProvider versionProvider,
                                             IDateTimeProvider dateTimeProvider)
        {
            _logAs = logAs ?? throw Error.ArgumentNull(nameof(logAs));
            _workOrderWriteRepository = workOrderWriteRepository ?? throw new ArgumentNullException(nameof(workOrderWriteRepository));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<Result> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            //var order = new Order.Builder()
            //                     .WithSite(request.Order.Site)
            //                     .WithCustomer(request.Order.Customer)
            //                     .WithOperationalDepartment(request.Order.OperationalDepartment)
            //                     .WithLicensePlateTrailer(request.Order.LicensePlateTrailer)
            //                     .WithLicensePlateTruck(request.Order.LicensePlateTruck)
            //                     .WithContainer(request.Order.Container)
            //                     .WithContainerLocation(request.Order.ContainerLocation)
            //                     .WithDockingZone(request.Order.DockingZone)
            //                     .Build();


            Result result;

            try
            {
                var order = new Order()
                {
                    Origin = new Origin()
                    {
                        Source = "ECC"
                    },
                    Number = null,

                    Customer = new Customer()
                    {
                        Code = null,
                        ProductionSite = null,
                        Reference1 = null,
                        Reference2 = null,
                        Reference3 = null,
                        Reference4 = null,
                        Reference5 = null
                    },
                    Customs = new Customs()
                    {
                        CertificateOfOrigin = null,
                        Document = new Document()
                        {
                            Name = null,
                            Number = null,
                            Office = null,
                            Date = null
                        }
                    },
                    Transport = new Transport()
                    {
                        Kind = null,
                        Type = null,
                        Driver = new Driver()
                        {
                            Name = null,
                            Wait = Wait.Undefined
                        },
                        Delivery = new Delivery()
                        {
                            Place = null
                        },
                        Loading = new Loading()
                        {
                            Place = null,
                            Reference = null,
                        },
                        Truck = new Truck()
                        {
                            LicensePlateTruck = null,
                            LicensePlateTrailer = null,
                        },
                        Container = new Container()
                        {
                            Number = null,
                            Location = null,
                            StackLocation = null
                        },
                        Railcar = new Railcar()
                        {
                            Number = null
                        },
                        Ard = new Ard()
                        {
                            Reference1 = null,
                            Reference2 = null,
                            Reference3 = null,
                            Reference4 = null,
                            Reference5 = null,
                            Reference6 = null,
                            Reference7 = null,
                            Reference8 = null,
                            Reference9 = null,
                            Reference10 = null
                        },
                        Arrival = new Arrival()
                        {
                            Expected = null,
                            Arrived = null,
                            Latest = null
                        },
                        BillOfLading = new BillOfLading()
                        {
                            Number = null,
                            WeightNet = null,
                            WeightGross = null
                        },
                        Carrier = new Carrier()
                        {
                            Arrived = null,
                            Booked = null
                        },
                        Weighbridge = new Weighbridge()
                        {
                            Net = null,
                            Gross = null
                        },
                        Seal = new Seal()
                        {
                            Seal1 = null,
                            Seal2 = null,
                            Seal3 = null
                        },
                        Adr = null
                    },
                    Operation = new Operation()
                    {

                        Dispatch = new Dispatch()
                        {
                            Priority = null,
                            To = null,
                            Comment = null
                        },

                        Type = OperationType.Inbound,
                        Name = request.Operation,
                        UnitPlanning = null,
                        TypePlanning = null,
                        Site = request.Site,
                        Zone = null,
                        OperationalDepartment = request.Department,
                        DockingZone = null,
                        LoadingDock = null,
                        ProductOverview = null,
                        LotbatchOverview = null
                    }
                };
                var operational = new Operational(Status.Open);

                var workOrder = new WorkOrder.Builder()
                                              .WithId(_identifierProvider.Generate())
                                              .WithIsEditable(true)
                                              .WithCreatedOn(new CreatedOn(DateTime.UtcNow))
                                              .WithUserCreated(request.UserCreated)
                                              .WithOrder(order)
                                              .WithOperational(operational)
                                              .Build();


                workOrder.Version = _versionProvider.Generate();

                await _workOrderWriteRepository.CreateAsync(workOrder);
                result = Result.Ok(workOrder.Id, workOrder.Version);
            }
            catch (UniqueKeyDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault()
                                        {
                                            Code = HandlerFaultCode.Conflict.Name,
                                            Message = HandlerFailures.Conflict,
                                            Target = "workOrder"
                                        }
                });
            }
            catch (Exception ex)
            {
                _logAs.Error(CustomFailures.CreateWorkOrderFailure, ex);
                result = Result.Fail(CustomFailures.CreateWorkOrderFailure);
            }
            return result;
        }
    }
}
