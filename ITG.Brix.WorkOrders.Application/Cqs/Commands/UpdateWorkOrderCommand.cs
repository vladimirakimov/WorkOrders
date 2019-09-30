using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions
{
    public class UpdateWorkOrderCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public Optional<string> Operant { get; private set; }
        public Optional<string> Status { get; private set; }
        public Optional<string> StartedOn { get; private set; }
        public Optional<IEnumerable<HandledUnitDto>> HandledUnits { get; private set; }
        public Optional<IEnumerable<RemarkDto>> Remarks { get; private set; }
        public Optional<IEnumerable<PictureDto>> Pictures { get; private set; }
        public Optional<IEnumerable<InputDto>> Inputs { get; private set; }
        public int Version { get; private set; }

        public UpdateWorkOrderCommand(Guid id,
                                      Optional<string> operant,
                                      Optional<string> status,
                                      Optional<string> startedOn,
                                      Optional<IEnumerable<HandledUnitDto>> handledUnits,
                                      Optional<IEnumerable<RemarkDto>> remarks,
                                      Optional<IEnumerable<PictureDto>> pictures,
                                      Optional<IEnumerable<InputDto>> inputs,
                                      int version)
        {
            Id = id;
            Operant = operant;
            Status = status;
            StartedOn = startedOn;
            HandledUnits = handledUnits;
            Remarks = remarks;
            Pictures = pictures;
            Inputs = inputs;
            Version = version;
        }
    }
}
