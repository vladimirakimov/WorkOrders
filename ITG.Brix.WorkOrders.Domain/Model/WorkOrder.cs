using ITG.Brix.WorkOrders.Domain.Exceptions;
using System;

namespace ITG.Brix.WorkOrders.Domain
{
    public class WorkOrder : Entity, IAggregateRoot
    {
        public bool IsEditable { get; set; }
        public Order Order { get; set; }
        public Operational Operational { get; set; }
        public string UserCreated { get; set; }
        public CreatedOn CreatedOn { get; set; }
        public int Version { get; set; }

        public WorkOrder(Guid id) : base(id) { }
        public WorkOrder(Guid id,
                         bool isEditable,
                         Order order,
                         Operational operational,
                         string userCreated,
                         CreatedOn createdOn) : base(id)
        {
            if (order == null)
            {
                throw Error.ArgumentNull(string.Format("{0} can't be null", nameof(order)));
            }

            IsEditable = isEditable;
            Order = order;
            Operational = operational;
            UserCreated = userCreated;
            CreatedOn = createdOn;
        }

        public class Builder
        {
            private Guid _id;
            private bool _isEditable;
            private Order _order;
            private Operational _operational;
            private string _userCreated;
            private CreatedOn _createdOn;

            public Builder WithId(Guid value)
            {
                _id = value;
                return this;
            }

            public Builder WithIsEditable(bool value)
            {
                _isEditable = value;
                return this;
            }

            public Builder WithOrder(Order value)
            {
                _order = value;
                return this;
            }

            public Builder WithOperational(Operational value)
            {
                _operational = value;
                return this;
            }

            public Builder WithUserCreated(string value)
            {
                _userCreated = value;
                return this;
            }

            public Builder WithCreatedOn(CreatedOn value)
            {
                _createdOn = value;
                return this;
            }

            public WorkOrder Build()
            {
                return new WorkOrder(id: _id,
                                     isEditable: _isEditable,
                                     order: _order,
                                     operational: _operational,
                                     userCreated: _userCreated,
                                     createdOn: _createdOn);
            }
        }
    }
}
