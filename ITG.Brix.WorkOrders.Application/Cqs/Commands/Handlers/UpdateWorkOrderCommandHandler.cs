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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Handlers
{
    public class UpdateWorkOrderCommandHandler : IRequestHandler<UpdateWorkOrderCommand, Result>
    {
        private readonly ILogAs _logAs;
        private readonly IWorkOrderReadRepository _workOrderReadRepository;
        private readonly IWorkOrderWriteRepository _workOrderWriteRepository;
        private readonly IVersionProvider _versionProvider;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ITypeConverterProvider _typeConverterProvider;

        public UpdateWorkOrderCommandHandler(ILogAs logAs,
                                             IWorkOrderReadRepository workOrderReadRepository,
                                             IWorkOrderWriteRepository workOrderWriteRepository,
                                             IVersionProvider versionProvider,
                                             IDateTimeProvider dateTimeProvider,
                                             ITypeConverterProvider typeConverterProvider)
        {
            _logAs = logAs ?? throw Error.ArgumentNull(nameof(logAs));
            _workOrderReadRepository = workOrderReadRepository ?? throw Error.ArgumentNull(nameof(workOrderReadRepository));
            _workOrderWriteRepository = workOrderWriteRepository ?? throw Error.ArgumentNull(nameof(workOrderWriteRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
            _dateTimeProvider = dateTimeProvider ?? throw Error.ArgumentNull(nameof(dateTimeProvider));
            _typeConverterProvider = typeConverterProvider ?? throw Error.ArgumentNull(nameof(typeConverterProvider));
        }

        public async Task<Result> Handle(UpdateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var workOrderToUpdate = await _workOrderReadRepository.GetAsync(request.Id);

                if (workOrderToUpdate.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                if (request.Operant.HasValue)
                {
                    var operant = request.Operant.Value;
                    workOrderToUpdate.Operational.SetOperant(operant);
                }
                if (request.Status.HasValue)
                {
                    var status = Status.Parse(request.Status.Value);
                    workOrderToUpdate.Operational.ChangeStatus(status);
                }
                if (request.StartedOn.HasValue)
                {
                    var startedOnValue = _dateTimeProvider.Parse(request.StartedOn.Value);
                    if (startedOnValue.HasValue)
                    {
                        var startedOn = new DateOn(startedOnValue.Value);
                        workOrderToUpdate.Operational.SetStartedOn(startedOn);
                    }
                }
                if (request.HandledUnits.HasValue)
                {
                    workOrderToUpdate.Operational.ClearHandledUnits();
                    foreach (var handledUnitDto in request.HandledUnits.Value)
                    {
                        var id = new Guid(handledUnitDto.Id);
                        var operantId = new Guid(handledUnitDto.OperantId);
                        var operantLogin = new Login(handledUnitDto.OperantLogin);
                        var sourceUnitId = new Guid(handledUnitDto.SourceUnitId);
                        var locationWarehouse = handledUnitDto.Warehouse;
                        var locationGate = handledUnitDto.Gate;
                        var locationRow = handledUnitDto.Row;
                        var locationPosition = handledUnitDto.Position;
                        var units = new Units(int.Parse(handledUnitDto.Units));
                        var isPartial = bool.Parse(handledUnitDto.IsPartial);
                        var isMixed = bool.Parse(handledUnitDto.IsMixed);
                        var quantity = new Quantity(int.Parse(handledUnitDto.Quantity));
                        var weightNet = new Weight(float.Parse(handledUnitDto.WeightNet));
                        var weightGross = new Weight(float.Parse(handledUnitDto.WeightGross));
                        var palletNumber = handledUnitDto.PalletNumber;
                        var ssccNumber = handledUnitDto.SsccNumber;

                        var operant = new Operant(operantId, operantLogin);
                        var sourceUnit = workOrderToUpdate.Order.Units.First(x => x.Id == sourceUnitId);
                        var handledOn = new HandledOn(_dateTimeProvider.Parse(handledUnitDto.HandledOn).Value);
                        var location = new Location(
                                            new Warehouse(new Label(locationWarehouse)),
                                            new Gate(new Label(locationGate)),
                                            new Row(new Label(locationRow)),
                                            new Position(new Label(locationPosition))
                                       );
                        var type = sourceUnit.Type;


                        var goodDtos = handledUnitDto.Products;
                        var goods = new List<Good>();
                        foreach (var goodDto in goodDtos)
                        {
                            var goodId = new Guid(goodDto.Id);

                            var good = new Good(goodId);

                            good.SetConfiguration(
                                    new Configuration(
                                        code: goodDto.CongfigurationCode,
                                        description: goodDto.CongfigurationDescription,
                                        quantity: goodDto.CongfigurationQuantity,
                                        unitType: goodDto.CongfigurationUnitType,
                                        netPerUnit: goodDto.CongfigurationNetPerUnit,
                                        netPerUnitAlwaysDifferent: goodDto.CongfigurationNetPerUnitAlwaysDifferent,
                                        grossPerUnit: goodDto.CongfigurationGrossPerUnit
                                    )
                                );
                            good.SetCode(goodDto.Code);
                            good.SetCustomer(goodDto.Customer);
                            good.SetArrival(goodDto.Arrival);
                            good.SetArticle(goodDto.Article);
                            good.SetArticlePackagingCode(goodDto.ArticlePackagingCode);
                            good.SetName(goodDto.Code);
                            good.SetGtin(goodDto.Gtin);
                            good.SetProductType(goodDto.ProductType);
                            good.SetMaterialType(goodDto.MaterialType);
                            good.SetColor(goodDto.Color);
                            good.SetShape(goodDto.Shape);
                            good.SetLotbatch(goodDto.Lotbatch);
                            good.SetLotbatch2(goodDto.Lotbatch2);
                            good.SetClientReference(goodDto.ClientReference);
                            good.SetClientReference2(null);
                            good.SetBestBeforeDate(_dateTimeProvider.Parse(goodDto.BestBeforeDate).HasValue ? new DateOn(_dateTimeProvider.Parse(goodDto.BestBeforeDate).Value) : null);
                            good.SetDateFifo(_dateTimeProvider.Parse(goodDto.DateFifo).HasValue ? new DateOn(_dateTimeProvider.Parse(goodDto.DateFifo).Value) : null);
                            good.SetCustomsDocument(goodDto.CustomsDocument);
                            good.SetStorageStatus(goodDto.StorageStatus);
                            good.SetStackheight(goodDto.Stackheight);
                            good.SetLength(goodDto.Length);
                            good.SetWidth(goodDto.Width);
                            good.SetHeight(goodDto.Height);
                            good.SetOriginalContainer(goodDto.OriginalContainer);
                            good.SetQuantity(new Quantity(_typeConverterProvider.ToInt(goodDto.Quantity)));
                            good.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(goodDto.WeightNet)));
                            good.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(goodDto.WeightGross)));

                            goods.Add(good);
                        }

                        var handledUnit = new HandledUnit(id, sourceUnit);
                        handledUnit.SetOperant(operant);
                        handledUnit.SetHandledOn(handledOn);
                        handledUnit.SetLocation(location);
                        handledUnit.SetType(type);

                        handledUnit.SetUnits(units);
                        handledUnit.SetIsPartial(isPartial);
                        handledUnit.SetIsMixed(isMixed);
                        handledUnit.SetQuantity(quantity);
                        handledUnit.SetWeightNet(weightNet);
                        handledUnit.SetWeightGross(weightGross);

                        handledUnit.SetPalletNumber(palletNumber);
                        handledUnit.SetSsccNumber(ssccNumber);

                        foreach (var good in goods)
                        {
                            handledUnit.AddGood(good);
                        }

                        workOrderToUpdate.Operational.AddHandledUnit(handledUnit);
                    }
                }
                if (request.Remarks.HasValue)
                {
                    workOrderToUpdate.Operational.ClearRemarks();
                    foreach (var remarkDto in request.Remarks.Value)
                    {
                        var operantId = new Guid(remarkDto.OperantId);
                        var operantLogin = remarkDto.Operant;
                        var operant = new Operant(operantId, new Login(operantLogin));
                        var createdOn = new CreatedOn(_dateTimeProvider.Parse(remarkDto.CreatedOn).Value);
                        var text = remarkDto.Text;
                        var remark = new Remark(operant, createdOn, text);
                        workOrderToUpdate.Operational.AddRemark(remark);
                    }
                }
                if (request.Pictures.HasValue)
                {
                    workOrderToUpdate.Operational.ClearPictures();
                    foreach (var pictureDto in request.Pictures.Value)
                    {
                        var operantId = new Guid(pictureDto.OperantId);
                        var operantLogin = pictureDto.Operant;
                        var operant = new Operant(operantId, new Login(operantLogin));
                        var createdOn = new CreatedOn(_dateTimeProvider.Parse(pictureDto.CreatedOn).Value);
                        var name = pictureDto.Name;
                        var picture = new Picture(operant, createdOn, name);
                        workOrderToUpdate.Operational.AddPicture(picture);
                    }
                }
                if (request.Inputs.HasValue)
                {
                    workOrderToUpdate.Operational.ClearInputs();
                    foreach (var inputDto in request.Inputs.Value)
                    {
                        var operantId = new Guid(inputDto.OperantId);
                        var operantLogin = inputDto.Operant;
                        var operant = new Operant(operantId, new Login(operantLogin));
                        var createdOn = new CreatedOn(_dateTimeProvider.Parse(inputDto.CreatedOn).Value);
                        var value = inputDto.Value;
                        var property = inputDto.Property;
                        var input = new Input(operant, createdOn, value, property);
                        workOrderToUpdate.Operational.AddInput(input);
                    }
                }


                workOrderToUpdate.Version = _versionProvider.Generate();

                await _workOrderWriteRepository.UpdateAsync(workOrderToUpdate);
                result = Result.Ok(workOrderToUpdate.Version);
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
            catch (CommandVersionException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotMet.Name,
                                            Message = HandlerFailures.NotMet,
                                            Target = "version"}
                                        }
                );
            }
            catch (UniqueKeyDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.Conflict.Name,
                                            Message = HandlerFailures.Conflict,
                                            Target = "workOrder"}
                                        }
                );
            }
            catch (Exception ex)
            {
                _logAs.Error(CustomFailures.UpdateWorkOrderFailure, ex);
                result = Result.Fail(CustomFailures.UpdateWorkOrderFailure);
            }
            return result;
        }
    }
}
