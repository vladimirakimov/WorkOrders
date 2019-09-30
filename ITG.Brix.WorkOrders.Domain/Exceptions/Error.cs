using System;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
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
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static ArgumentNullException ArgumentNull(string messageFormat, params object[] messageArgs)
        {
            return new ArgumentNullException(string.Format(messageFormat, messageArgs));
        }

        /// <summary>
        ///     Creates an <see cref="ArgumentException" /> with the provided properties.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
        {
            return new ArgumentException(string.Format(messageFormat, messageArgs));
        }

        /// <summary>
        ///     Creates an <see cref="IdFieldShouldNotBeDefaultGuidException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static IdFieldShouldNotBeDefaultGuidException IdFieldShouldNotBeDefaultGuid()
        {
            return new IdFieldShouldNotBeDefaultGuidException();
        }

        /// <summary>
        ///     Creates an <see cref="ItemValueNullOrWhitespaceException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static ItemValueNullOrWhitespaceException ItemValueNullOrWhitespace()
        {
            return new ItemValueNullOrWhitespaceException();
        }

        /// <summary>
        ///     Creates an <see cref="ItemValueFormatException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static ItemValueFormatException ItemValueFormat()
        {
            return new ItemValueFormatException();
        }

        /// <summary>
        ///     Creates an <see cref="FilterKeyValueNullOrWhitespaceException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static FilterKeyValueNullOrWhitespaceException FilterKeyValueNullOrWhitespace()
        {
            return new FilterKeyValueNullOrWhitespaceException();
        }

        /// <summary>
        ///     Creates an <see cref="FilterKeyValueFormatException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static FilterKeyValueFormatException FilterKeyValueFormat()
        {
            return new FilterKeyValueFormatException();
        }

        /// <summary>
        ///     Creates an <see cref="CreatedOnShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static CreatedOnShouldNotBeNullException CreatedOnShouldNotBeNull()
        {
            return new CreatedOnShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="CreatedOnValueFieldShouldBeValidException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static CreatedOnValueFieldShouldBeValidException CreatedOnValueFieldShouldBeValid()
        {
            return new CreatedOnValueFieldShouldBeValidException();
        }

        /// <summary>
        ///     Creates an <see cref="DateOnValueFieldShouldBeValidException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static DateOnValueFieldShouldBeValidException DateOnValueFieldShouldBeValid()
        {
            return new DateOnValueFieldShouldBeValidException();
        }

        /// <summary>
        ///     Creates an <see cref="WeightValueFieldShouldBeGreaterOrEqualToZeroException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static WeightValueFieldShouldBeGreaterOrEqualToZeroException WeightValueFieldShouldBeGreaterOrEqualToZero()
        {
            return new WeightValueFieldShouldBeGreaterOrEqualToZeroException();
        }

        /// <summary>
        ///     Creates an <see cref="QuantityShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static QuantityShouldNotBeNullException QuantityShouldNotBeNull()
        {
            return new QuantityShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="QuantityValueFieldShouldBeGreaterOrEqualToZeroException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static QuantityValueFieldShouldBeGreaterOrEqualToZeroException QuantityValueFieldShouldBeGreaterOrEqualToZero()
        {
            return new QuantityValueFieldShouldBeGreaterOrEqualToZeroException();
        }

        /// <summary>
        ///     Creates an <see cref="LabelValueFieldShouldNotBeEmptyException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static LabelValueFieldShouldNotBeEmptyException LabelValueFieldShouldNotBeEmpty()
        {
            return new LabelValueFieldShouldNotBeEmptyException();
        }

        /// <summary>
        ///     Creates an <see cref="WarehouseLabelFieldShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static WarehouseLabelFieldShouldNotBeNullException WarehouseLabelFieldShouldNotBeNull()
        {
            return new WarehouseLabelFieldShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="GateLabelFieldShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static GateLabelFieldShouldNotBeNullException GateLabelFieldShouldNotBeNull()
        {
            return new GateLabelFieldShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="RowLabelFieldShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static RowLabelFieldShouldNotBeNullException RowLabelFieldShouldNotBeNull()
        {
            return new RowLabelFieldShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="PositionLabelFieldShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static PositionLabelFieldShouldNotBeNullException PositionLabelFieldShouldNotBeNull()
        {
            return new PositionLabelFieldShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="LocationWarehouseShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static LocationWarehouseShouldNotBeNullException LocationWarehouseShouldNotBeNull()
        {
            return new LocationWarehouseShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="LocationGateShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static LocationGateShouldNotBeNullException LocationGateShouldNotBeNull()
        {
            return new LocationGateShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="LocationRowShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static LocationRowShouldNotBeNullException LocationRowShouldNotBeNull()
        {
            return new LocationRowShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="LocationPositionShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static LocationPositionShouldNotBeNullException LocationPositionShouldNotBeNull()
        {
            return new LocationPositionShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="HandledUnitOperantShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static HandledUnitOperantShouldNotBeNullException HandledUnitOperantShouldNotBeNull()
        {
            return new HandledUnitOperantShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="HandledUnitHandledOnShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static HandledUnitHandledOnShouldNotBeNullException HandledUnitHandledOnShouldNotBeNull()
        {
            return new HandledUnitHandledOnShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="HandledUnitLocationShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static HandledUnitLocationShouldNotBeNullException HandledUnitLocationShouldNotBeNull()
        {
            return new HandledUnitLocationShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="HandledUnitGoodShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static HandledUnitGoodShouldNotBeNullException HandledUnitGoodShouldNotBeNull()
        {
            return new HandledUnitGoodShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="HandledOnShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static HandledOnShouldNotBeNullException HandledOnShouldNotBeNull()
        {
            return new HandledOnShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="HandledOnValueFieldShouldBeValidException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static HandledOnValueFieldShouldBeValidException HandledOnValueFieldShouldBeValid()
        {
            return new HandledOnValueFieldShouldBeValidException();
        }

        /// <summary>
        ///     Creates an <see cref="UnitGroupShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static UnitGroupShouldNotBeNullException UnitGroupShouldNotBeNull()
        {
            return new UnitGroupShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="UnitMixedShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static UnitMixedShouldNotBeNullException UnitMixedShouldNotBeNull()
        {
            return new UnitMixedShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="UnitLocationShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static UnitLocationShouldNotBeNullException UnitLocationShouldNotBeNull()
        {
            return new UnitLocationShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="UnitsShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static UnitsShouldNotBeNullException UnitsShouldNotBeNull()
        {
            return new UnitsShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="UnitsValueFieldShouldBeGreaterOrEqualToZeroException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static UnitsValueFieldShouldBeGreaterOrEqualToZeroException UnitsValueFieldShouldBeGreaterOrEqualToZero()
        {
            return new UnitsValueFieldShouldBeGreaterOrEqualToZeroException();
        }

        /// <summary>
        ///     Creates an <see cref="LoginValueFieldShouldNotBeEmptyException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static LoginValueFieldShouldNotBeEmptyException LoginValueFieldShouldNotBeEmpty()
        {
            return new LoginValueFieldShouldNotBeEmptyException();
        }

        /// <summary>
        ///     Creates an <see cref="PictureOperantFieldShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static PictureOperantFieldShouldNotBeNullException PictureOperantFieldShouldNotBeNull()
        {
            return new PictureOperantFieldShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="PictureCreatedOnFieldShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static PictureCreatedOnFieldShouldNotBeNullException PictureCreatedOnFieldShouldNotBeNull()
        {
            return new PictureCreatedOnFieldShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="PictureNameFieldShouldNotBeEmptyException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static PictureNameFieldShouldNotBeEmptyException PictureNameFieldShouldNotBeEmpty()
        {
            return new PictureNameFieldShouldNotBeEmptyException();
        }

        /// <summary>
        ///     Creates an <see cref="RemarkOperantFieldShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static RemarkOperantFieldShouldNotBeNullException RemarkOperantFieldShouldNotBeNull()
        {
            return new RemarkOperantFieldShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="RemarkCreatedOnFieldShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static RemarkCreatedOnFieldShouldNotBeNullException RemarkCreatedOnFieldShouldNotBeNull()
        {
            return new RemarkCreatedOnFieldShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="RemarkTextFieldShouldNotBeEmptyException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static RemarkTextFieldShouldNotBeEmptyException RemarkTextFieldShouldNotBeEmpty()
        {
            return new RemarkTextFieldShouldNotBeEmptyException();
        }

        /// <summary>
        ///     Creates an <see cref="OperantIdFieldShouldNotBeDefaultGuidException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static OperantIdFieldShouldNotBeDefaultGuidException OperantIdFieldShouldNotBeDefaultGuid()
        {
            return new OperantIdFieldShouldNotBeDefaultGuidException();
        }

        /// <summary>
        ///     Creates an <see cref="OperantLoginFieldShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static OperantLoginFieldShouldNotBeNullException OperantLoginFieldShouldNotBeNull()
        {
            return new OperantLoginFieldShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="InputOperantFieldShouldNotBeEmptyException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static InputOperantFieldShouldNotBeEmptyException InputOperantFieldShouldNotBeEmpty()
        {
            return new InputOperantFieldShouldNotBeEmptyException();
        }

        /// <summary>
        ///     Creates an <see cref="InputCreatedOnFieldShouldNotBeEmptyException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static InputCreatedOnFieldShouldNotBeEmptyException InputCreatedOnFieldShouldNotBeEmpty()
        {
            return new InputCreatedOnFieldShouldNotBeEmptyException();
        }

        /// <summary>
        ///     Creates an <see cref="InputValueFieldShouldNotBeEmptyException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static InputValueFieldShouldNotBeEmptyException InputValueFieldShouldNotBeEmpty()
        {
            return new InputValueFieldShouldNotBeEmptyException();
        }

        /// <summary>
        ///     Creates an <see cref="InputPropertyFieldShouldNotBeEmptyException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static InputPropertyFieldShouldNotBeEmptyException InputPropertyFieldShouldNotBeEmpty()
        {
            return new InputPropertyFieldShouldNotBeEmptyException();
        }
    }
}
