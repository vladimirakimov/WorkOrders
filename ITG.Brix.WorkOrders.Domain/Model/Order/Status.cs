using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Status
    {
        public static readonly Status Open = new Status(1, "Open");
        public static readonly Status Busy = new Status(2, "Busy");
        public static readonly Status Blocked = new Status(3, "Blocked");
        public static readonly Status Finished = new Status(4, "Finished");
        public static readonly Status Closed = new Status(5, "Closed");

        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Status() { }

        public Status(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<Status> List()
        {
            return new[] { Open, Busy, Blocked, Finished, Closed };
        }

        public static Status Parse(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for Status: {string.Join(", ", List().Select(s => s.Name))}");
            }

            return state;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Status;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
