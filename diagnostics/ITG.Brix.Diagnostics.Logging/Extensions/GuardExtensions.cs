using ITG.Brix.Diagnostics.Guards;

namespace ITG.Brix.Diagnostics.Logging.Extensions
{
    internal static partial class GuardExtensions
    {
        internal static Guard<T> AgainstNull<T>(this Guard<T> guard)
            where T : class
        {
            if (guard.Value == null)
            {
                throw guard.Exception;
            }

            return guard;
        }
    }
}
