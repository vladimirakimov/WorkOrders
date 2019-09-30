using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Models
{
    public class OperationalModel
    {
        public string Operant { get; set; }
        public string Status { get; set; }
        public string ExtraInformation { get; set; }
        public string StartedOn { get; set; }
        public string StoppedOn { get; set; }
        public IEnumerable<HandledUnitModel> Units { get; set; }
        public IEnumerable<RemarkModel> Remarks { get; set; }
        public IEnumerable<PictureModel> Pictures { get; set; }
        public IEnumerable<InputModel> Inputs { get; set; }
    }
}
