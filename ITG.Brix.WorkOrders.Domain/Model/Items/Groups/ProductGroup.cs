using System;

namespace ITG.Brix.WorkOrders.Domain
{
    [Flags]
    public enum ProductGroup
    {
        None = 0x0,
        On = 0x1
    }
}
