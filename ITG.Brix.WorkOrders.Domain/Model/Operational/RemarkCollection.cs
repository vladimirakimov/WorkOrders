using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ITG.Brix.WorkOrders.Domain
{
    public sealed class RemarkCollection
    {
        private readonly IList<Remark> _remarks;

        public RemarkCollection()
        {
            _remarks = new List<Remark>();
        }

        public IReadOnlyCollection<Remark> AsReadOnly()
        {
            IReadOnlyCollection<Remark> result = new ReadOnlyCollection<Remark>(_remarks);
            return result;
        }

        public void Add(Remark remark)
        {
            if (!_remarks.Contains(remark))
            {
                _remarks.Add(remark);
            }
        }

        public void Remove(Remark remark)
        {
            if (_remarks.Contains(remark))
            {
                _remarks.Remove(remark);
            }
        }

        public void Clear()
        {
            _remarks.Clear();
        }
    }
}
