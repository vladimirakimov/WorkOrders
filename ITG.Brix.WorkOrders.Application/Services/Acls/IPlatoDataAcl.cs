using ITG.Brix.WorkOrders.Domain;
using System;

namespace ITG.Brix.WorkOrders.Application.Services.Acls
{
    public interface IPlatoDataAcl
    {
        string Location(string location);
        Guid ProductId(string productCode);
        DateOn DateOnOrNull(string utc);
        bool IsConvertibleToUtcOrNull(string utcOrEmpty);
    }
}
