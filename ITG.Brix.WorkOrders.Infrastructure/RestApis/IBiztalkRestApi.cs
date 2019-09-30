using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Infrastructure.RestApis
{
    public interface IBiztalkRestApi
    {
        Task<string> GetOrder(string jsonBody);
    }
}
