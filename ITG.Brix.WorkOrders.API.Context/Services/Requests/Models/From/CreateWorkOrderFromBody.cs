using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From
{
    public class CreateWorkOrderFromBody
    {
        [DataMember]
        public string UserCreated { get; set; }
        [DataMember]
        public string Operation { get; set; }
        [DataMember]
        public string Site { get; set; }
        [DataMember]
        public string Department { get; set; }
    }
}
