using ITG.Brix.WorkOrders.API.Context.Providers;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.Application.Cqs.Commands;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions;
using ITG.Brix.WorkOrders.Application.DataTypes;
using ITG.Brix.WorkOrders.Application.Extensions;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Mappers.Impl
{
    public class CqsMapper : ICqsMapper
    {
        private readonly IJsonProvider _jsonProvider;

        public CqsMapper(IJsonProvider jsonProvider)
        {
            _jsonProvider = jsonProvider ?? throw new ArgumentNullException(nameof(jsonProvider));
        }

        public ListWorkOrderQuery Map(ListWorkOrderRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListWorkOrderQuery(filter, top, skip);
            return result;
        }

        public GetWorkOrderQuery Map(GetWorkOrderRequest request)
        {
            var id = new Guid(request.RouteId);

            var result = new GetWorkOrderQuery(id);
            return result;
        }

        public DeleteWorkOrderCommand Map(DeleteWorkOrderRequest request)
        {
            var id = new Guid(request.RouteId);

            var version = ToVersion(request.HeaderIfMatch);

            var result = new DeleteWorkOrderCommand(id, version);
            return result;
        }

        public CreateWorkOrderCommand Map(CreateWorkOrderRequest request)
        {
            var result = new CreateWorkOrderCommand(request.BodyUserCreated, request.BodySite, request.BodyOperation, request.BodyDepartment);
            return result;
        }

        public UpdateWorkOrderCommand Map(UpdateWorkOrderRequest request)
        {
            var id = new Guid(request.RouteId);

            var valuePairs = _jsonProvider.ToDictionary(request.BodyPatch);

            Optional<string> operant = valuePairs.GetOptional<string>("operant");
            Optional<string> status = valuePairs.GetOptional<string>("status");
            Optional<string> startedOn = valuePairs.GetOptional<string>("startedOn");
            Optional<IEnumerable<HandledUnitDto>> handledUnits = valuePairs.GetOptional<IEnumerable<HandledUnitDto>>("handledUnits");
            Optional<IEnumerable<RemarkDto>> remarks = valuePairs.GetOptional<IEnumerable<RemarkDto>>("remarks");
            Optional<IEnumerable<PictureDto>> pictures = valuePairs.GetOptional<IEnumerable<PictureDto>>("pictures");
            Optional<IEnumerable<InputDto>> inputs = valuePairs.GetOptional<IEnumerable<InputDto>>("inputs");


            var version = ToVersion(request.HeaderIfMatch);

            var result = new UpdateWorkOrderCommand(id,
                                                    operant,
                                                    status,
                                                    startedOn,
                                                    handledUnits,
                                                    remarks,
                                                    pictures,
                                                    inputs,
                                                    version);
            return result;
        }

        public ListPropertyQuery Map(ListPropertyRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListPropertyQuery(filter, top, skip);
            return result;
        }

        public ListOrderItemQuery Map(ListOrderItemRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListOrderItemQuery(filter, top, skip);
            return result;
        }

        public ListProductItemQuery Map(ListProductItemRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListProductItemQuery(filter, top, skip);
            return result;
        }

        public CreatePlatoOrderCommand Map(CreatePlatoOrderRequest request)
        {
            var result = new CreatePlatoOrderCommand(request.workOrderXml);
            return result;
        }

        private int ToVersion(string eTag)
        {
            var eTagValue = eTag.Replace("\"", "");
            var result = int.Parse(eTagValue);

            return result;
        }
    }
}
