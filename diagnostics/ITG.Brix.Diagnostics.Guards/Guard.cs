using System;

namespace ITG.Brix.Diagnostics.Guards
{
    /// <summary>
    /// Facilitates the instantiation of <see cref="Guard{T}"/> instances.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Guards on specific object value.
        /// </summary>
        /// <example>
        /// public string MethodName(object obj)
        /// {
        ///     Guard.On(...);
        ///     return obj.ToString();
        /// }
        /// </example>
        public static Guard<T> On<T>(T value, Exception exception)
        {
            return new Guard<T>(value, exception);
        }
    }
}
