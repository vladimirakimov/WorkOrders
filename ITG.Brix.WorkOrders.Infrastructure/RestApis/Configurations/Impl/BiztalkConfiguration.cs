using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Infrastructure.Diagnostics;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;

namespace ITG.Brix.WorkOrders.Infrastructure.RestApis.Configurations.Impl
{
    public class BiztalkConfiguration : IBiztalkConfiguration
    {
        private readonly string _host;

        public BiztalkConfiguration(string host)
        {
            Guard.On(host, Error.ArgumentNull(nameof(host))).AgainstNull();

            _host = host;
        }

        public string Host => _host;
    }
}
