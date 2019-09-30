using System;
using System.Collections.Generic;
using System.Text;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels
{
    public class InputClass
    {
        public Guid OperantId { get; set; }
        public string OperantLogin { get; set; }
        public long CreatedOn { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
    }
}
