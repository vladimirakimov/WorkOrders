using ITG.Brix.WorkOrders.Application.Exceptions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Services.Acls.Impl
{
    public class PlatoDataAcl : IPlatoDataAcl
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public PlatoDataAcl(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw Error.ArgumentNull(nameof(dateTimeProvider));
        }


        private Dictionary<string, Guid> codeToIdMap = new Dictionary<string, Guid>();
        public string Location(string location)
        {
            var result = string.IsNullOrWhiteSpace(location) ? Label.UnsetValue : location.Trim();
            return result;
        }

        public Guid ProductId(string productCode)
        {
            if (!codeToIdMap.ContainsKey(productCode))
            {
                var id = Guid.NewGuid();
                codeToIdMap.Add(productCode, id);
            }
            var result = codeToIdMap[productCode];

            return result;
        }

        public DateOn DateOnOrNull(string utc)
        {
            if (string.IsNullOrWhiteSpace(utc))
            {
                return null;
            }
            DateTime dateTimeUtc;
            try
            {
                dateTimeUtc = _dateTimeProvider.ParseUtc(utc);
            }
            catch
            {
                dateTimeUtc = DateTime.UtcNow;
            }

            var result = new DateOn(dateTimeUtc);

            return result;
        }

        public bool IsConvertibleToUtcOrNull(string utcOrEmpty)
        {
            if (string.IsNullOrWhiteSpace(utcOrEmpty))
            {
                return true;
            }

            var result = _dateTimeProvider.CanParseUtc(utcOrEmpty);

            return result;
        }
    }
}
