using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ITG.Brix.WorkOrders.Domain.Model.Operational
{
    public class InputCollection
    {
        private readonly IList<Input> _inputs;

        public InputCollection()
        {
            _inputs = new List<Input>();
        }

        public IReadOnlyCollection<Input> AsReadOnly()
        {
            IReadOnlyCollection<Input> result = new ReadOnlyCollection<Input>(_inputs);
            return result;
        }

        public void Add(Input input)
        {
            if (!_inputs.Contains(input))
            {
                _inputs.Add(input);
            }
        }

        public void Remove(Input input)
        {
            if (_inputs.Contains(input))
            {
                _inputs.Remove(input);
            }
        }

        public void Clear()
        {
            _inputs.Clear();
        }
    }
}
