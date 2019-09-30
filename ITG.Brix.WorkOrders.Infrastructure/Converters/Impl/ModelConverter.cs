using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Infrastructure.Converters.Impl
{
    public class ModelConverter : IModelConverter
    {
        private readonly ITypeConverterProvider _typeConverterProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ModelConverter(ITypeConverterProvider typeConverterProvider, IDateTimeProvider dateTimeProvider)
        {
            _typeConverterProvider = typeConverterProvider ?? throw Error.ArgumentNull(nameof(typeConverterProvider));
            _dateTimeProvider = dateTimeProvider ?? throw Error.ArgumentNull(nameof(dateTimeProvider));
        }

        public WorkOrderClass ToClass(WorkOrder workOrder)
        {
            #region Units
            var unitClasses = new List<UnitClass>();
            foreach (var unit in workOrder.Order.Units)
            {
                var productClasses = new List<ProductClass>();
                foreach (var product in unit.Products)
                {
                    var remarks = new List<string>();
                    foreach (var remarkComment in product.Remarks)
                    {
                        remarks.Add(remarkComment);
                    }

                    var safetyRemarks = new List<string>();
                    foreach (var safetyRemarkComment in product.SafetyRemarks)
                    {
                        safetyRemarks.Add(safetyRemarkComment);
                    }

                    var notes = new List<string>();
                    foreach (var noteComment in product.Notes)
                    {
                        notes.Add(noteComment);
                    }

                    var productClass = new ProductClass()
                    {
                        Id = product.Id,

                        ConfigurationCode = product.Configuration.Code,
                        ConfigurationDescription = product.Configuration.Description,
                        ConfigurationQuantity = product.Configuration.Quantity,
                        ConfigurationUnitType = product.Configuration.UnitType,
                        ConfigurationNetPerUnit = product.Configuration.NetPerUnit,
                        ConfigurationNetPerUnitAlwaysDifferent = product.Configuration.NetPerUnitAlwaysDifferent,
                        ConfigurationGrossPerUnit = product.Configuration.GrossPerUnit,

                        Remarks = remarks,
                        SafetyRemarks = safetyRemarks,
                        Notes = notes,

                        Code = product.Code,
                        Customer = product.Customer,
                        Arrival = product.Arrival,
                        Article = product.Article,
                        ArticlePackagingCode = product.ArticlePackagingCode,
                        Name = product.Name,
                        Gtin = product.Gtin,
                        ProductType = product.ProductType,
                        MaterialType = product.MaterialType,
                        Color = product.Color,
                        Shape = product.Shape,
                        Lotbatch = product.Lotbatch,
                        Lotbatch2 = product.Lotbatch2,
                        ClientReference = product.ClientReference,
                        ClientReference2 = product.ClientReference2,
                        BestBeforeDate = product.BestBeforeDate,
                        DateFifo = product.DateFifo,
                        CustomsDocument = product.CustomsDocument,
                        StorageStatus = product.StorageStatus,
                        Stackheight = product.Stackheight,
                        Length = product.Length,
                        Width = product.Width,
                        Height = product.Height,
                        OriginalContainer = product.OriginalContainer,
                        Quantity = product.Quantity,
                        WeightNet = product.WeightNet,
                        WeightGross = product.WeightGross
                    };
                    productClasses.Add(productClass);
                }


                var unitClass = new UnitClass()
                {
                    Id = unit.Id,

                    LocationWarehouse = unit.Location.Warehouse,
                    LocationGate = unit.Location.Gate,
                    LocationRow = unit.Location.Row,
                    LocationPosition = unit.Location.Position,

                    GroupKey = unit.Group.Key,
                    GroupWeightNet = unit.Group.WeightNet,
                    GroupWeightGross = unit.Group.WeightGross,

                    MixedKey = unit.Mixed.Key,
                    MixedPalletNumber = unit.Mixed.PalletNumber,

                    Units = unit.Units,
                    UnitType = unit.Type.Name,
                    Quantity = unit.Quantity,

                    WeightNet = unit.WeightNet,
                    WeightGross = unit.WeightGross,

                    IsPartial = unit.IsPartial,
                    PalletNumber = unit.PalletNumber,
                    SsccNumber = unit.SsccNumber,

                    Products = productClasses
                };
                unitClasses.Add(unitClass);
            }

            #endregion

            var extraActivityClasses = new List<ExtraActivityClass>();
            foreach (var product in workOrder.Order.Operation.ExtraActivities)
            {
                var extraActivitiyClass = new ExtraActivityClass()
                {
                    Code = product.Code,
                    Activity = product.Activity,
                    Description = product.Description,
                    IsExecuted = product.IsExecuted,
                    Quantity = product.Quantity,
                    Remark = product.Remark,
                };
                extraActivityClasses.Add(extraActivitiyClass);
            }

            var operationalRemarks = new List<string>();
            foreach (var operationalRemark in workOrder.Order.Operation.OperationalRemarks)
            {
                operationalRemarks.Add(operationalRemark);
            }

            var handledUnitClasses = new List<HandledUnitClass>();
            foreach (var handledUnit in workOrder.Operational.HandledUnits)
            {
                var goodClasses = new List<GoodClass>();
                foreach (var good in handledUnit.Goods)
                {
                    var goodClass = new GoodClass()
                    {
                        Id = good.Id,

                        ConfigurationCode = good.Configuration.Code,
                        ConfigurationDescription = good.Configuration.Description,
                        ConfigurationQuantity = good.Configuration.Quantity,
                        ConfigurationUnitType = good.Configuration.UnitType,
                        ConfigurationNetPerUnit = good.Configuration.NetPerUnit,
                        ConfigurationNetPerUnitAlwaysDifferent = good.Configuration.NetPerUnitAlwaysDifferent,
                        ConfigurationGrossPerUnit = good.Configuration.GrossPerUnit,

                        Code = good.Code,
                        Customer = good.Customer,
                        Arrival = good.Arrival,
                        Article = good.Article,
                        ArticlePackagingCode = good.ArticlePackagingCode,
                        Name = good.Name,
                        Gtin = good.Gtin,
                        ProductType = good.ProductType,
                        MaterialType = good.MaterialType,
                        Color = good.Color,
                        Shape = good.Shape,
                        Lotbatch = good.Lotbatch,
                        Lotbatch2 = good.Lotbatch2,
                        ClientReference = good.ClientReference,
                        ClientReference2 = good.ClientReference2,
                        BestBeforeDate = good.BestBeforeDate,
                        DateFifo = good.DateFifo,
                        CustomsDocument = good.CustomsDocument,
                        StorageStatus = good.StorageStatus,
                        Stackheight = good.Stackheight,
                        Length = good.Length,
                        Width = good.Width,
                        Height = good.Height,
                        OriginalContainer = good.OriginalContainer,
                        Quantity = good.Quantity,
                        WeightNet = good.WeightNet,
                        WeightGross = good.WeightGross
                    };
                    goodClasses.Add(goodClass);
                }

                var handledUnitClass = new HandledUnitClass()
                {
                    Id = handledUnit.Id,

                    SourceUnitId = handledUnit.SourceUnit.Id,

                    OperantId = handledUnit.Operant.Id,
                    OperantLogin = handledUnit.Operant.Login,

                    HandledOn = handledUnit.HandledOn,

                    LocationWarehouse = handledUnit.Location.Warehouse,
                    LocationGate = handledUnit.Location.Gate,
                    LocationRow = handledUnit.Location.Row,
                    LocationPosition = handledUnit.Location.Position,

                    Units = handledUnit.Units,
                    UnitType = handledUnit.Type.Name,
                    Quantity = handledUnit.Quantity,

                    WeightNet = handledUnit.WeightNet,
                    WeightGross = handledUnit.WeightGross,

                    IsPartial = handledUnit.IsPartial,
                    IsMixed = handledUnit.IsMixed,
                    PalletNumber = handledUnit.PalletNumber,
                    SsccNumber = handledUnit.SsccNumber,

                    Products = goodClasses
                };


                handledUnitClasses.Add(handledUnitClass);
            }

            var remarkClasses = new List<RemarkClass>();
            foreach (var remark in workOrder.Operational.Remarks)
            {
                var remarkClass = new RemarkClass()
                {
                    OperantId = remark.Operant.Id,
                    OperantLogin = remark.Operant.Login,
                    CreatedOn = remark.CreatedOn,
                    Text = remark.Text
                };
                remarkClasses.Add(remarkClass);
            }

            var pictureClasses = new List<PictureClass>();
            foreach (var picture in workOrder.Operational.Pictures)
            {
                var pictureClass = new PictureClass()
                {
                    OperantId = picture.Operant.Id,
                    OperantLogin = picture.Operant.Login,
                    CreatedOn = picture.CreatedOn,
                    Name = picture.Name
                };
                pictureClasses.Add(pictureClass);
            }

            var inputClasses = new List<InputClass>();
            foreach (var input in workOrder.Operational.Inputs)
            {
                var inputClass = new InputClass()
                {
                    OperantId = input.Operant.Id,
                    OperantLogin = input.Operant.Login,
                    CreatedOn = input.CreatedOn,
                    Property = input.Property,
                    Value = input.Value
                };
                inputClasses.Add(inputClass);
            }

            var workOrderClass = new WorkOrderClass()
            {
                Id = workOrder.Id,
                Version = workOrder.Version,
                UserCreated = workOrder.UserCreated,
                CreatedOn = workOrder.CreatedOn,

                OrderNumber = workOrder.Order.Number,

                OrderOriginSource = workOrder.Order.Origin.Source,
                OrderOriginEntryNumber = workOrder.Order.Origin.EntryNumber,

                OrderCustomerCode = workOrder.Order.Customer.Code,
                OrderCustomerProductionSite = workOrder.Order.Customer.ProductionSite,
                OrderCustomerReference1 = workOrder.Order.Customer.Reference1,
                OrderCustomerReference2 = workOrder.Order.Customer.Reference2,
                OrderCustomerReference3 = workOrder.Order.Customer.Reference3,
                OrderCustomerReference4 = workOrder.Order.Customer.Reference4,
                OrderCustomerReference5 = workOrder.Order.Customer.Reference5,

                OrderCustomsCertificateOfOrigin = workOrder.Order.Customs.CertificateOfOrigin,
                OrderCustomsDocumentName = workOrder.Order.Customs.Document.Name,
                OrderCustomsDocumentNumber = workOrder.Order.Customs.Document.Number,
                OrderCustomsDocumentOffice = workOrder.Order.Customs.Document.Office,
                OrderCustomsDocumentDate = workOrder.Order.Customs.Document.Date,


                OrderTransportKind = workOrder.Order.Transport.Kind,
                OrderTransportType = workOrder.Order.Transport.Type,
                OrderTransportDriverName = workOrder.Order.Transport.Driver.Name,
                OrderTransportDriverWait = workOrder.Order.Transport.Driver.Wait.ToString(),
                OrderTransportDeliveryEta = workOrder.Order.Transport.Delivery.Eta,
                OrderTransportDeliveryLta = workOrder.Order.Transport.Delivery.Lta,
                OrderTransportDeliveryPlace = workOrder.Order.Transport.Delivery.Place,
                OrderTransportDeliveryReference = workOrder.Order.Transport.Delivery.Reference,
                OrderTransportLoadingEta = workOrder.Order.Transport.Loading.Eta,
                OrderTransportLoadingLta = workOrder.Order.Transport.Loading.Lta,
                OrderTransportLoadingPlace = workOrder.Order.Transport.Loading.Place,
                OrderTransportLoadingReference = workOrder.Order.Transport.Loading.Reference,
                OrderTransportTruckLicensePlateTruck = workOrder.Order.Transport.Truck.LicensePlateTruck,
                OrderTransportTruckLicensePlateTrailer = workOrder.Order.Transport.Truck.LicensePlateTrailer,
                OrderTransportContainerFreeUntilOnTerminalShippingLine = workOrder.Order.Transport.Container.FreeUntilOnTerminal.ShippingLine,
                OrderTransportContainerFreeUntilOnTerminalCustomer = workOrder.Order.Transport.Container.FreeUntilOnTerminal.Customer,
                OrderTransportContainerNumber = workOrder.Order.Transport.Container.Number,
                OrderTransportContainerLocation = workOrder.Order.Transport.Container.Location,
                OrderTransportContainerStackLocation = workOrder.Order.Transport.Container.StackLocation,
                OrderTransportRailcarNumber = workOrder.Order.Transport.Railcar.Number,
                OrderTransportArdReference1 = workOrder.Order.Transport.Ard.Reference1,
                OrderTransportArdReference2 = workOrder.Order.Transport.Ard.Reference2,
                OrderTransportArdReference3 = workOrder.Order.Transport.Ard.Reference3,
                OrderTransportArdReference4 = workOrder.Order.Transport.Ard.Reference4,
                OrderTransportArdReference5 = workOrder.Order.Transport.Ard.Reference5,
                OrderTransportArdReference6 = workOrder.Order.Transport.Ard.Reference6,
                OrderTransportArdReference7 = workOrder.Order.Transport.Ard.Reference7,
                OrderTransportArdReference8 = workOrder.Order.Transport.Ard.Reference8,
                OrderTransportArdReference9 = workOrder.Order.Transport.Ard.Reference9,
                OrderTransportArdReference10 = workOrder.Order.Transport.Ard.Reference10,
                OrderTransportArrivalExpected = workOrder.Order.Transport.Arrival.Expected,
                OrderTransportArrivalLatest = workOrder.Order.Transport.Arrival.Latest,
                OrderTransportArrivalArrived = workOrder.Order.Transport.Arrival.Arrived,
                OrderTransportBillOfLadingNumber = workOrder.Order.Transport.BillOfLading.Number,
                OrderTransportBillOfLadingWeightNet = workOrder.Order.Transport.BillOfLading.WeightNet,
                OrderTransportBillOfLadingWeightGross = workOrder.Order.Transport.BillOfLading.WeightGross,
                OrderTransportCarrierBooked = workOrder.Order.Transport.Carrier.Booked,
                OrderTransportCarrierArrived = workOrder.Order.Transport.Carrier.Arrived,
                OrderTransportWeighbridgeNet = workOrder.Order.Transport.Weighbridge.Net,
                OrderTransportWeighbridgeGross = workOrder.Order.Transport.Weighbridge.Gross,
                OrderTransportSealSeal1 = workOrder.Order.Transport.Seal.Seal1,
                OrderTransportSealSeal2 = workOrder.Order.Transport.Seal.Seal2,
                OrderTransportSealSeal3 = workOrder.Order.Transport.Seal.Seal3,
                OrderTransportAdr = workOrder.Order.Transport.Adr,

                OrderUnits = unitClasses,

                OrderOperationPriorityCode = workOrder.Order.Operation.Priority.Code,
                OrderOperationPriorityValue = workOrder.Order.Operation.Priority.Value,

                OrderOperationDispatchPriority = workOrder.Order.Operation.Dispatch.Priority,
                OrderOperationDispatchTo = workOrder.Order.Operation.Dispatch.To,
                OrderOperationDispatchComment = workOrder.Order.Operation.Dispatch.Comment,

                OrderOperationExtraActivities = extraActivityClasses,

                OrderOperationType = workOrder.Order.Operation.Type.Name,
                OrderOperationName = workOrder.Order.Operation.Name,
                OrderOperationGroup = workOrder.Order.Operation.Group,
                OrderOperationUnitPlanning = workOrder.Order.Operation.UnitPlanning,
                OrderOperationTypePlanning = workOrder.Order.Operation.TypePlanning,
                OrderOperationSite = workOrder.Order.Operation.Site,
                OrderOperationZone = workOrder.Order.Operation.Zone,
                OrderOperationOperationalDepartment = workOrder.Order.Operation.OperationalDepartment,
                OrderOperationOperationalRemarks = operationalRemarks,
                OrderOperationDockingZone = workOrder.Order.Operation.DockingZone,
                OrderOperationLoadingDock = workOrder.Order.Operation.LoadingDock,
                OrderOperationProductOverview = workOrder.Order.Operation.ProductOverview,
                OrderOperationLotbatchOverview = workOrder.Order.Operation.LotbatchOverview,

                OperationalOperant = workOrder.Operational.Operant,
                OperationalStatus = workOrder.Operational.Status.Name,
                OperationalUnits = handledUnitClasses,
                OperationalRemarks = remarkClasses,
                OperationalPictures = pictureClasses,
                OperationalStartedOn = workOrder.Operational.StartedOn,
                OperationalInputs = inputClasses
            };

            return workOrderClass;
        }

        public WorkOrder ToDomain(WorkOrderClass workOrderClass)
        {
            var workOrder = new WorkOrder(workOrderClass.Id)
            {
                UserCreated = workOrderClass.UserCreated,
                CreatedOn = new CreatedOn(workOrderClass.CreatedOn),
                Version = workOrderClass.Version,

                Order = new Order()
                {
                    Number = workOrderClass.OrderNumber,

                    Origin = new Origin()
                    {
                        Source = workOrderClass.OrderOriginSource,
                        EntryNumber = workOrderClass.OrderOriginEntryNumber
                    },

                    Customer = new Customer()
                    {
                        Code = workOrderClass.OrderCustomerCode,
                        ProductionSite = workOrderClass.OrderCustomerProductionSite,
                        Reference1 = workOrderClass.OrderCustomerReference1,
                        Reference2 = workOrderClass.OrderCustomerReference2,
                        Reference3 = workOrderClass.OrderCustomerReference3,
                        Reference4 = workOrderClass.OrderCustomerReference4,
                        Reference5 = workOrderClass.OrderCustomerReference5
                    },

                    Customs = new Customs()
                    {
                        CertificateOfOrigin = workOrderClass.OrderCustomsCertificateOfOrigin,
                        Document = new Document()
                        {
                            Name = workOrderClass.OrderCustomsDocumentName,
                            Number = workOrderClass.OrderCustomsDocumentNumber,
                            Office = workOrderClass.OrderCustomsDocumentOffice,
                            Date = workOrderClass.OrderCustomsDocumentDate.HasValue ? new DateOn(workOrderClass.OrderCustomsDocumentDate.Value) : null,
                        }
                    },

                    Transport = new Transport()
                    {
                        Kind = workOrderClass.OrderTransportKind,
                        Type = workOrderClass.OrderTransportType,
                        Driver = new Driver()
                        {
                            Name = workOrderClass.OrderTransportDriverName,
                            Wait = (Wait)Enum.Parse(typeof(Wait), workOrderClass.OrderTransportDriverWait),
                        },
                        Delivery = new Delivery()
                        {
                            Eta = workOrderClass.OrderTransportDeliveryEta.HasValue ? new DateOn(workOrderClass.OrderTransportDeliveryEta.Value) : null,
                            Lta = workOrderClass.OrderTransportDeliveryLta.HasValue ? new DateOn(workOrderClass.OrderTransportDeliveryLta.Value) : null,
                            Place = workOrderClass.OrderTransportDeliveryPlace,
                            Reference = workOrderClass.OrderTransportDeliveryReference
                        },
                        Loading = new Loading()
                        {
                            Eta = workOrderClass.OrderTransportLoadingEta.HasValue ? new DateOn(workOrderClass.OrderTransportLoadingEta.Value) : null,
                            Lta = workOrderClass.OrderTransportLoadingLta.HasValue ? new DateOn(workOrderClass.OrderTransportLoadingLta.Value) : null,
                            Place = workOrderClass.OrderTransportLoadingPlace,
                            Reference = workOrderClass.OrderTransportLoadingReference
                        },
                        Truck = new Truck()
                        {
                            LicensePlateTruck = workOrderClass.OrderTransportTruckLicensePlateTruck,
                            LicensePlateTrailer = workOrderClass.OrderTransportTruckLicensePlateTrailer
                        },
                        Container = new Container()
                        {
                            FreeUntilOnTerminal = new FreeUntilOnTerminal()
                            {
                                ShippingLine = workOrderClass.OrderTransportContainerFreeUntilOnTerminalShippingLine,
                                Customer = workOrderClass.OrderTransportContainerFreeUntilOnTerminalCustomer
                            },
                            Number = workOrderClass.OrderTransportContainerNumber,
                            Location = workOrderClass.OrderTransportContainerLocation,
                            StackLocation = workOrderClass.OrderTransportContainerStackLocation

                        },
                        Railcar = new Railcar()
                        {
                            Number = workOrderClass.OrderTransportRailcarNumber
                        },
                        Ard = new Ard()
                        {
                            Reference1 = workOrderClass.OrderTransportArdReference1,
                            Reference2 = workOrderClass.OrderTransportArdReference2,
                            Reference3 = workOrderClass.OrderTransportArdReference3,
                            Reference4 = workOrderClass.OrderTransportArdReference4,
                            Reference5 = workOrderClass.OrderTransportArdReference5,
                            Reference6 = workOrderClass.OrderTransportArdReference6,
                            Reference7 = workOrderClass.OrderTransportArdReference7,
                            Reference8 = workOrderClass.OrderTransportArdReference8,
                            Reference9 = workOrderClass.OrderTransportArdReference9,
                            Reference10 = workOrderClass.OrderTransportArdReference10
                        },
                        Arrival = new Arrival()
                        {
                            Expected = workOrderClass.OrderTransportArrivalExpected.HasValue ? new DateOn(workOrderClass.OrderTransportArrivalExpected.Value) : null,
                            Latest = workOrderClass.OrderTransportArrivalLatest.HasValue ? new DateOn(workOrderClass.OrderTransportArrivalLatest.Value) : null,
                            Arrived = workOrderClass.OrderTransportArrivalArrived.HasValue ? new DateOn(workOrderClass.OrderTransportArrivalArrived.Value) : null,
                        },
                        BillOfLading = new BillOfLading()
                        {
                            Number = workOrderClass.OrderTransportBillOfLadingNumber,
                            WeightNet = workOrderClass.OrderTransportBillOfLadingWeightNet,
                            WeightGross = workOrderClass.OrderTransportBillOfLadingWeightGross
                        },
                        Carrier = new Carrier()
                        {
                            Booked = workOrderClass.OrderTransportCarrierBooked,
                            Arrived = workOrderClass.OrderTransportCarrierArrived
                        },
                        Weighbridge = new Weighbridge()
                        {
                            Net = workOrderClass.OrderTransportWeighbridgeNet,
                            Gross = workOrderClass.OrderTransportWeighbridgeGross
                        },
                        Seal = new Seal()
                        {
                            Seal1 = workOrderClass.OrderTransportSealSeal1,
                            Seal2 = workOrderClass.OrderTransportSealSeal2,
                            Seal3 = workOrderClass.OrderTransportSealSeal3
                        },
                        Adr = workOrderClass.OrderTransportAdr
                    },

                    Operation = new Operation()
                    {
                        Priority = new Priority()
                        {
                            Code = workOrderClass.OrderOperationPriorityCode,
                            Value = workOrderClass.OrderOperationPriorityValue
                        },
                        Dispatch = new Dispatch()
                        {
                            Priority = workOrderClass.OrderOperationDispatchPriority,
                            To = workOrderClass.OrderOperationDispatchTo,
                            Comment = workOrderClass.OrderOperationDispatchComment
                        },

                        Type = OperationType.Parse(workOrderClass.OrderOperationType),
                        Name = workOrderClass.OrderOperationName,
                        Group = workOrderClass.OrderOperationGroup,
                        UnitPlanning = workOrderClass.OrderOperationUnitPlanning,
                        TypePlanning = workOrderClass.OrderOperationTypePlanning,
                        Site = workOrderClass.OrderOperationSite,
                        Zone = workOrderClass.OrderOperationZone,
                        OperationalDepartment = workOrderClass.OrderOperationOperationalDepartment,
                        DockingZone = workOrderClass.OrderOperationDockingZone,
                        LoadingDock = workOrderClass.OrderOperationLoadingDock,
                        ProductOverview = workOrderClass.OrderOperationProductOverview,
                        LotbatchOverview = workOrderClass.OrderOperationLotbatchOverview
                    },
                }
            };

            #region Units

            var units = new List<Unit>();

            foreach (var unitClass in workOrderClass.OrderUnits)
            {
                var products = new List<Product>();

                foreach (var productClass in unitClass.Products)
                {
                    var productRemarks = new List<string>();
                    foreach (var remarkComment in productClass.Remarks)
                    {
                        productRemarks.Add(remarkComment);
                    }

                    var safetyRemarks = new List<string>();
                    foreach (var safetyRemarkComment in productClass.SafetyRemarks)
                    {
                        safetyRemarks.Add(safetyRemarkComment);
                    }

                    var notes = new List<string>();
                    foreach (var noteComment in productClass.Notes)
                    {
                        notes.Add(noteComment);
                    }

                    var product = new Product(productClass.Id)
                    {
                        Remarks = productRemarks,
                        SafetyRemarks = safetyRemarks,
                        Notes = notes
                    };
                    product.SetConfiguration(
                        new Configuration(
                            code: productClass.ConfigurationCode,
                            description: productClass.ConfigurationDescription,
                            quantity: productClass.ConfigurationQuantity,
                            unitType: productClass.ConfigurationUnitType,
                            netPerUnit: productClass.ConfigurationNetPerUnit,
                            netPerUnitAlwaysDifferent: productClass.ConfigurationNetPerUnitAlwaysDifferent,
                            grossPerUnit: productClass.ConfigurationGrossPerUnit
                        )
                    );
                    product.SetCode(productClass.Code);
                    product.SetCustomer(productClass.Customer);
                    product.SetArrival(productClass.Arrival);
                    product.SetArticle(productClass.Article);
                    product.SetArticlePackagingCode(productClass.ArticlePackagingCode);
                    product.SetName(productClass.Name);
                    product.SetGtin(productClass.Gtin);
                    product.SetProductType(productClass.ProductType);
                    product.SetMaterialType(productClass.MaterialType);
                    product.SetColor(productClass.Color);
                    product.SetShape(productClass.Shape);
                    product.SetLotbatch(productClass.Lotbatch);
                    product.SetLotbatch2(productClass.Lotbatch2);
                    product.SetClientReference(productClass.ClientReference);
                    product.SetClientReference2(productClass.ClientReference2);
                    product.SetBestBeforeDate(productClass.BestBeforeDate.HasValue ? new DateOn(productClass.BestBeforeDate.Value) : null);
                    product.SetDateFifo(productClass.DateFifo.HasValue ? new DateOn(productClass.DateFifo.Value) : null);
                    product.SetCustomsDocument(productClass.CustomsDocument);
                    product.SetStorageStatus(productClass.StorageStatus);
                    product.SetStackheight(productClass.Stackheight);
                    product.SetLength(productClass.Length);
                    product.SetWidth(productClass.Width);
                    product.SetHeight(productClass.Height);
                    product.SetOriginalContainer(productClass.OriginalContainer);
                    product.SetQuantity(new Quantity(productClass.Quantity));
                    product.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(productClass.WeightNet)));
                    product.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(productClass.WeightGross)));
                    products.Add(product);
                }


                var unit = new Unit(unitClass.Id, UnitType.Parse(unitClass.UnitType));
                var location = new Location(
                    new Warehouse(new Label(unitClass.LocationWarehouse)),
                    new Gate(new Label(unitClass.LocationGate)),
                    new Row(new Label(unitClass.LocationRow)),
                    new Position(new Label(unitClass.LocationPosition))
                );
                unit.SetLocation(location);
                unit.SetGroup(new Group(
                                key: unitClass.GroupKey,
                                weightNet: unitClass.GroupWeightNet != null ? new Weight(_typeConverterProvider.ToFloat(unitClass.GroupWeightNet)) : null,
                                weightGross: unitClass.GroupWeightGross != null ? new Weight(_typeConverterProvider.ToFloat(unitClass.GroupWeightGross)) : null
                                )
                );
                unit.SetMixed(new Mixed(key: unitClass.MixedKey, palletNumber: unitClass.MixedPalletNumber));
                unit.SetUnits(new Units(unitClass.Units));
                unit.SetIsPartial(unitClass.IsPartial);
                unit.SetQuantity(new Quantity(unitClass.Quantity));
                unit.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(unitClass.WeightNet)));
                unit.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(unitClass.WeightGross)));
                unit.SetPalletNumber(unitClass.PalletNumber);
                unit.SetSsccNumber(unitClass.SsccNumber);
                unit.SetProducts(products);
                units.Add(unit);
            }
            workOrder.Order.Units = new List<Unit>(units);

            #endregion

            var extraActivities = new List<ExtraActivity>();
            foreach (var extraActivityClass in workOrderClass.OrderOperationExtraActivities)
            {
                var extraActivity = new ExtraActivity()
                {
                    Code = extraActivityClass.Code,
                    Activity = extraActivityClass.Activity,
                    Description = extraActivityClass.Description,
                    IsExecuted = extraActivityClass.IsExecuted,
                    Quantity = extraActivityClass.Quantity,
                    Remark = extraActivityClass.Remark
                };
                extraActivities.Add(extraActivity);
            }
            workOrder.Order.Operation.ExtraActivities = new List<ExtraActivity>(extraActivities);

            var operationalRemarks = new List<string>();
            foreach (var operationalRemark in workOrderClass.OrderOperationOperationalRemarks)
            {
                operationalRemarks.Add(operationalRemark);
            }
            workOrder.Order.Operation.OperationalRemarks = operationalRemarks;

            #region Operational

            var handledUnits = new List<HandledUnit>();
            foreach (var handledUnitClass in workOrderClass.OperationalUnits)
            {
                var sourceUnit = workOrder.Order.Units.First(x => x.Id == handledUnitClass.SourceUnitId);
                var handledUnit = new HandledUnit(handledUnitClass.Id, sourceUnit);
                var operant = new Operant(handledUnitClass.OperantId, new Login(handledUnitClass.OperantLogin));
                var location = new Location(
                    new Warehouse(new Label(handledUnitClass.LocationWarehouse)),
                    new Gate(new Label(handledUnitClass.LocationGate)),
                    new Row(new Label(handledUnitClass.LocationRow)),
                    new Position(new Label(handledUnitClass.LocationPosition))
                );
                handledUnit.SetOperant(operant);
                handledUnit.SetHandledOn(new HandledOn(handledUnitClass.HandledOn));
                handledUnit.SetLocation(location);
                handledUnit.SetUnits(new Units(handledUnitClass.Units));
                handledUnit.SetIsPartial(handledUnitClass.IsPartial);
                handledUnit.SetQuantity(new Quantity(handledUnitClass.Quantity));
                handledUnit.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(handledUnitClass.WeightNet)));
                handledUnit.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(handledUnitClass.WeightGross)));
                handledUnit.SetPalletNumber(handledUnitClass.PalletNumber);
                handledUnit.SetSsccNumber(handledUnitClass.SsccNumber);

                foreach (var goodClass in handledUnitClass.Products)
                {
                    var good = new Good(goodClass.Id);
                    good.SetConfiguration(
                        new Configuration(
                            code: goodClass.ConfigurationCode,
                            description: goodClass.ConfigurationDescription,
                            quantity: goodClass.ConfigurationQuantity,
                            unitType: goodClass.ConfigurationUnitType,
                            netPerUnit: goodClass.ConfigurationNetPerUnit,
                            netPerUnitAlwaysDifferent: goodClass.ConfigurationNetPerUnitAlwaysDifferent,
                            grossPerUnit: goodClass.ConfigurationGrossPerUnit
                        )
                    );
                    good.SetCode(goodClass.Code);
                    good.SetCustomer(goodClass.Customer);
                    good.SetArrival(goodClass.Arrival);
                    good.SetArticle(goodClass.Article);
                    good.SetArticlePackagingCode(goodClass.ArticlePackagingCode);
                    good.SetName(goodClass.Name);
                    good.SetGtin(goodClass.Gtin);
                    good.SetProductType(goodClass.ProductType);
                    good.SetMaterialType(goodClass.MaterialType);
                    good.SetColor(goodClass.Color);
                    good.SetShape(goodClass.Shape);
                    good.SetLotbatch(goodClass.Lotbatch);
                    good.SetLotbatch2(goodClass.Lotbatch2);
                    good.SetClientReference(goodClass.ClientReference);
                    good.SetClientReference2(goodClass.ClientReference2);
                    good.SetBestBeforeDate(goodClass.BestBeforeDate.HasValue ? new DateOn(goodClass.BestBeforeDate.Value) : null);
                    good.SetDateFifo(goodClass.DateFifo.HasValue ? new DateOn(goodClass.DateFifo.Value) : null);
                    good.SetCustomsDocument(goodClass.CustomsDocument);
                    good.SetStorageStatus(goodClass.StorageStatus);
                    good.SetStackheight(goodClass.Stackheight);
                    good.SetLength(goodClass.Length);
                    good.SetWidth(goodClass.Width);
                    good.SetHeight(goodClass.Height);
                    good.SetOriginalContainer(goodClass.OriginalContainer);
                    good.SetQuantity(new Quantity(goodClass.Quantity));
                    good.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(goodClass.WeightNet)));
                    good.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(goodClass.WeightGross)));

                    handledUnit.AddGood(good);
                }

                handledUnits.Add(handledUnit);
            }

            var remarks = new List<Remark>();
            foreach (var remarkClass in workOrderClass.OperationalRemarks)
            {
                var operantId = remarkClass.OperantId;
                var operantLogin = remarkClass.OperantLogin;
                var operant = new Operant(operantId, new Login(operantLogin));
                var createdOn = new CreatedOn(remarkClass.CreatedOn);
                var text = remarkClass.Text;
                var remark = new Remark(operant, createdOn, text);

                remarks.Add(remark);
            }

            var pictures = new List<Picture>();
            foreach (var pictureClass in workOrderClass.OperationalPictures)
            {

                var operantId = pictureClass.OperantId;
                var operantLogin = pictureClass.OperantLogin;
                var operant = new Operant(operantId, new Login(operantLogin));
                var createdOn = new CreatedOn(pictureClass.CreatedOn);
                var name = pictureClass.Name;
                var picture = new Picture(operant, createdOn, name);

                pictures.Add(picture);
            }

            var inputs = new List<Input>();
            foreach (var inputClass in workOrderClass.OperationalInputs)
            {

                var operantId = inputClass.OperantId;
                var operantLogin = inputClass.OperantLogin;
                var operant = new Operant(operantId, new Login(operantLogin));
                var createdOn = new CreatedOn(inputClass.CreatedOn);
                var value = inputClass.Value;
                var property = inputClass.Property;
                var input = new Input(operant, createdOn, value, property);

                inputs.Add(input);
            }

            var operational = new Operational(Status.Parse(workOrderClass.OperationalStatus));
            operational.SetOperant(workOrderClass.OperationalOperant);

            foreach (var handledUnit in handledUnits)
            {
                operational.AddHandledUnit(handledUnit);
            }

            foreach (var remark in remarks)
            {
                operational.AddRemark(remark);
            }

            foreach (var picture in pictures)
            {
                operational.AddPicture(picture);
            }

            foreach (var input in inputs)
            {
                operational.AddInput(input);
            }

            if (workOrderClass.OperationalStartedOn.HasValue)
            {
                operational.SetStartedOn(new DateOn(workOrderClass.OperationalStartedOn.Value));
            }

            workOrder.Operational = operational;

            #endregion

            return workOrder;
        }
    }
}
