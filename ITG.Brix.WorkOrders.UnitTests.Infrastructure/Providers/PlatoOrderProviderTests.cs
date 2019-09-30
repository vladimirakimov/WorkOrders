using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Providers
{
    [TestClass]
    public class PlatoOrderProviderTests
    {
        [TestMethod]
        public void GetPlatoOrderOverviewShouldSucceed()
        {
            // Arrange
            IPlatoOrderProvider platoOrderProvider = new PlatoOrderProvider();

            var jsonPlatoOrderOverview = @"
{
                ""Source"": ""BKAL33+KBT T"",
  ""Transport"": {
                    ""ID"": ""781415"",
    ""TransportNo"": ""UEN000001"",
    ""RelationType"": ""Inbound"",
    ""Operation"": null,
    ""UnitPlanning"": ""WHT"",
    ""TypePlanning"": ""MAG"",
    ""Customer"": ""DBPLASTICS"",
    ""CustomerReference1"": ""EORDER TEST"",
    ""CustomerReference2"": ""STANDARD"",
    ""CustomerReference3"": ""CR3"",
    ""CustomerReference4"": ""CR4"",
    ""CustomerReference5"": ""CR5"",
    ""LoadingReference"": ""LR"",
    ""ARDReference1"": null,
    ""ARDReference2"": null,
    ""ARDReference3"": null,
    ""ARDReference4"": null,
    ""ARDReference5"": null,
    ""ARDReference6"": null,
    ""ARDReference7"": null,
    ""ARDReference8"": null,
    ""ARDReference9"": null,
    ""ARDReference10"": null,
    ""ProductionSite"": ""CANADA01"",
    ""LoadingPlace"": ""0000201449"",
    ""DeliveryPlace"": ""LB1227"",
    ""BillOfLading"": ""BOL"",
    ""BLNetWeight"": ""1050"",
    ""BLGrossWeight"": ""1075"",
    ""CertificateOfOrigin"": ""4"",
    ""CarrierBooked"": ""LKW"",
    ""CarrierArrived"": ""LKW"",
    ""Kind"": ""40DCI-W"",
    ""TransportType"": ""CP"",
    ""DriverWaits"": ""true"",
    ""Driver"": ""JEF"",
    ""LicensePlateTruck"": ""DEF"",
    ""LicensePlateTrailer"": ""ABC"",
    ""Container"": ""SVEN1231231"",
    ""Railcar"": null,
    ""Seal1"": ""SEAL1"",
    ""Seal2"": ""SEAL2"",
    ""Seal3"": ""SEAL3"",
    ""NetWeightWeighbridge"": ""2480"",
    ""GrossWeightWeighbridge"": ""2500"",
    ""FreeUntilOnTerminal"": ""2018-03-23"",
    ""FreeUntilOnTerminalCustomer"": ""2018-03-27"",
    ""ADR"": null,
    ""CustomsDocument"": ""COMMUNITY"",
    ""DocumentNumber"": ""4444"",
    ""DocumentOffice"": ""VOSSELAAR"",
    ""DocumentDate"": ""2018-03-21"",
    ""ETA"": ""13:01:49Z"",
    ""LTA"": ""13:01:54Z"",
    ""DateExpected"": ""2017-06-14"",
    ""DateArrived"": ""2019-01-16"",
    ""TimeArrived"": ""10:06:50.343Z"",
    ""Site"": ""LB1227"",
    ""OperationalDepartment"": ""SVEN"",
    ""DockingZone"": ""BLOK DB"",
    ""ContainerLocation"": ""1000"",
    ""ContainerStackLocation"": ""1001"",
    ""EorderPriority"": ""KTN"",
    ""Dispatch"": {
                        ""LoadingDock"": null,
      ""DispatchPriority"": ""90"",
      ""DispatchedTo"": null,
      ""DispatchComment"": null
    },
    ""Zone"": ""UNKNOWN_"",
    ""ProductOverview"": ""HE3490LS"",
    ""LotbatchOverview"": ""3010171""
  }
            }
";

            // Act
            var result = platoOrderProvider.GetPlatoOrderOverview(jsonPlatoOrderOverview);

            // Assert
            result.Source.Should().Be("BKAL33+KBT T");
            result.ID.Should().Be("781415");
            result.TransportNo.Should().Be("UEN000001");

            result.Dispatch.Should().NotBeNull();
            result.Dispatch.DispatchPriority.Should().Be("90");
        }
    }
}
