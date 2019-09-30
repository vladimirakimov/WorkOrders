using System;

namespace ITG.Brix.WorkOrders.Domain
{
    [Flags]
    public enum GeneralGroup
    {
        None = 0x0,
        Identification = 0x1,
        Overview = 0x2,
        Execution = 0x4
    }
}
