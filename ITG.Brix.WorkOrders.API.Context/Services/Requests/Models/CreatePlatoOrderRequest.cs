namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models
{
    public class CreatePlatoOrderRequest
    {
        private readonly string _body;
        public CreatePlatoOrderRequest(string body)
        {
            _body = body;
        }

        public string workOrderXml => _body;
    }
}
