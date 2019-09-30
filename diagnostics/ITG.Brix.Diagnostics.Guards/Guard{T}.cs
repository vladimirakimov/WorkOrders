using System;

namespace ITG.Brix.Diagnostics.Guards
{
    public sealed class Guard<T>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Guard{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="exception">The exception.</param>
        public Guard(T value, Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(GuardExceptionMessage.ValueMandatory);
            }

            Value = value;
            Exception = exception;
        }
    }
}
