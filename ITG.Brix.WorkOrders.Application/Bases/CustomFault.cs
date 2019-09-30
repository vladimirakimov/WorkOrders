using ITG.Brix.WorkOrders.Application.Enums;

namespace ITG.Brix.WorkOrders.Application.Bases
{
    public class CustomFault : Failure
    {
        public ErrorType Type
        {
            get
            {
                return ErrorType.CustomError;
            }
        }
    }
}
