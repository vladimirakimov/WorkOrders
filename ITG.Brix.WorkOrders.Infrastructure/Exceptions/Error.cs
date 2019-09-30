﻿using System;

namespace ITG.Brix.WorkOrders.Infrastructure.Exceptions
{
    /// <summary>
    ///     Error class for creating and unwrapping <see cref="Exception" /> instances.
    /// </summary>
    internal static class Error
    {
        /// <summary>
        ///     Creates an <see cref="ArgumentNullException" /> with the provided properties.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="ArgumentNullException" />.</returns>
        internal static ArgumentNullException ArgumentNull(string messageFormat, params object[] messageArgs)
        {
            return new ArgumentNullException(string.Format(messageFormat, messageArgs));
        }

        /// <summary>
        ///     Creates an <see cref="EntityNotFoundDbException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="EntityNotFoundDbException" />.</returns>
        internal static EntityNotFoundDbException EntityNotFoundDb()
        {
            return new EntityNotFoundDbException();
        }

        /// <summary>
        ///     Creates an <see cref="EntityVersionDbException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="EntityVersionDbException" />.</returns>
        internal static EntityVersionDbException EntityVersionDb()
        {
            return new EntityVersionDbException();
        }

        /// <summary>
        ///     Creates an <see cref="FilterODataException" /> with the provided properties.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="FilterODataException" />.</returns>
        internal static FilterODataException FilterOData(string messageFormat, params object[] messageArgs)
        {
            return new FilterODataException(string.Format(messageFormat, messageArgs));
        }

        /// <summary>
        ///     Creates an <see cref="FilterODataException" /> with the provided properties.
        /// </summary>
        /// <param name="exception">An <see cref="Exception" /> object.</param>
        /// <returns>The logged <see cref="FilterODataException" />.</returns>
        internal static FilterODataException FilterOData(Exception exception)
        {
            return new FilterODataException(exception);
        }

        /// <summary>
        ///     Creates an <see cref="GenericDbException" /> with the provided properties.
        /// </summary>
        /// <param name="exception">An <see cref="Exception" /> object.</param>
        /// <returns>The logged <see cref="GenericDbException" />.</returns>
        internal static GenericDbException GenericDb(Exception exception)
        {
            return new GenericDbException(exception);
        }

        /// <summary>
        ///     Creates an <see cref="UniqueKeyDbException" /> with the provided properties.
        /// </summary>
        /// <param name="exception">An <see cref="Exception" /> object.</param>
        /// <returns>The logged <see cref="UniqueKeyDbException" />.</returns>
        internal static UniqueKeyDbException UniqueKey(Exception exception)
        {
            return new UniqueKeyDbException(exception);
        }


        /// <summary>
        ///     Creates an <see cref="ParseUtcArgumentException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static ParseUtcArgumentException ParseUtcArgument()
        {
            return new ParseUtcArgumentException();
        }

        /// <summary>
        ///     Creates an <see cref="UtcFormatException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static UtcFormatException UtcFormat(string utc)
        {
            return new UtcFormatException(utc);
        }
    }
}
