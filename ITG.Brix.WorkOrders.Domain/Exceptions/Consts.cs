namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    internal static class ExceptionMessage
    {
        public const string CreatedOnShouldNotBeNull = "CreatedOn shouldn't be null.";
        public const string CreatedOnValueFieldShouldBeValid = "CreatedOn value field should be utc greater or equal to 1 January 1970.";
        public const string DateOnValueFieldShouldBeValid = "DateOn value field should be utc greater or equal to 1 January 1970.";
        public const string IdFieldShouldNotBeDefaultGuid = "Id field shouldn't be default guid.";
        public const string ItemValueNullOrWhitespace = "Item value shouldn't be null or whitespace.";
        public const string ItemValueFormat = "Item value should have correct format, only lowercased letters and underline symbols allowed.";
        public const string FilterKeyValueNullOrWhitespace = "Filter key value shouldn't be null or whitespace.";
        public const string FilterKeyValueFormat = "Filter key value should have correct format, only lowercased letters and dash symbols allowed.";
        public const string WeightValueFieldShouldBeGreaterOrEqualToZero = "Weight value field should be greater or equal to 0.";
        public const string QuantityShouldNotBeNull = "Quantity shouldn't be null.";
        public const string QuantityValueFieldShouldBeGreaterOrEqualToZero = "Quantity value field should be greater or equal to 0.";
        public const string LabelValueFieldShouldNotBeEmpty = "Label value field shouldn't be empty.";
        public const string LoginValueFieldShouldNotBeEmpty = "Login value field shouldn't be empty.";
        public const string WarehouseLabelFieldShouldNotBeNull = "Warehouse label field shouldn't be null.";
        public const string GateLabelFieldShouldNotBeNull = "Gate label field shouldn't be null.";
        public const string RowLabelFieldShouldNotBeNull = "Row label field shouldn't be null.";
        public const string PositionLabelFieldShouldNotBeNull = "Position label field shouldn't be null.";
        public const string LocationWarehouseShouldNotBeNull = "Location warehouse shouldn't be null.";
        public const string LocationGateShouldNotBeNull = "Location gate shouldn't be null.";
        public const string LocationRowShouldNotBeNull = "Location row shouldn't be null.";
        public const string LocationPositionShouldNotBeNull = "Location position shouldn't be null.";
        public const string HandledOnShouldNotBeNull = "HandledOn shouldn't be null.";
        public const string HandledOnValueFieldShouldBeValid = "HandledOn value field should be utc greater or equal to 1 January 1970.";
        public const string HandledUnitOperantShouldNotBeNull = "HandledUnit operant shouldn't be null.";
        public const string HandledUnitHandledOnShouldNotBeNull = "HandledUnit handled on shouldn't be null.";
        public const string HandledUnitLocationShouldNotBeNull = "HandledUnit location shouldn't be null.";
        public const string HandledUnitGoodShouldNotBeNull = "HandledUnit good shouldn't be null.";
        public const string UnitGroupShouldNotBeNull = "Unit group shouldn't be null.";
        public const string UnitMixedShouldNotBeNull = "Unit mixed shouldn't be null.";
        public const string UnitLocationShouldNotBeNull = "Unit location shouldn't be null.";
        public const string UnitsShouldNotBeNull = "Units shouldn't be null.";
        public const string UnitsValueFieldShouldBeGreaterOrEqualToZero = "Units value field should be greater or equal to 0.";
        public const string PictureOperantFieldShouldNotBeNull = "Picture operant field shouldn't be null.";
        public const string PictureCreatedOnFieldShouldNotBeNull = "Picture createdOn field shouldn't be null.";
        public const string PictureNameFieldShouldNotBeEmpty = "Picture name field shouldn't be empty.";
        public const string RemarkOperantFieldShouldNotBeNull = "Remark operant field shouldn't be null.";
        public const string RemarkCreatedOnFieldShouldNotBeNull = "Remark createdOn field shouldn't be null.";
        public const string RemarkTextFieldShouldNotBeEmpty = "Remark text field shouldn't be empty.";
        public const string OperantIdFieldShouldNotBeDefaultGuid = "Operant id field shouldn't be default guid.";
        public const string OperantLoginFieldShouldNotBeNull = "Operant login field shouldn't be null.";
        public const string InputOperantFieldShouldNotBeEmpty = "Input operant field shouldn't be empty.";
        public const string InputCreatedOnFieldShouldNotBeEmpty = "Input createdOn field shouldn't be empty.";
        public const string InputValueFieldShouldNotBeEmpty = "Input value field shouldn't be empty.";
        public const string InputPropertyFieldShouldNotBeEmpty = "Input property field shouldn't be empty.";
    }
}
