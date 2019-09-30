using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Models
{
    public class OperationModel
    {
        public PriorityModel Priority { get; set; }
        public DispatchModel Dispatch { get; set; }
        public IEnumerable<ExtraActivityModel> ExtraActivities { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string UnitPlanning { get; set; }
        public string TypePlanning { get; set; }
        public string Site { get; set; }
        public string Zone { get; set; }
        public string OperationalDepartment { get; set; }
        public IEnumerable<string> OperationalRemarks { get; set; }
        public string DockingZone { get; set; }
        public string LoadingDock { get; set; }
        public string ProductOverview { get; set; }
        public string LotbatchOverview { get; set; }
    }
}
