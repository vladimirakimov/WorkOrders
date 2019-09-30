using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Infrastructure.Diagnostics;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using System;

namespace ITG.Brix.WorkOrders.Infrastructure.RestApis.Configurations.Impl
{
    public class BiztalkContext : IBiztalkContext
    {
        private readonly IBiztalkConfiguration _biztalkConfiguration;

        public BiztalkContext(IBiztalkConfiguration biztalkConfiguration)
        {
            Guard.On(biztalkConfiguration, Error.ArgumentNull(nameof(biztalkConfiguration))).AgainstNull();

            _biztalkConfiguration = biztalkConfiguration;
        }

        public Uri Uri
        {
            get
            {
                var result = new Uri(string.Format("{0}{1}", _biztalkConfiguration.Host.TrimEnd('/'), "/ECC/BTSHTTPReceive.dll"));
                return result;
            }
        }
    }
}
