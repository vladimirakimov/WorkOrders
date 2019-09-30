using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Bases;
using ITG.Brix.WorkOrders.API.Context.Constants;
using ITG.Brix.WorkOrders.API.Context.Resources;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using ITG.Brix.WorkOrders.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Models;
using ITG.Brix.WorkOrders.Application.Resources;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.IntegrationTests.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.IntegrationTests.Controllers
{
    [TestClass]
    [TestCategory("Integration")]
    public class WorkOrdersControllerTests
    {
        private HttpClient _client;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            ControllerTestsHelper.InitServer();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseHelper.Init("WorkOrders");
            _client = ControllerTestsHelper.GetClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _client.Dispose();
        }

        #region List
        [TestMethod]
        public async Task ListAllShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";

            await ControllerHelper.CreateWorkOrder("anyUser");

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}", apiVersion));
            var responseBody = await response.Content.ReadAsStringAsync();
            var workOrdersModel = JsonConvert.DeserializeObject<WorkOrdersModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            workOrdersModel.Value.Should().NotBeNull();
            workOrdersModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("userCreated eq 'anyUser'", 0)]
        [DataRow("userCreated ne 'AnyUser'", 26)]
        [DataRow("startswith(userCreated, 'any') eq true", 26)]
        [DataRow("startswith(userCreated, 'Hello') eq false", 26)]
        [DataRow("startswith(userCreated, 'any') eq true and endswith(userCreated, 'z') eq true", 1)]
        [DataRow("endswith(userCreated, 'z') eq true", 1)]
        [DataRow("endswith(userCreated, 'z') eq false", 25)]
        [DataRow("substringof('User', userCreated) eq true", 26)]
        [DataRow("tolower(userCreated) eq 'anyusera'", 1)]
        [DataRow("toupper(userCreated) eq 'ANYUSERA'", 1)]
        public async Task ListWithFilterShouldSucceed(string filter, int countResult)
        {
            // Arrange
            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateWorkOrder("anyUser" + c);
            }

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$filter={1}", apiVersion, filter));
            var responseBody = await response.Content.ReadAsStringAsync();
            var workOrdersModel = JsonConvert.DeserializeObject<WorkOrdersModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            workOrdersModel.Value.Should().HaveCount(countResult);
            workOrdersModel.Count.Should().Be(countResult);
            workOrdersModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("(source eq 'BKAL33+KBT T') and (site eq 'WVN136' or site eq 'LB1227')", "BKAL33+KBT T", "LB1227", "UNKNOWN", 1)]
        [DataRow("(source eq 'BKAL33+KBT T') and (site eq 'WVN136' or site eq 'LB1227') and (zone eq 'NONE')", "BKAL33+KBT T", "LB1227", "UNKNOWN", 0)]
        [DataRow("(source eq 'BKAL33+KBT T') and (site eq 'WVN136' or site eq 'LB1227') and (zone eq 'NONE' or zone eq 'UNKNOWN')", "BKAL33+KBT T", "LB1227", "UNKNOWN", 1)]
        [DataRow("(source eq 'BKAL33+KBT T') and (site eq 'WVN136' or site eq 'LB1227') and (zone eq 'UNKNOWN')", "BKAL33+KBT T", "LB1227", "UNKNOWN", 1)]
        public async Task ListWithCompoundFilterShouldSucceed(string filter, string source, string site, string zone, int countResult)
        {
            // Arrange
            filter = Uri.EscapeDataString(filter);
            var apiVersion = "1.0";

            await ControllerHelper.CreateWorkOrderThroughPlato(source, site, zone);


            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$filter={1}", apiVersion, filter));
            var responseBody = await response.Content.ReadAsStringAsync();
            var workOrdersModel = JsonConvert.DeserializeObject<WorkOrdersModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            workOrdersModel.Value.Should().HaveCount(countResult);
            workOrdersModel.Count.Should().Be(countResult);
            workOrdersModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow(0, 10, 10)]
        [DataRow(1, 10, 10)]
        [DataRow(10, 100, 16)]
        public async Task ListWithSkipAndTopShouldSucceed(int skip, int top, int countResult)
        {
            // Arrange
            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateWorkOrder("anyUser" + c);
            }

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$skip={1}&$top={2}", apiVersion, skip, top));
            var responseBody = await response.Content.ReadAsStringAsync();
            var workOrdersModel = JsonConvert.DeserializeObject<WorkOrdersModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            workOrdersModel.Value.Should().HaveCount(countResult);
            workOrdersModel.Count.Should().Be(countResult);
            workOrdersModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("userCreated ne 'AnyUsera'", 0, 10, 10)]
        [DataRow("userCreated ne 'AnyUsera'", 0, 30, 26)]
        [DataRow("userCreated ne 'AnyUsera'", 0, 10, 10)]
        [DataRow("userCreated ne 'AnyUsera'", 20, 10, 6)]
        public async Task ListWithFilterSkipTopShouldSucceed(string filter, int skip, int top, int countResult)
        {
            // Arrange
            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateWorkOrder("anyUser" + c);
            }


            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$filter={1}&$skip={2}&$top={3}", apiVersion, filter, skip, top));
            var responseBody = await response.Content.ReadAsStringAsync();
            var workOrdersModel = JsonConvert.DeserializeObject<WorkOrdersModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            workOrdersModel.Value.Should().HaveCount(countResult);
            workOrdersModel.Count.Should().Be(countResult);
            workOrdersModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("any eq 'anyUser'")]
        [DataRow("any")]
        public async Task ListShouldFailWhenQueryFilterIsNotValid(string filter)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.InvalidQueryFilter,
                            Message = HandlerFailures.InvalidQueryFilter,
                            Target = Consts.Failure.Detail.Target.QueryFilter
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$filter={1}", apiVersion, filter));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("concat(concat(anyUser, ', '), anyOperation) eq 'AnyUser, AnyOperation'")]
        [DataRow("length(anyUser) eq 7")]
        [DataRow("replace(anyUser, ' ', '') eq 'AnyUser'")]
        [DataRow("trim(anyUser) eq 'AnyUser'")]
        public async Task ListShouldFailWhenQueryFilterHasUnsupportedFunctions(string filter)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.InvalidQueryFilter,
                            Message = HandlerFailures.InvalidQueryFilter,
                            Target = Consts.Failure.Detail.Target.QueryFilter
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$filter={1}", apiVersion, filter));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("    ")]
        public async Task ListShouldSucceedWhenQueryTopPresentButUnset(string top)
        {
            // Arrange
            var apiVersion = "1.0";

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$top={1}", apiVersion, top));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [DataTestMethod]
        [DataRow("some invalid value - not a sequence of digits")]
        [DataRow("null")]
        [DataRow("''")]
        [DataRow("'   '")]
        public async Task ListShouldFailWhenQueryTopIsNotValid(string top)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQueryTop,
                                    Message = CustomFailures.TopInvalid,
                                    Target = Consts.Failure.Detail.Target.QueryTop
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$top={1}", apiVersion, top));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("0")]
        [DataRow("-1")]
        [DataRow("99999999999999999999999")]
        public async Task ListShouldFailWhenQueryTopNotInRange(string top)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQueryTop,
                                    Message = string.Format(CustomFailures.TopRange, Application.Constants.Consts.CqsValidation.TopMaxValue),
                                    Target = Consts.Failure.Detail.Target.QueryTop
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$top={1}", apiVersion, top));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("    ")]
        public async Task ListShouldSucceedWhenQuerySkipPresentButUnset(string skip)
        {
            // Arrange
            var apiVersion = "1.0";

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$skip={1}", apiVersion, skip));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [DataTestMethod]
        [DataRow("some invalid value - not a sequence of digits")]
        [DataRow("null")]
        [DataRow("''")]
        [DataRow("'   '")]
        public async Task ListShouldFailWhenQuerySkipIsNotValid(string skip)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQuerySkip,
                                    Message = CustomFailures.SkipInvalid,
                                    Target = Consts.Failure.Detail.Target.QuerySkip
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$skip={1}", apiVersion, skip));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("-1")]
        [DataRow("99999999999999999999999")]
        public async Task ListShouldFailWhenQuerySkipNotInRange(string skip)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQuerySkip,
                                    Message = string.Format(CustomFailures.SkipRange, Application.Constants.Consts.CqsValidation.SkipMaxValue),
                                    Target = Consts.Failure.Detail.Target.QuerySkip
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}&$skip={1}", apiVersion, skip));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task ListShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync("api/workorders");
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task ListShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders?api-version={0}", apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }
        #endregion

        #region Get
        [TestMethod]
        [Ignore("Biztalk: supress-try")]
        public async Task GetShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";

            var createWorkOrderResult = await ControllerHelper.CreateWorkOrder("anyUser");
            var routeId = createWorkOrderResult.Id;
            var eTag = createWorkOrderResult.ETag;

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var responseBody = await response.Content.ReadAsStringAsync();
            var workOrderModel = JsonConvert.DeserializeObject<WorkOrderModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.ETag.Should().NotBeNull();
            response.Headers.ETag.Tag.Should().Be(eTag);
            workOrderModel.UserCreated.Should().Be("anyUser");
        }

        [TestMethod]
        public async Task GetShouldFailWhenRouteIdIsInvalid()
        {
            // Arrange
            var routeId = "someInvalidId";

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = RequestFailures.EntityNotFoundByIdentifier,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders/{0}", routeId));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenRouteIdIsInvalidWithApiVersion()
        {
            // Arrange
            var apiVersion = "1.0";
            var routeId = "someInvalidId";

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = RequestFailures.EntityNotFoundByIdentifier,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        [Ignore("Biztalk: supress-try")]
        public async Task GetShouldFailWhenRouteIdDoesNotExist()
        {
            // Arrange
            var apiVersion = "1.0";
            var routeId = Guid.NewGuid();

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = string.Format(HandlerFailures.NotFound, "WorkOrder"),
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenRouteIdIsEmptyGuid()
        {
            // Arrange
            var apiVersion = "1.0";
            var routeId = Guid.Empty.ToString();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = CustomFailures.WorkOrderNotFound,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders/{0}", routeId));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";
            var routeId = Guid.NewGuid().ToString();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }
        #endregion

        #region Create

        [TestMethod]
        public async Task CreatePlatoOrderShouldSucceed()
        {
            // Arrange
            var source = "BKAL33+KBT T";
            var site = "LB1227";
            var zone = "UNKNOWN";
            var jsonBody = ControllerHelper.GetPlatoOverviewJsonAsString(source, site, zone);

            // Act
            var response = await _client.PostAsync("api/workOrders/create", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            responseBody.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";
            var body = new CreateWorkOrderFromBody()
            {
                UserCreated = "anyUser",
                Operation = "anyOperation",
                Department = "any",
                Site = "any"
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync(string.Format("api/workOrders?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            response.Headers.ETag.Should().NotBeNull();
            responseBody.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task CreateShouldFailWhenBodyIsNonJsonContentType()
        {
            // Arrange
            var apiVersion = "1.0";
            var body = new CreateWorkOrderFromBody()
            {
                Operation = "anyOperation",
                Department = "any",
                Site = "any"
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync(string.Format("api/workorders?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "text/plain"));
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
            responseBody.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task CreateShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var body = new CreateWorkOrderFromBody()
            {
                Operation = "anyOperation",
                Department = "any",
                Site = "any"
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync("api/workorders", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";

            var body = new CreateWorkOrderFromBody()
            {
                Operation = "anyOperation",
                Department = "any",
                Site = "any"
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/workorders?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }
        #endregion

        #region Update

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";
            var createWorkOrderResult = await ControllerHelper.CreateWorkOrder("anyUser");
            var routeId = createWorkOrderResult.Id;
            var ifmatch = createWorkOrderResult.ETag;
            var jsonInString = @"{
                                    ""status"" : ""open"",
                                    ""startedOn"": ""2019-04-19T12:15:57Z""
                                 }";

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            response.Headers.ETag.Should().NotBeNull();
            body.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task UpdateHandledUnitsShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";
            var workOrderId = Guid.NewGuid();
            var order = new Order();
            var unitId = Guid.NewGuid();
            var unit = new Unit(unitId, UnitType.Single);
            var location = new Location(
                                new Warehouse(new Label("Warehouse")),
                                new Gate(new Label("Gate")),
                                new Row(new Label("Row")),
                                new Position(new Label("Position"))
                            );
            unit.SetLocation(location);
            unit.SetGroup(new Group(
                            key: "groupKey",
                            weightNet: new Weight(12),
                            weightGross: new Weight(14)
                            )
            );
            unit.SetMixed(new Mixed(key: null, palletNumber: null));
            unit.SetUnits(new Units(1));
            unit.SetIsPartial(false);
            unit.SetQuantity(new Quantity(55));
            unit.SetWeightNet(new Weight(13));
            unit.SetWeightGross(new Weight(15));
            unit.SetPalletNumber("PalletNumber");
            unit.SetSsccNumber("SsccNumber");

            order.Units = new List<Unit>() { unit };

            var createWorkOrderResult = await ControllerHelper.CreateWorkOrderThroughEcc(workOrderId, order);
            var routeId = createWorkOrderResult.Id;
            var ifmatch = createWorkOrderResult.ETag;
            var body = new
            {
                HandledUnits = new List<object>(){
                    new {
                            Id = Guid.NewGuid(),
                            SourceUnitId = unitId,
                            OperantId = Guid.NewGuid(),
                            OperantLogin = "OperantLogin",
                            HandledOn = "2019-05-16T06:48:50Z",

                            Warehouse = "Warehouse",
                            Gate = "Gate",
                            Row = "Row",
                            Position = "Position",

                            Units = "1",
                            IsPartial = "false",
                            IsMixed = "true",
                            Quantity = "55",

                            WeightNet = "12.00",
                            WeightGross = "14.00",
                            PalletNumber = "PalletNumber",
                            SsccNumber = "SsccNumber",

                            Products = new List<object>()
                            {
                                new
                                {
                                     Id = Guid.NewGuid(),
                                     ConfigurationCode = "",
                                     ConfigurationDescription = "",
                                     ConfigurationQuantity = "",
                                     ConfigurationUnitType = "",
                                     ConfigurationNetPerUnit = "",
                                     ConfigurationNetPerUnitAlwaysDifferent = "",
                                     ConfigurationGrossPerUnit = "",

                                     Code = "",
                                     Customer = "",
                                     Arrival = "",
                                     Article = "",
                                     ArticlePackagingCode = "",
                                     Name = "",
                                     Gtin = "",
                                     ProductType = "",
                                     MaterialType = "",
                                     Color = "",
                                     Shape = "",
                                     Lotbatch = "",
                                     Lotbatch2 = "",
                                     ClientReference = "",
                                     ClientReference2 = "",
                                     BestBeforeDate = "2019-01-30T06:48:50Z",
                                     DateFifo = (string)null,
                                     CustomsDocument = "",
                                     StorageStatus = "",
                                     Stackheight = "",
                                     Length = "",
                                     Width = "",
                                     Height = "",
                                     OriginalContainer = "",
                                     Quantity = "55",
                                     WeightNet = "12.00",
                                     WeightGross = "14.00"
                                },
                                new
                                {
                                     Id = Guid.NewGuid(),
                                     ConfigurationCode = "",
                                     ConfigurationDescription = "",
                                     ConfigurationQuantity = "",
                                     ConfigurationUnitType = "",
                                     ConfigurationNetPerUnit = "",
                                     ConfigurationNetPerUnitAlwaysDifferent = "",
                                     ConfigurationGrossPerUnit = "",

                                     Code = "",
                                     Customer = "",
                                     Arrival = "",
                                     Article = "",
                                     ArticlePackagingCode = "",
                                     Name = "",
                                     Gtin = "",
                                     ProductType = "",
                                     MaterialType = "",
                                     Color = "",
                                     Shape = "",
                                     Lotbatch = "",
                                     Lotbatch2 = "",
                                     ClientReference = "",
                                     ClientReference2 = "",
                                     BestBeforeDate = (string)null,
                                     DateFifo = "2019-01-30T06:48:50Z",
                                     CustomsDocument = "",
                                     StorageStatus = "",
                                     Stackheight = "",
                                     Length = "",
                                     Width = "",
                                     Height = "",
                                     OriginalContainer = "",
                                     Quantity = "55",
                                     WeightNet = "13.00",
                                     WeightGross = "15.00"
                                }
                            }
                    }
                }
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            responseBody.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task UpdateRemarksShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";
            var createWorkOrderResult = await ControllerHelper.CreateWorkOrder("anyUser");
            var routeId = createWorkOrderResult.Id;
            var ifmatch = createWorkOrderResult.ETag;
            var body = new
            {
                Remarks = new List<object>(){
                    new {
                        OperantId = Guid.NewGuid(),
                        Operant = "Operant",
                        CreatedOn = "2019-03-28T06:54:51Z",
                        Text = "text"
                    }
                }
            };
            var jsonBody = JsonConvert.SerializeObject(body);


            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        //[TestMethod]
        //public async Task UpdateShouldFailWhenBodyIsNonJsonContentType()
        //{
        //    // Arrange
        //    var apiVersion = "1.0";
        //    var createWorkOrderResult = await ControllerHelper.CreateWorkOrder("anyUser");
        //    var routeId = createWorkOrderResult.Id;
        //    var ifmatch = createWorkOrderResult.ETag;
        //    var updateWorkOrder = new UpdateWorkOrderFromBody
        //    {
        //        CreatedOn = DateTime.Now.ToString(),
        //        IsEditable = true,
        //        Operational = new RequestOperationalDto()
        //        {
        //            ExtraInformation = "any",
        //            Operant = "any",
        //            Status = "Open",
        //            StartOperationDate = DateTime.Now.ToString(),
        //            StartOperationTime = DateTime.Now.ToString(),
        //            StopOperationDate = DateTime.Now.ToString(),
        //            StopOperationTime = DateTime.Now.ToString()
        //        },
        //        Order = new RequestOrderDto()
        //        {
        //            Container = "any",
        //            ContainerLocation = "any",
        //            Customer = "any",
        //            DockingZone = "any",
        //            LicensePlateTrailer = "any",
        //            LicensePlateTruck = "any",
        //            OperationalDepartment = "any",
        //            Site = "any"
        //        },
        //        TimeCreated = DateTime.Now.ToString(),
        //        UserCreated = "anyUser"
        //    };
        //    var jsonString = JsonConvert.SerializeObject(updateWorkOrder);


        //    var expectedResponseError = new ResponseError
        //    {
        //        Error = new ResponseErrorBody
        //        {
        //            Code = ServiceError.UnsupportedMediaType.Code,
        //            Message = ServiceError.UnsupportedMediaType.Message,
        //            Details = new List<ResponseErrorField>
        //            {
        //                new ResponseErrorField
        //                {
        //                    Code = Consts.Failure.Detail.Code.Unsupported,
        //                    Message = string.Format(RequestFailures.HeaderUnsupportedValue, "Content-Type"),
        //                    Target = Consts.Failure.Detail.Target.ContentType
        //                }
        //            }
        //        }
        //    };

        //    // Act
        //    _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
        //    var response = await _client.PutAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonString, Encoding.UTF8, "text/plain"));
        //    var body = await response.Content.ReadAsStringAsync();
        //    var error = JsonConvert.DeserializeObject<ResponseError>(body);

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
        //    body.Should().NotBeNull();
        //    error.Should().Be(expectedResponseError);
        //}

        #endregion

        #region Delete

        [TestMethod]
        public async Task DeleteShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";
            var createWorkOrderResult = await ControllerHelper.CreateWorkOrder("anyUser");
            var routeId = createWorkOrderResult.Id;
            var ifmatch = createWorkOrderResult.ETag;

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            body.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenRouteIdIsInvalid()
        {
            // Arrange
            var apiVersion = "1.0";
            var createWorkOrderResult = await ControllerHelper.CreateWorkOrder("anyUser");
            var routeId = "someInvalidRouteId";
            var ifmatch = createWorkOrderResult.ETag;

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = RequestFailures.EntityNotFoundByIdentifier,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenRouteIdIsEmptyGuid()
        {
            // Arrange
            var apiVersion = "1.0";
            var createWorkOrderResult = await ControllerHelper.CreateWorkOrder("anyUser");
            var routeId = Guid.Empty;
            var ifmatch = createWorkOrderResult.ETag;


            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = CustomFailures.WorkOrderNotFound,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenRouteIdDoesNotExist()
        {
            // Arrange
            var apiVersion = "1.0";
            var createWorkOrderResult = await ControllerHelper.CreateWorkOrder("anyUser");
            var routeId = Guid.NewGuid();
            var ifmatch = createWorkOrderResult.ETag;

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = HandlerFaultCode.NotFound.Name,
                            Message = string.Format(HandlerFailures.NotFound, "WorkOrder"),
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.DeleteAsync(string.Format("api/workorders/{0}", routeId));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";
            var routeId = Guid.NewGuid().ToString();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.DeleteAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenHeaderIfMatchIsMissing()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();
            var apiVersion = "1.0";


            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredHeader.Code,
                    Message = ServiceError.MissingRequiredHeader.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.HeaderRequired,"If-Match"),
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            var response = await _client.DeleteAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenHeaderIfMatchIsInvalid()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();
            var apiVersion = "1.0";
            var ifmatch = "\" \"";

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidHeaderValue.Code,
                    Message = ServiceError.InvalidHeaderValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.HeaderInvalidValue, "If-Match"),
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenHeaderIfMatchIsWrong()
        {
            // Arrange
            var createWorkOrderResult = await ControllerHelper.CreateWorkOrder("anyUser");
            var routeId = createWorkOrderResult.Id;
            var ifmatch = "\"8001\"";

            var apiVersion = "1.0";

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ConditionNotMet.Code,
                    Message = ServiceError.ConditionNotMet.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = HandlerFaultCode.NotMet.Name,
                            Message = HandlerFailures.NotMet,
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/workorders/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.PreconditionFailed);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        #endregion
    }
}
