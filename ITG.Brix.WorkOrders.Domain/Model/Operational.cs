using ITG.Brix.WorkOrders.Domain.Model.Operational;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Operational : Entity
    {
        private readonly HandledUnitCollection _handledUnits;
        private readonly RemarkCollection _remarks;
        private readonly PictureCollection _pictures;
        private readonly InputCollection _inputs;

        public string Operant { get; private set; }
        public Status Status { get; private set; }
        public string ExtraInformation { get; private set; }
        public DateOn StartedOn { get; private set; }
        public DateOn StoppedOn { get; private set; }
        public IReadOnlyCollection<HandledUnit> HandledUnits => _handledUnits.AsReadOnly();
        public IReadOnlyCollection<Remark> Remarks => _remarks.AsReadOnly();
        public IReadOnlyCollection<Picture> Pictures => _pictures.AsReadOnly();
        public IReadOnlyCollection<Input> Inputs => _inputs.AsReadOnly();


        public Operational(Status status) : base(Guid.NewGuid())
        {
            Status = status;
            _handledUnits = new HandledUnitCollection();
            _remarks = new RemarkCollection();
            _pictures = new PictureCollection();
            _inputs = new InputCollection();
        }

        public void SetOperant(string operant)
        {
            Operant = operant;
        }

        public void ChangeStatus(Status status)
        {
            Status = status;
        }

        public void SetStartedOn(DateOn startedOn)
        {
            StartedOn = startedOn;
        }

        public void AddHandledUnit(HandledUnit handledUnit)
        {
            _handledUnits.Add(handledUnit);
        }

        public void ClearHandledUnits()
        {
            _handledUnits.Clear();
        }

        public void AddRemark(Remark remark)
        {
            _remarks.Add(remark);
        }

        public void ClearRemarks()
        {
            _remarks.Clear();
        }

        public void AddPicture(Picture picture)
        {
            _pictures.Add(picture);
        }

        public void ClearPictures()
        {
            _pictures.Clear();
        }

        public void AddInput(Input input)
        {
            _inputs.Add(input);
        }

        public void ClearInputs()
        {
            _inputs.Clear();
        }
    }
}
