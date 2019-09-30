using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Infrastructure.Diagnostics;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using ITG.Brix.WorkOrders.Infrastructure.RestApis.Configurations;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Infrastructure.RestApis.Impl
{
    public class BiztalkRestApi : IBiztalkRestApi
    {
        private readonly HttpClient _httpClient;
        private readonly IBiztalkContext _biztalkContext;

        public BiztalkRestApi(HttpClient httpClient, IBiztalkContext biztalkContext)
        {
            Guard.On(httpClient, Error.ArgumentNull(nameof(httpClient))).AgainstNull();
            Guard.On(biztalkContext, Error.ArgumentNull(nameof(biztalkContext))).AgainstNull();

            _httpClient = httpClient;
            _biztalkContext = biztalkContext;
        }

        public async Task<string> GetOrder(string jsonBody)
        {
            try
            {
                var response = await _httpClient.PostAsync(_biztalkContext.Uri, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception exception)
            {
                throw new BiztalkCallException(exception);
            }
        }
    }
}
