using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ITG.Brix.WorkOrders.Domain
{
    public sealed class PictureCollection
    {
        private readonly IList<Picture> _pictures;

        public PictureCollection()
        {
            _pictures = new List<Picture>();
        }

        public IReadOnlyCollection<Picture> AsReadOnly()
        {
            IReadOnlyCollection<Picture> result = new ReadOnlyCollection<Picture>(_pictures);
            return result;
        }

        public void Add(Picture picture)
        {
            if (!_pictures.Contains(picture))
            {
                _pictures.Add(picture);
            }
        }

        public void Remove(Picture picture)
        {
            if (_pictures.Contains(picture))
            {
                _pictures.Remove(picture);
            }
        }

        public void Clear()
        {
            _pictures.Clear();
        }
    }
}
