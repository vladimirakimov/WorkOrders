using ITG.Brix.WorkOrders.Application.Exceptions;
using ITG.Brix.WorkOrders.Application.Services.Acls;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Services.Impl
{
    public class DomainConverter : IDomainConverter
    {
        private readonly IPlatoDataAcl _platoDataAcl;
        private readonly ITypeConverterProvider _typeConverterProvider;

        public DomainConverter(IPlatoDataAcl platoDataAcl, ITypeConverterProvider typeConverterProvider)
        {
            _platoDataAcl = platoDataAcl ?? throw Error.ArgumentNull(nameof(platoDataAcl));
            _typeConverterProvider = typeConverterProvider ?? throw Error.ArgumentNull(nameof(typeConverterProvider));
        }

        public Order ToOrder(PlatoOrderOverview platoOrderOverview)
        {
            var order = new Order()
            {
                Origin = new Origin()
                {
                    EntryNumber = platoOrderOverview.ID,
                    Source = platoOrderOverview.Source
                },
                Number = platoOrderOverview.TransportNo,

                Customer = new Customer()
                {
                    Code = platoOrderOverview.Customer,
                    ProductionSite = platoOrderOverview.ProductionSite,
                    Reference1 = platoOrderOverview.CustomerReference1,
                    Reference2 = platoOrderOverview.CustomerReference2,
                    Reference3 = platoOrderOverview.CustomerReference3,
                    Reference4 = platoOrderOverview.CustomerReference4,
                    Reference5 = platoOrderOverview.CustomerReference5
                },
                Customs = new Customs()
                {
                    CertificateOfOrigin = platoOrderOverview.CertificateOfOrigin,
                    Document = new Document()
                    {
                        Name = platoOrderOverview.CustomsDocument,
                        Number = platoOrderOverview.DocumentNumber,
                        Office = platoOrderOverview.DocumentOffice,
                        Date = _platoDataAcl.DateOnOrNull(platoOrderOverview.DocumentDate)
                    }
                },
                Transport = new Transport()
                {
                    Kind = platoOrderOverview.Kind,
                    Type = platoOrderOverview.TransportType,
                    Driver = new Driver()
                    {
                        Name = platoOrderOverview.Driver,
                        Wait = platoOrderOverview.DriverWaits != "true" && platoOrderOverview.DriverWaits != "false" ?
                                Wait.Undefined : platoOrderOverview.DriverWaits != "true" ? Wait.Yes : Wait.No
                    },
                    Delivery = new Delivery()
                    {
                        Place = platoOrderOverview.DeliveryPlace
                    },
                    Loading = new Loading()
                    {
                        Place = platoOrderOverview.LoadingPlace,
                        Reference = platoOrderOverview.LoadingReference,
                    },
                    Truck = new Truck()
                    {
                        LicensePlateTruck = platoOrderOverview.LicensePlateTruck,
                        LicensePlateTrailer = platoOrderOverview.LicensePlateTrailer,
                    },
                    Container = new Container()
                    {
                        Number = platoOrderOverview.Container,
                        Location = platoOrderOverview.ContainerLocation,
                        StackLocation = platoOrderOverview.ContainerStackLocation
                    },
                    Railcar = new Railcar()
                    {
                        Number = platoOrderOverview.Railcar
                    },
                    Ard = new Ard()
                    {
                        Reference1 = platoOrderOverview.ARDReference1,
                        Reference2 = platoOrderOverview.ARDReference2,
                        Reference3 = platoOrderOverview.ARDReference3,
                        Reference4 = platoOrderOverview.ARDReference4,
                        Reference5 = platoOrderOverview.ARDReference5,
                        Reference6 = platoOrderOverview.ARDReference6,
                        Reference7 = platoOrderOverview.ARDReference7,
                        Reference8 = platoOrderOverview.ARDReference8,
                        Reference9 = platoOrderOverview.ARDReference9,
                        Reference10 = platoOrderOverview.ARDReference10
                    },
                    Arrival = new Arrival()
                    {
                        Expected = _platoDataAcl.DateOnOrNull(platoOrderOverview.ETA),
                        Arrived = _platoDataAcl.DateOnOrNull(platoOrderOverview.Arrived),
                        Latest = _platoDataAcl.DateOnOrNull(platoOrderOverview.LTA)
                    },
                    BillOfLading = new BillOfLading()
                    {
                        Number = platoOrderOverview.BillOfLading,
                        WeightNet = platoOrderOverview.BLNetWeight,
                        WeightGross = platoOrderOverview.BLGrossWeight
                    },
                    Carrier = new Carrier()
                    {
                        Arrived = platoOrderOverview.CarrierArrived,
                        Booked = platoOrderOverview.CarrierBooked
                    },
                    Weighbridge = new Weighbridge()
                    {
                        Net = platoOrderOverview.NetWeightWeighbridge,
                        Gross = platoOrderOverview.GrossWeightWeighbridge
                    },
                    Seal = new Seal()
                    {
                        Seal1 = platoOrderOverview.Seal1,
                        Seal2 = platoOrderOverview.Seal2,
                        Seal3 = platoOrderOverview.Seal3
                    },
                    Adr = platoOrderOverview.ADR
                },
                Units = new List<Unit>(),
                Operation = new Operation()
                {
                    Dispatch = new Dispatch()
                    {
                        Priority = platoOrderOverview.Dispatch?.DispatchPriority,
                        To = platoOrderOverview.Dispatch?.DispatchedTo,
                        Comment = platoOrderOverview.Dispatch?.DispatchComment
                    },

                    Type = OperationType.Parse(platoOrderOverview.RelationType),
                    Group = platoOrderOverview.OperationGroup,
                    Name = platoOrderOverview.Operation,
                    UnitPlanning = platoOrderOverview.UnitPlanning,
                    TypePlanning = platoOrderOverview.TypePlanning,
                    Site = platoOrderOverview.Site,
                    Zone = platoOrderOverview.Zone,
                    OperationalDepartment = platoOrderOverview.OperationalDepartment,
                    DockingZone = platoOrderOverview.DockingZone,
                    LoadingDock = platoOrderOverview.Dispatch?.LoadingDock,
                    ProductOverview = platoOrderOverview.ProductOverview,
                    LotbatchOverview = platoOrderOverview.LotbatchOverview
                }
            };

            return order;
        }

        public Order ToOrder(PlatoTransport platoTransport)
        {
            var order = new Order();
            order.Origin.Source = platoTransport.Source;
            order.Origin.EntryNumber = platoTransport.ID;

            order.Number = platoTransport.TransportNo;

            order.Customer.Code = platoTransport.Customer;
            order.Customer.ProductionSite = platoTransport.ProductionSite;
            order.Customer.Reference1 = platoTransport.CustomerReference1;
            order.Customer.Reference2 = platoTransport.CustomerReference2;
            order.Customer.Reference3 = platoTransport.CustomerReference3;
            order.Customer.Reference4 = platoTransport.CustomerReference4;
            order.Customer.Reference5 = platoTransport.CustomerReference5;

            order.Customs.CertificateOfOrigin = platoTransport.CertificateOfOrigin;
            order.Customs.Document.Name = platoTransport.CustomsDocument;
            order.Customs.Document.Number = platoTransport.DocumentNumber;
            order.Customs.Document.Office = platoTransport.DocumentOffice;
            order.Customs.Document.Date = _platoDataAcl.DateOnOrNull(platoTransport.DocumentDate);

            order.Transport.Kind = platoTransport.Kind;
            order.Transport.Type = platoTransport.TransportType;
            order.Transport.Driver.Name = platoTransport.Driver;
            order.Transport.Driver.Wait = platoTransport.DriverWaits != "true" && platoTransport.DriverWaits != "false" ? Wait.Undefined : platoTransport.DriverWaits != "true" ? Wait.Yes : Wait.No;
            order.Transport.Delivery.Place = platoTransport.DeliveryPlace;
            order.Transport.Loading.Place = platoTransport.LoadingPlace;
            order.Transport.Loading.Reference = platoTransport.LoadingReference;
            order.Transport.Truck.LicensePlateTruck = platoTransport.LicensePlateTruck;
            order.Transport.Truck.LicensePlateTrailer = platoTransport.LicensePlateTrailer;
            order.Transport.Container.Number = platoTransport.Container;
            order.Transport.Container.Location = platoTransport.ContainerLocation;
            order.Transport.Container.StackLocation = platoTransport.ContainerStackLocation;
            order.Transport.Container.FreeUntilOnTerminal.ShippingLine = platoTransport.FreeUntilOnTerminal;
            order.Transport.Container.FreeUntilOnTerminal.Customer = platoTransport.FreeUntilOnTerminalCustomer;
            order.Transport.Railcar.Number = platoTransport.Railcar;
            order.Transport.Ard.Reference1 = platoTransport.ARDReference1;
            order.Transport.Ard.Reference2 = platoTransport.ARDReference2;
            order.Transport.Ard.Reference3 = platoTransport.ARDReference3;
            order.Transport.Ard.Reference4 = platoTransport.ARDReference4;
            order.Transport.Ard.Reference5 = platoTransport.ARDReference5;
            order.Transport.Ard.Reference6 = platoTransport.ARDReference6;
            order.Transport.Ard.Reference7 = platoTransport.ARDReference7;
            order.Transport.Ard.Reference8 = platoTransport.ARDReference8;
            order.Transport.Ard.Reference9 = platoTransport.ARDReference9;
            order.Transport.Ard.Reference10 = platoTransport.ARDReference10;
            order.Transport.Arrival.Expected = _platoDataAcl.DateOnOrNull(platoTransport.ETA);
            order.Transport.Arrival.Arrived = _platoDataAcl.DateOnOrNull(platoTransport.Arrived);
            order.Transport.Arrival.Latest = _platoDataAcl.DateOnOrNull(platoTransport.LTA);
            order.Transport.BillOfLading.Number = platoTransport.BillOfLading;
            order.Transport.BillOfLading.WeightNet = platoTransport.BLNetWeight;
            order.Transport.BillOfLading.WeightGross = platoTransport.BLGrossWeight;
            order.Transport.Carrier.Arrived = platoTransport.CarrierArrived;
            order.Transport.Carrier.Booked = platoTransport.CarrierBooked;
            order.Transport.Weighbridge.Net = platoTransport.NetWeightWeighbridge;
            order.Transport.Weighbridge.Gross = platoTransport.GrossWeightWeighbridge;
            order.Transport.Seal.Seal1 = platoTransport.Seal1;
            order.Transport.Seal.Seal2 = platoTransport.Seal2;
            order.Transport.Seal.Seal3 = platoTransport.Seal3;
            order.Transport.Adr = platoTransport.ADR;


            #region Units

            var productEntryMarkers = new List<PlatoProductEntryMarker>();
            foreach (var productEntry in platoTransport.ProductEntries)
            {
                if (productEntry != null)
                {
                    productEntryMarkers.Add(new PlatoProductEntryMarker { ProductEntry = productEntry, IsProcessed = false });
                }
            }
            var units = new List<Unit>();
            for (var ix = 0; ix < productEntryMarkers.Count; ix++)
            {
                var productEntryMarker = productEntryMarkers[ix];
                if (!productEntryMarker.IsProcessed)
                {
                    var productEntry = productEntryMarker.ProductEntry;

                    var quantityShu = _typeConverterProvider.ToInt(productEntry.QuantitySHU, 1);
                    var mixedKey = productEntry.MixedID;
                    if (!string.IsNullOrWhiteSpace(mixedKey) && mixedKey != "0")
                    {
                        var products = new List<Product>();
                        for (var jx = ix; jx < productEntryMarkers.Count; jx++)
                        {
                            var currentProductEntryMarker = productEntryMarkers[jx];
                            var currentProductEntry = currentProductEntryMarker.ProductEntry;
                            if (mixedKey.Equals(currentProductEntry.MixedID))
                            {
                                var productId = _platoDataAcl.ProductId(currentProductEntry.Configuration.Code);
                                var product = new Product(productId)
                                {
                                    Remarks = new List<string>(currentProductEntry.ProductRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                    SafetyRemarks = new List<string>(currentProductEntry.SafetyRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                    Notes = new List<string>(currentProductEntry.Notes.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                };
                                product.SetConfiguration(
                                    new Configuration(
                                        code: currentProductEntry.Configuration.Code,
                                        description: currentProductEntry.Configuration.Description,
                                        quantity: currentProductEntry.Configuration.Quantity,
                                        unitType: currentProductEntry.Configuration.ConfigurationUnit.UnitType,
                                        netPerUnit: currentProductEntry.Configuration.ConfigurationUnit.NetPerUnit,
                                        netPerUnitAlwaysDifferent: currentProductEntry.Configuration.ConfigurationUnit.NetPerUnitAlwaysDifferent,
                                        grossPerUnit: currentProductEntry.Configuration.ConfigurationUnit.GrossPerUnit
                                    )
                                );
                                product.SetCode(currentProductEntry.EntryNo);
                                product.SetCustomer(currentProductEntry.Customer);
                                product.SetArrival(currentProductEntry.Arrival);
                                product.SetArticle(currentProductEntry.Article);
                                product.SetArticlePackagingCode(currentProductEntry.ArticlePackagingCode);
                                product.SetName(currentProductEntry.ProductCode);
                                product.SetGtin(currentProductEntry.Product.GTIN);
                                product.SetProductType(currentProductEntry.Product.ProductType);
                                product.SetMaterialType(currentProductEntry.Product.MaterialType);
                                product.SetColor(currentProductEntry.Product.Color);
                                product.SetShape(currentProductEntry.Product.Shape);
                                product.SetLotbatch(currentProductEntry.LotBatch);
                                product.SetLotbatch2(currentProductEntry.LotBatch2);
                                product.SetClientReference(currentProductEntry.ClientReference);
                                product.SetClientReference2(null);
                                product.SetBestBeforeDate(_platoDataAcl.DateOnOrNull(currentProductEntry.BestBeforeDate));
                                product.SetDateFifo(_platoDataAcl.DateOnOrNull(currentProductEntry.DateFifo));
                                product.SetCustomsDocument(currentProductEntry.CustomsDocument);
                                product.SetStorageStatus(currentProductEntry.StorageStatus);
                                product.SetStackheight(currentProductEntry.StackHeight);
                                product.SetLength(currentProductEntry.Length);
                                product.SetWidth(currentProductEntry.Width);
                                product.SetHeight(currentProductEntry.Height);
                                product.SetOriginalContainer(currentProductEntry.OriginalContainer);
                                product.SetQuantity(new Quantity(_typeConverterProvider.ToInt(currentProductEntry.Quantity)));
                                product.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(currentProductEntry.NetWeight)));
                                product.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(currentProductEntry.GrossWeight)));
                                products.Add(product);

                                productEntryMarkers[jx].IsProcessed = true;
                            }

                        }

                        var unit = new Unit(Guid.NewGuid(), UnitType.Multiple);
                        var location = new Location(
                            new Warehouse(new Label(_platoDataAcl.Location(productEntry.Location.Warehouse))),
                            new Gate(new Label(_platoDataAcl.Location(productEntry.Location.Gate))),
                            new Row(new Label(_platoDataAcl.Location(productEntry.Location.Row))),
                            new Position(new Label(_platoDataAcl.Location(productEntry.Location.Position)))
                            );
                        unit.SetLocation(location);
                        unit.SetGroup(new Group(
                                        key: null,
                                        weightNet: null,
                                        weightGross: null
                                        )
                        );
                        unit.SetMixed(new Mixed(key: mixedKey, palletNumber: productEntry.MixedPalletNo));
                        unit.SetUnits(new Units(1));
                        unit.SetIsPartial("true".Equals(productEntry.IsPartial));
                        unit.SetQuantity(new Quantity(products.Sum(x => x.Quantity)));
                        unit.SetWeightNet(new Weight(products.Sum(x => _typeConverterProvider.ToFloat(x.WeightNet))));
                        unit.SetWeightGross(new Weight(products.Sum(x => _typeConverterProvider.ToFloat(x.WeightGross))));
                        unit.SetPalletNumber(productEntry.PalletNo);
                        unit.SetSsccNumber(productEntry.SSCCNo);
                        unit.SetProducts(products);
                        units.Add(unit);
                    }
                    else if (quantityShu == 1)
                    {
                        var products = new List<Product>();
                        var productId = _platoDataAcl.ProductId(productEntry.Configuration.Code);
                        var product = new Product(productId)
                        {
                            Remarks = new List<string>(productEntry.ProductRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                            SafetyRemarks = new List<string>(productEntry.SafetyRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                            Notes = new List<string>(productEntry.Notes.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x)))
                        };
                        product.SetConfiguration(
                            new Configuration(
                                code: productEntry.Configuration.Code,
                                description: productEntry.Configuration.Description,
                                quantity: productEntry.Configuration.Quantity,
                                unitType: productEntry.Configuration.ConfigurationUnit.UnitType,
                                netPerUnit: productEntry.Configuration.ConfigurationUnit.NetPerUnit,
                                netPerUnitAlwaysDifferent: productEntry.Configuration.ConfigurationUnit.NetPerUnitAlwaysDifferent,
                                grossPerUnit: productEntry.Configuration.ConfigurationUnit.GrossPerUnit
                            )
                        );
                        product.SetCode(productEntry.EntryNo);
                        product.SetCustomer(productEntry.Customer);
                        product.SetArrival(productEntry.Arrival);
                        product.SetArticle(productEntry.Article);
                        product.SetArticlePackagingCode(productEntry.ArticlePackagingCode);
                        product.SetName(productEntry.ProductCode);
                        product.SetGtin(productEntry.Product.GTIN);
                        product.SetProductType(productEntry.Product.ProductType);
                        product.SetMaterialType(productEntry.Product.MaterialType);
                        product.SetColor(productEntry.Product.Color);
                        product.SetShape(productEntry.Product.Shape);
                        product.SetLotbatch(productEntry.LotBatch);
                        product.SetLotbatch2(productEntry.LotBatch2);
                        product.SetClientReference(productEntry.ClientReference);
                        product.SetClientReference2(null);
                        product.SetBestBeforeDate(_platoDataAcl.DateOnOrNull(productEntry.BestBeforeDate));
                        product.SetDateFifo(_platoDataAcl.DateOnOrNull(productEntry.DateFifo));
                        product.SetCustomsDocument(productEntry.CustomsDocument);
                        product.SetStorageStatus(productEntry.StorageStatus);
                        product.SetStackheight(productEntry.StackHeight);
                        product.SetLength(productEntry.Length);
                        product.SetWidth(productEntry.Width);
                        product.SetHeight(productEntry.Height);
                        product.SetOriginalContainer(productEntry.OriginalContainer);
                        product.SetQuantity(new Quantity(_typeConverterProvider.ToInt(productEntry.Quantity)));
                        product.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(productEntry.NetWeight)));
                        product.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(productEntry.GrossWeight)));
                        products.Add(product);


                        var unit = new Unit(Guid.NewGuid(), UnitType.Single);
                        var location = new Location(
                            new Warehouse(new Label(_platoDataAcl.Location(productEntry.Location.Warehouse))),
                            new Gate(new Label(_platoDataAcl.Location(productEntry.Location.Gate))),
                            new Row(new Label(_platoDataAcl.Location(productEntry.Location.Row))),
                            new Position(new Label(_platoDataAcl.Location(productEntry.Location.Position)))
                        );
                        unit.SetLocation(location);
                        unit.SetGroup(new Group(
                                        key: null,
                                        weightNet: null,
                                        weightGross: null
                                        )
                        );
                        unit.SetMixed(new Mixed(key: null, palletNumber: null));
                        unit.SetUnits(new Units(1));
                        unit.SetIsPartial("true".Equals(productEntry.IsPartial));
                        unit.SetQuantity(new Quantity(_typeConverterProvider.ToInt(productEntry.Quantity)));
                        unit.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(productEntry.NetWeight)));
                        unit.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(productEntry.GrossWeight)));
                        unit.SetPalletNumber(productEntry.PalletNo);
                        unit.SetSsccNumber(productEntry.SSCCNo);
                        unit.SetProducts(products);
                        units.Add(unit);
                    }
                    else if (quantityShu > 1)
                    {
                        var productEntryConfigurationQuantity = _typeConverterProvider.ToInt(productEntry.Configuration.Quantity);
                        var productEntryQuantity = _typeConverterProvider.ToInt(productEntry.Quantity);

                        if (productEntryConfigurationQuantity > 1)
                        {
                            var ratio = (float)productEntryQuantity / productEntryConfigurationQuantity;

                            var groupKey = "shu-" + productEntry.EntryNo + "-" + quantityShu;
                            if (_typeConverterProvider.IsInt(ratio))
                            {
                                var products = new List<Product>();
                                var productId = _platoDataAcl.ProductId(productEntry.Configuration.Code);
                                var product = new Product(productId)
                                {
                                    Remarks = new List<string>(productEntry.ProductRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                    SafetyRemarks = new List<string>(productEntry.SafetyRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                    Notes = new List<string>(productEntry.Notes.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x)))
                                };
                                product.SetConfiguration(
                                    new Configuration(
                                        code: productEntry.Configuration.Code,
                                        description: productEntry.Configuration.Description,
                                        quantity: productEntry.Configuration.Quantity,
                                        unitType: productEntry.Configuration.ConfigurationUnit.UnitType,
                                        netPerUnit: productEntry.Configuration.ConfigurationUnit.NetPerUnit,
                                        netPerUnitAlwaysDifferent: productEntry.Configuration.ConfigurationUnit.NetPerUnitAlwaysDifferent,
                                        grossPerUnit: productEntry.Configuration.ConfigurationUnit.GrossPerUnit
                                    )
                                );
                                product.SetCode(productEntry.EntryNo);
                                product.SetCustomer(productEntry.Customer);
                                product.SetArrival(productEntry.Arrival);
                                product.SetArticle(productEntry.Article);
                                product.SetArticlePackagingCode(productEntry.ArticlePackagingCode);
                                product.SetName(productEntry.ProductCode);
                                product.SetGtin(productEntry.Product.GTIN);
                                product.SetProductType(productEntry.Product.ProductType);
                                product.SetMaterialType(productEntry.Product.MaterialType);
                                product.SetColor(productEntry.Product.Color);
                                product.SetShape(productEntry.Product.Shape);
                                product.SetLotbatch(productEntry.LotBatch);
                                product.SetLotbatch2(productEntry.LotBatch2);
                                product.SetClientReference(productEntry.ClientReference);
                                product.SetClientReference2(null);
                                product.SetBestBeforeDate(_platoDataAcl.DateOnOrNull(productEntry.BestBeforeDate));
                                product.SetDateFifo(_platoDataAcl.DateOnOrNull(productEntry.DateFifo));
                                product.SetCustomsDocument(productEntry.CustomsDocument);
                                product.SetStorageStatus(productEntry.StorageStatus);
                                product.SetStackheight(productEntry.StackHeight);
                                product.SetLength(productEntry.Length);
                                product.SetWidth(productEntry.Width);
                                product.SetHeight(productEntry.Height);
                                product.SetOriginalContainer(productEntry.OriginalContainer);
                                product.SetQuantity(new Quantity(_typeConverterProvider.ToInt(productEntry.Quantity) / (int)ratio));
                                product.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(productEntry.NetWeight)) / ratio);
                                product.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(productEntry.GrossWeight)) / ratio);
                                products.Add(product);

                                var unit = new Unit(Guid.NewGuid(), UnitType.Weight);
                                var location = new Location(
                                    new Warehouse(new Label(_platoDataAcl.Location(productEntry.Location.Warehouse))),
                                    new Gate(new Label(_platoDataAcl.Location(productEntry.Location.Gate))),
                                    new Row(new Label(_platoDataAcl.Location(productEntry.Location.Row))),
                                    new Position(new Label(_platoDataAcl.Location(productEntry.Location.Position)))
                                );
                                unit.SetLocation(location);
                                unit.SetGroup(new Group(
                                                key: groupKey,
                                                weightNet: new Weight(_typeConverterProvider.ToFloat(productEntry.NetWeight)),
                                                weightGross: new Weight(_typeConverterProvider.ToFloat(productEntry.GrossWeight))
                                                )
                                );
                                unit.SetMixed(new Mixed(key: null, palletNumber: null));
                                unit.SetUnits(new Units((int)ratio));
                                unit.SetIsPartial(false);
                                unit.SetQuantity(new Quantity(_typeConverterProvider.ToInt(productEntry.Quantity)));
                                unit.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(productEntry.NetWeight)));
                                unit.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(productEntry.GrossWeight)));
                                unit.SetPalletNumber(productEntry.PalletNo);
                                unit.SetSsccNumber(productEntry.SSCCNo);
                                unit.SetProducts(products);
                                units.Add(unit);
                            }
                            else
                            {
                                var unitsComplete = (int)ratio;
                                var unitsPartial = 1;
                                var unitQuantityComplete = unitsComplete * _typeConverterProvider.ToInt(productEntry.Configuration.Quantity);
                                var unitQuantityPartial = _typeConverterProvider.ToInt(productEntry.Quantity) - unitQuantityComplete;
                                var unitNetWeightComplete = unitQuantityComplete * _typeConverterProvider.ToFloat(productEntry.Configuration.ConfigurationUnit.NetPerUnit);
                                var unitNetWeightPartial = _typeConverterProvider.ToFloat(productEntry.NetWeight) - unitNetWeightComplete;
                                var unitGrossWeightComplete = unitQuantityComplete * _typeConverterProvider.ToFloat(productEntry.Configuration.ConfigurationUnit.GrossPerUnit);
                                var unitGrossWeightPartial = _typeConverterProvider.ToFloat(productEntry.GrossWeight) - unitGrossWeightComplete;

                                var productQunatityComplete = new Quantity(unitQuantityComplete / unitsComplete);
                                var productQunatityPartial = new Quantity(unitQuantityPartial / unitsPartial);
                                var productNetWeightComplete = new Weight(unitNetWeightComplete / unitsComplete);
                                var productNetWeightPartial = new Weight(unitNetWeightPartial / unitsPartial);
                                var productGrossWeightComplete = new Weight(unitGrossWeightComplete / unitsComplete);
                                var productGrossWeightPartial = new Weight(unitGrossWeightPartial / unitsPartial);

                                List<Product> products;
                                Guid productId;
                                Product product;
                                Unit unit;

                                #region Complete
                                products = new List<Product>();
                                productId = _platoDataAcl.ProductId(productEntry.Configuration.Code);
                                product = new Product(productId)
                                {
                                    Remarks = new List<string>(productEntry.ProductRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                    SafetyRemarks = new List<string>(productEntry.SafetyRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                    Notes = new List<string>(productEntry.Notes.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x)))
                                };
                                product.SetConfiguration(
                                    new Configuration(
                                        code: productEntry.Configuration.Code,
                                        description: productEntry.Configuration.Description,
                                        quantity: productEntry.Configuration.Quantity,
                                        unitType: productEntry.Configuration.ConfigurationUnit.UnitType,
                                        netPerUnit: productEntry.Configuration.ConfigurationUnit.NetPerUnit,
                                        netPerUnitAlwaysDifferent: productEntry.Configuration.ConfigurationUnit.NetPerUnitAlwaysDifferent,
                                        grossPerUnit: productEntry.Configuration.ConfigurationUnit.GrossPerUnit
                                    )
                                );
                                product.SetCode(productEntry.EntryNo);
                                product.SetCustomer(productEntry.Customer);
                                product.SetArrival(productEntry.Arrival);
                                product.SetArticle(productEntry.Article);
                                product.SetArticlePackagingCode(productEntry.ArticlePackagingCode);
                                product.SetName(productEntry.ProductCode);
                                product.SetGtin(productEntry.Product.GTIN);
                                product.SetProductType(productEntry.Product.ProductType);
                                product.SetMaterialType(productEntry.Product.MaterialType);
                                product.SetColor(productEntry.Product.Color);
                                product.SetShape(productEntry.Product.Shape);
                                product.SetLotbatch(productEntry.LotBatch);
                                product.SetLotbatch2(productEntry.LotBatch2);
                                product.SetClientReference(productEntry.ClientReference);
                                product.SetClientReference2(null);
                                product.SetBestBeforeDate(_platoDataAcl.DateOnOrNull(productEntry.BestBeforeDate));
                                product.SetDateFifo(_platoDataAcl.DateOnOrNull(productEntry.DateFifo));
                                product.SetCustomsDocument(productEntry.CustomsDocument);
                                product.SetStorageStatus(productEntry.StorageStatus);
                                product.SetStackheight(productEntry.StackHeight);
                                product.SetLength(productEntry.Length);
                                product.SetWidth(productEntry.Width);
                                product.SetHeight(productEntry.Height);
                                product.SetOriginalContainer(productEntry.OriginalContainer);
                                product.SetQuantity(productQunatityComplete);
                                product.SetWeightNet(productNetWeightComplete);
                                product.SetWeightGross(productGrossWeightComplete);
                                products.Add(product);


                                unit = new Unit(Guid.NewGuid(), UnitType.Weight);
                                var location = new Location(
                                    new Warehouse(new Label(_platoDataAcl.Location(productEntry.Location.Warehouse))),
                                    new Gate(new Label(_platoDataAcl.Location(productEntry.Location.Gate))),
                                    new Row(new Label(_platoDataAcl.Location(productEntry.Location.Row))),
                                    new Position(new Label(_platoDataAcl.Location(productEntry.Location.Position)))
                                );
                                unit.SetLocation(location);
                                unit.SetGroup(new Group(
                                                key: groupKey,
                                                weightNet: new Weight(_typeConverterProvider.ToFloat(productEntry.NetWeight)),
                                                weightGross: new Weight(_typeConverterProvider.ToFloat(productEntry.GrossWeight))
                                                )
                                );
                                unit.SetMixed(new Mixed(key: null, palletNumber: null));
                                unit.SetUnits(new Units(unitsComplete));
                                unit.SetIsPartial(false);
                                unit.SetQuantity(new Quantity(unitQuantityComplete));
                                unit.SetWeightNet(new Weight(unitNetWeightComplete));
                                unit.SetWeightGross(new Weight(unitGrossWeightComplete));
                                unit.SetPalletNumber(productEntry.PalletNo);
                                unit.SetSsccNumber(productEntry.SSCCNo);
                                unit.SetProducts(products);
                                units.Add(unit);
                                #endregion
                                #region Partial
                                products = new List<Product>();
                                productId = _platoDataAcl.ProductId(productEntry.Configuration.Code);
                                product = new Product(productId)
                                {
                                    Remarks = new List<string>(productEntry.ProductRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                    SafetyRemarks = new List<string>(productEntry.SafetyRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                    Notes = new List<string>(productEntry.Notes.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x)))
                                };
                                product.SetConfiguration(
                                    new Configuration(
                                        code: productEntry.Configuration.Code,
                                        description: productEntry.Configuration.Description,
                                        quantity: productEntry.Configuration.Quantity,
                                        unitType: productEntry.Configuration.ConfigurationUnit.UnitType,
                                        netPerUnit: productEntry.Configuration.ConfigurationUnit.NetPerUnit,
                                        netPerUnitAlwaysDifferent: productEntry.Configuration.ConfigurationUnit.NetPerUnitAlwaysDifferent,
                                        grossPerUnit: productEntry.Configuration.ConfigurationUnit.GrossPerUnit
                                    )
                                );
                                product.SetCode(productEntry.EntryNo);
                                product.SetCustomer(productEntry.Customer);
                                product.SetArrival(productEntry.Arrival);
                                product.SetArticle(productEntry.Article);
                                product.SetArticlePackagingCode(productEntry.ArticlePackagingCode);
                                product.SetName(productEntry.ProductCode);
                                product.SetGtin(productEntry.Product.GTIN);
                                product.SetProductType(productEntry.Product.ProductType);
                                product.SetMaterialType(productEntry.Product.MaterialType);
                                product.SetColor(productEntry.Product.Color);
                                product.SetShape(productEntry.Product.Shape);
                                product.SetLotbatch(productEntry.LotBatch);
                                product.SetLotbatch2(productEntry.LotBatch2);
                                product.SetClientReference(productEntry.ClientReference);
                                product.SetClientReference2(null);
                                product.SetBestBeforeDate(_platoDataAcl.DateOnOrNull(productEntry.BestBeforeDate));
                                product.SetDateFifo(_platoDataAcl.DateOnOrNull(productEntry.DateFifo));
                                product.SetCustomsDocument(productEntry.CustomsDocument);
                                product.SetStorageStatus(productEntry.StorageStatus);
                                product.SetStackheight(productEntry.StackHeight);
                                product.SetLength(productEntry.Length);
                                product.SetWidth(productEntry.Width);
                                product.SetHeight(productEntry.Height);
                                product.SetOriginalContainer(productEntry.OriginalContainer);
                                product.SetQuantity(productQunatityPartial);
                                product.SetWeightNet(productNetWeightPartial);
                                product.SetWeightGross(productGrossWeightPartial);

                                products.Add(product);


                                unit = new Unit(Guid.NewGuid(), UnitType.Weight);
                                location = new Location(
                                    new Warehouse(new Label(_platoDataAcl.Location(productEntry.Location.Warehouse))),
                                    new Gate(new Label(_platoDataAcl.Location(productEntry.Location.Gate))),
                                    new Row(new Label(_platoDataAcl.Location(productEntry.Location.Row))),
                                    new Position(new Label(_platoDataAcl.Location(productEntry.Location.Position)))
                                );
                                unit.SetLocation(location);
                                unit.SetGroup(new Group(
                                                key: groupKey,
                                                weightNet: new Weight(_typeConverterProvider.ToFloat(productEntry.NetWeight)),
                                                weightGross: new Weight(_typeConverterProvider.ToFloat(productEntry.GrossWeight))
                                                )
                                );
                                unit.SetMixed(new Mixed(key: null, palletNumber: null));
                                unit.SetUnits(new Units(unitsPartial));
                                unit.SetIsPartial(true);
                                unit.SetQuantity(new Quantity(unitQuantityPartial));
                                unit.SetWeightNet(new Weight(unitNetWeightPartial));
                                unit.SetWeightGross(new Weight(unitGrossWeightPartial));
                                unit.SetPalletNumber(productEntry.PalletNo);
                                unit.SetSsccNumber(productEntry.SSCCNo);
                                unit.SetProducts(products);
                                units.Add(unit);
                                #endregion
                            }
                        }
                        else if (productEntryConfigurationQuantity == 1)
                        {
                            var products = new List<Product>();
                            var productId = _platoDataAcl.ProductId(productEntry.Configuration.Code);
                            var product = new Product(productId)
                            {
                                Remarks = new List<string>(productEntry.ProductRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                SafetyRemarks = new List<string>(productEntry.SafetyRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x))),
                                Notes = new List<string>(productEntry.Notes.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x)))
                            };
                            product.SetConfiguration(
                                new Configuration(
                                    code: productEntry.Configuration.Code,
                                    description: productEntry.Configuration.Description,
                                    quantity: productEntry.Configuration.Quantity,
                                    unitType: productEntry.Configuration.ConfigurationUnit.UnitType,
                                    netPerUnit: productEntry.Configuration.ConfigurationUnit.NetPerUnit,
                                    netPerUnitAlwaysDifferent: productEntry.Configuration.ConfigurationUnit.NetPerUnitAlwaysDifferent,
                                    grossPerUnit: productEntry.Configuration.ConfigurationUnit.GrossPerUnit
                                )
                            );
                            product.SetCode(productEntry.EntryNo);
                            product.SetCustomer(productEntry.Customer);
                            product.SetArrival(productEntry.Arrival);
                            product.SetArticle(productEntry.Article);
                            product.SetArticlePackagingCode(productEntry.ArticlePackagingCode);
                            product.SetName(productEntry.ProductCode);
                            product.SetGtin(productEntry.Product.GTIN);
                            product.SetProductType(productEntry.Product.ProductType);
                            product.SetMaterialType(productEntry.Product.MaterialType);
                            product.SetColor(productEntry.Product.Color);
                            product.SetShape(productEntry.Product.Shape);
                            product.SetLotbatch(productEntry.LotBatch);
                            product.SetLotbatch2(productEntry.LotBatch2);
                            product.SetClientReference(productEntry.ClientReference);
                            product.SetClientReference2(null);
                            product.SetBestBeforeDate(_platoDataAcl.DateOnOrNull(productEntry.BestBeforeDate));
                            product.SetDateFifo(_platoDataAcl.DateOnOrNull(productEntry.DateFifo));
                            product.SetCustomsDocument(productEntry.CustomsDocument);
                            product.SetStorageStatus(productEntry.StorageStatus);
                            product.SetStackheight(productEntry.StackHeight);
                            product.SetLength(productEntry.Length);
                            product.SetWidth(productEntry.Width);
                            product.SetHeight(productEntry.Height);
                            product.SetOriginalContainer(productEntry.OriginalContainer);
                            product.SetQuantity(new Quantity(1));
                            product.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(productEntry.NetWeight)) / _typeConverterProvider.ToFloat(productEntry.Quantity));
                            product.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(productEntry.GrossWeight)) / _typeConverterProvider.ToFloat(productEntry.Quantity));
                            products.Add(product);


                            var unit = new Unit(Guid.NewGuid(), UnitType.Single);
                            var location = new Location(
                                new Warehouse(new Label(_platoDataAcl.Location(productEntry.Location.Warehouse))),
                                new Gate(new Label(_platoDataAcl.Location(productEntry.Location.Gate))),
                                new Row(new Label(_platoDataAcl.Location(productEntry.Location.Row))),
                                new Position(new Label(_platoDataAcl.Location(productEntry.Location.Position)))
                            );
                            unit.SetLocation(location);
                            unit.SetGroup(new Group(
                                            key: null,
                                            weightNet: null,
                                            weightGross: null
                                            )
                            );
                            unit.SetMixed(new Mixed(key: null, palletNumber: null));
                            unit.SetUnits(new Units(_typeConverterProvider.ToInt(productEntry.QuantitySHU)));
                            unit.SetIsPartial("true".Equals(productEntry.IsPartial));
                            unit.SetQuantity(new Quantity(_typeConverterProvider.ToInt(productEntry.Quantity)));
                            unit.SetWeightNet(new Weight(_typeConverterProvider.ToFloat(productEntry.NetWeight)));
                            unit.SetWeightGross(new Weight(_typeConverterProvider.ToFloat(productEntry.GrossWeight)));
                            unit.SetPalletNumber(productEntry.PalletNo);
                            unit.SetSsccNumber(productEntry.SSCCNo);
                            unit.SetProducts(products);
                            units.Add(unit);
                        }
                    }
                }
            }
            order.Units = units;

            #endregion

            order.Operation.Priority.Code = platoTransport.EorderPriority;
            order.Operation.Priority.Value = null;//Plato should provide!!!
            order.Operation.Dispatch.Priority = platoTransport.Dispatch?.DispatchPriority;
            order.Operation.Dispatch.To = platoTransport.Dispatch?.DispatchedTo;
            order.Operation.Dispatch.Comment = platoTransport.Dispatch?.DispatchComment;


            var extraActivities = new List<ExtraActivity>();
            foreach (var platoExtraActivity in platoTransport.ExtraActivities)
            {
                var isEmpty = platoExtraActivity.ID == "0" &&
                              string.IsNullOrWhiteSpace(platoExtraActivity.Activity) &&
                              string.IsNullOrWhiteSpace(platoExtraActivity.Description) &&
                              string.IsNullOrWhiteSpace(platoExtraActivity.IsExecuted) &&
                              platoExtraActivity.Quantity == "1" &&
                              string.IsNullOrWhiteSpace(platoExtraActivity.Description) &&
                              string.IsNullOrWhiteSpace(platoExtraActivity.Remarks);
                if (!isEmpty)
                {
                    extraActivities.Add(new ExtraActivity()
                    {
                        Code = platoExtraActivity.ID,
                        Activity = platoExtraActivity.Activity,
                        Description = platoExtraActivity.Description,
                        IsExecuted = platoExtraActivity.IsExecuted,
                        Quantity = platoExtraActivity.Quantity,
                        Remark = platoExtraActivity.Remarks
                    });
                }
            }
            order.Operation.ExtraActivities = extraActivities;

            order.Operation.Type = OperationType.Parse(platoTransport.RelationType);
            order.Operation.Group = platoTransport.OperationGroup;
            order.Operation.Name = platoTransport.Operation;
            order.Operation.UnitPlanning = platoTransport.UnitPlanning;
            order.Operation.TypePlanning = platoTransport.TypePlanning;
            order.Operation.Site = platoTransport.Site;
            order.Operation.Zone = platoTransport.Zone;
            order.Operation.OperationalDepartment = platoTransport.OperationalDepartment;
            order.Operation.OperationalRemarks = new List<string>(platoTransport.OperationalRemarks.Select(x => x).Where(x => !string.IsNullOrWhiteSpace(x)));
            order.Operation.DockingZone = platoTransport.DockingZone;
            order.Operation.LoadingDock = platoTransport.Dispatch?.LoadingDock;
            order.Operation.ProductOverview = platoTransport.ProductOverview;
            order.Operation.LotbatchOverview = platoTransport.LotbatchOverview;

            return order;
        }
    }
}
