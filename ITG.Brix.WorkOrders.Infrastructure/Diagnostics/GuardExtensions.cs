using ITG.Brix.Diagnostics.Guards;

namespace ITG.Brix.WorkOrders.Infrastructure.Diagnostics
{
    internal static partial class GuardExtensions
    {
        #region T

        internal static Guard<T> AgainstNull<T>(this Guard<T> guard)
            where T : class
        {
            if (guard.Value == null)
            {
                throw guard.Exception;
            }

            return guard;
        }

        #endregion

        #region string

        internal static Guard<string> AgainstNullOrWhiteSpace(this Guard<string> guard)
        {
            if (string.IsNullOrWhiteSpace(guard.Value))
            {
                throw guard.Exception;
            }

            return guard;
        }

        #endregion

        #region bool

        internal static Guard<bool> AgainstFalse(this Guard<bool> guard)
        {
            if (guard.Value == false)
            {
                throw guard.Exception;
            }

            return guard;
        }

        #endregion
    }
}
