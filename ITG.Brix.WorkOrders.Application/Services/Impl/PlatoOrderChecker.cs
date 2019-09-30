using ITG.Brix.WorkOrders.Application.Exceptions;
using ITG.Brix.WorkOrders.Application.Services.Acls;
using ITG.Brix.WorkOrders.Domain;
using System;

namespace ITG.Brix.WorkOrders.Application.Services.Impl
{
    public class PlatoOrderChecker : IPlatoOrderChecker
    {
        private readonly IPlatoDataAcl _platoDataAcl;

        public PlatoOrderChecker(IPlatoDataAcl platoDataAcl)
        {
            _platoDataAcl = platoDataAcl ?? throw Error.ArgumentNull(nameof(platoDataAcl));
        }

        public void Check(PlatoOrderOverview platoOrderOverview)
        {
            if (!_platoDataAcl.IsConvertibleToUtcOrNull(platoOrderOverview.DocumentDate))
            {
                var message = string.Format("Plato overview with:{0}[source:\"{1}\", relationType:\"{2}\", transportNo:\"{3}\", operation:\"{4}\"]{5}has invalid value \"{6}\" for key \"{7}\"",
                    Environment.NewLine,
                    platoOrderOverview.Source,
                    platoOrderOverview.RelationType,
                    platoOrderOverview.ID,
                    platoOrderOverview.Operation,
                    Environment.NewLine,
                    platoOrderOverview.DocumentDate,
                    nameof(platoOrderOverview.DocumentDate)
                    );
                throw Error.PlatoOrderOverviewCheck(message);
            }
        }
    }
}
