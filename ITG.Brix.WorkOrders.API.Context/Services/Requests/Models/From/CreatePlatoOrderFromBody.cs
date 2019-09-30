using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From
{
    public class CreatePlatoOrderFromBody
    {
        [DataMember]
        public string OrderContent { get; set; }
    }
}
