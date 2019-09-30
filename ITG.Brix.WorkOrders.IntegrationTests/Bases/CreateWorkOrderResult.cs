using System;

namespace ITG.Brix.WorkOrders.IntegrationTests.Bases
{
    public class CreateWorkOrderResult
    {
        public CreateWorkOrderResult(Guid id, string eTag)
        {
            Id = id;
            ETag = eTag;
        }

        public Guid Id { get; private set; }
        public string ETag { get; private set; }
    }
}
