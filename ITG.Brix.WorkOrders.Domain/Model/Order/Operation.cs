using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Operation
    {
        public Operation()
        {
            Type = OperationType.Inbound;

            Priority = new Priority();
            Dispatch = new Dispatch();
            ExtraActivities = new List<ExtraActivity>();
            OperationalRemarks = new List<string>();
        }

        public Priority Priority { get; set; }
        public Dispatch Dispatch { get; set; }
        public IEnumerable<ExtraActivity> ExtraActivities { get; set; }
        public OperationType Type { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
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
