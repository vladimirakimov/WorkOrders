using System;

namespace ITG.Brix.Diagnostics.Logging.Exceptions
{
    /// <summary>
    ///     Error class for creating and unwrapping <see cref="Exception" /> instances.
    /// </summary>
    internal static class Error
    {
        /// <summary>
        ///     Creates an <see cref="LoggerShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static LoggerShouldNotBeNullException LoggerShouldNotBeNull()
        {
            return new LoggerShouldNotBeNullException();
        }
    }
}
