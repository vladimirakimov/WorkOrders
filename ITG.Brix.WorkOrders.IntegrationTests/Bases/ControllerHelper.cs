using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.IntegrationTests.Extensions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.IntegrationTests.Bases
{
    public static class ControllerHelper
    {
        public static string GetPlatoOverviewJsonAsString(string source, string site, string zone)
        {
            var result = @"
{
    ""Source"":""" + source + @""",
    ""Transport"":{
                    ""ID"":""781415"",
        ""TransportNo"":""UEN000001"",
        ""RelationType"":""Inbound"",
        ""Operation"":""Unload into warehouse"",
        ""UnitPlanning"":""WHT"",
        ""TypePlanning"":""MAG"",
        ""Customer"":""DBPLASTICS"",
        ""CustomerReference1"":""EORDER TEST"",
        ""CustomerReference2"":""STANDARD"",
        ""CustomerReference3"":""CR3"",
        ""CustomerReference4"":""CR4"",
        ""CustomerReference5"":""CR5X"",
        ""LoadingReference"":""LR"",
        ""ARDReference1"":""CR3"",
        ""ARDReference2"":""CR4"",
        ""ARDReference3"":""CR5X"",
        ""ARDReference4"":""EORDERTEST"",
        ""ARDReference5"":""STANDARD"",
        ""ARDReference6"":""SVEN1231231"",
        ""ARDReference7"":null,
        ""ARDReference8"":null,
        ""ARDReference9"":null,
        ""ARDReference10"":null,
        ""ProductionSite"":""CANADA01"",
        ""LoadingPlace"":""0000201449"",
        ""DeliveryPlace"":""LB1227"",
        ""BillOfLading"":""BOL"",
        ""BLNetWeight"":""1050"",
        ""BLGrossWeight"":""1075"",
        ""CertificateOfOrigin"":""4"",
        ""CarrierBooked"":""LKW"",
        ""CarrierArrived"":""LKW"",
        ""Kind"":""40DCI-W"",
        ""TransportType"":""CP"",
        ""DriverWaits"":""true"",
        ""Driver"":""DRIVER"",
        ""LicensePlateTruck"":""TRUCK"",
        ""LicensePlateTrailer"":""TRAILER"",
        ""Container"":""SVEN1231231"",
        ""Railcar"":null,
        ""Seal1"":""SEAL1"",
        ""Seal2"":""SEAL2"",
        ""Seal3"":""SEAL3"",
        ""NetWeightWeighbridge"":""2480"",
        ""GrossWeightWeighbridge"":""2500"",
        ""FreeUntilOnTerminal"":""2018-03-23T12:01:49Z"",
        ""FreeUntilOnTerminalCustomer"":""2018-03-27T12:01:49Z"",
        ""ADR"":null,
        ""CustomsDocument"":""COMMUNITY"",
        ""DocumentNumber"":""4444"",
        ""DocumentOffice"":""VOSSELAAR"",
        ""DocumentDate"":""2018-03-21T12:01:49Z"",
        ""ETA"":""2017-06-14T12:01:49Z"",
        ""LTA"":null,
        ""Arrived"":null,
        ""Site"":""" + site + @""",
        ""OperationalDepartment"":""SVEN"",
        ""DockingZone"":""BLOK DB"",
        ""ContainerLocation"":""1000"",
        ""ContainerStackLocation"":""1001"",
        ""EorderPriority"":""KTN"",
        ""Dispatch"":{
                        ""LoadingDock"":null,
            ""DispatchPriority"":""90"",
            ""DispatchedTo"":null,
            ""DispatchComment"":null
        },
        ""Zone"":""" + zone + @""",
        ""ProductOverview"":""HE3490LS"",
        ""LotbatchOverview"":""3010171""
    }
}";

            return result;
        }
        public static async Task<CreateWorkOrderResult> CreateWorkOrder(string userCreated)
        {
            using (var client = ControllerTestsHelper.GetClient())
            {
                var apiVersion = "1.0";

                var body = new CreateWorkOrderFromBody()
                {
                    Operation = "anyOperation",
                    Department = "any",
                    Site = "any",
                    UserCreated = userCreated
                };

                var jsonBody = JsonConvert.SerializeObject(body);

                var response = await client.PostAsync(string.Format("api/workorders?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));

                var id = response.Headers.Location.GetId();
                var eTag = response.Headers.ETag.Tag;
                var result = new CreateWorkOrderResult(id, eTag);
                return await Task.FromResult(result);
            }
        }

        public static async Task<CreateWorkOrderResult> CreateWorkOrderThroughEcc(Guid id, Order order)
        {
            var workOrder = RepositoryHelper.CreateWorkOrder(id, order);

            var result = new CreateWorkOrderResult(workOrder.Id, "\"" + workOrder.Version.ToString() + "\"");

            return await Task.FromResult(result);
        }

        public static async Task<CreateWorkOrderResult> CreateWorkOrderThroughPlato(string source, string site, string zone)
        {
            using (var client = ControllerTestsHelper.GetClient())
            {
                var apiVersion = "1.0";

                var jsonBody = GetPlatoOverviewJsonAsString(source, site, zone);
                var response = await client.PostAsync(string.Format("api/workorders/create?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));

                var id = response.Headers.Location.GetId();
                var eTag = response.Headers.ETag.Tag;
                var result = new CreateWorkOrderResult(id, eTag);
                return await Task.FromResult(result);
            }
        }
    }
}
