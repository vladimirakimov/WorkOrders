using ITG.Brix.WorkOrders.Application.Enums;

namespace ITG.Brix.WorkOrders.Application.Bases
{
    public class ValidationFault : Failure
    {
        public ErrorType Type
        {
            get
            {
                return ErrorType.ValidationError;
            }
        }
    }
}
