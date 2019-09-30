using FluentAssertions;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators;
using ITG.Brix.WorkOrders.Application.DataTypes;
using ITG.Brix.WorkOrders.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class UpdateWorkOrderCommandValidatorTests
    {
        private UpdateWorkOrderCommandValidator _validator;

        private HandledUnitDto _validHandledUnitDto;
        private RemarkDto _validRemarkDto;
        private PictureDto _validPictureDto;
        private InputDto _validInputDto;


        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateWorkOrderCommandValidator();
            _validHandledUnitDto = new HandledUnitDto
            {
                Id = Guid.NewGuid().ToString(),
                SourceUnitId = Guid.NewGuid().ToString(),
                OperantId = Guid.NewGuid().ToString(),
                OperantLogin = "OperantLogin",
                HandledOn = "2019-05-16T06:48:50Z",

                Warehouse = "Warehouse",
                Gate = "Gate",
                Row = "Row",
                Position = "Position",

                Units = "1",
                IsPartial = "false",
                IsMixed = "true",
                Quantity = "55",

                WeightNet = "12.00",
                WeightGross = "14.00",
                PalletNumber = "PalletNumber",
                SsccNumber = "SsccNumber",

                Products = new List<GoodDto>()
                {
                    new GoodDto()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CongfigurationCode = "",
                        CongfigurationDescription = "",
                        CongfigurationQuantity = "",
                        CongfigurationUnitType = "",
                        CongfigurationNetPerUnit = "",
                        CongfigurationNetPerUnitAlwaysDifferent = "",
                        CongfigurationGrossPerUnit = "",

                        Code = "",
                        Customer = "",
                        Arrival = "",
                        Article = "",
                        ArticlePackagingCode = "",
                        Name = "",
                        Gtin = "",
                        ProductType = "",
                        MaterialType = "",
                        Color = "",
                        Shape = "",
                        Lotbatch = "",
                        Lotbatch2 = "",
                        ClientReference = "",
                        ClientReference2 = "",
                        BestBeforeDate = "",
                        DateFifo = "",
                        CustomsDocument = "",
                        StorageStatus = "",
                        Stackheight = "",
                        Length = "",
                        Width = "",
                        Height = "",
                        OriginalContainer = "",
                        Quantity = "55",
                        WeightNet = "14",
                        WeightGross = "15"
                    }
                }
            };
            _validRemarkDto = new RemarkDto()
            {
                OperantId = Guid.NewGuid().ToString(),
                Operant = "OperantLogin",
                CreatedOn = "2019-05-16T06:48:50Z",
                Text = "Text"
            };
            _validPictureDto = new PictureDto()
            {
                OperantId = Guid.NewGuid().ToString(),
                Operant = "OperantLogin",
                CreatedOn = "2019-05-16T06:48:50Z",
                Name = "Picture.jpg"
            };
            _validInputDto = new InputDto()
            {
                OperantId = Guid.NewGuid().ToString(),
                Operant = "OperantLogin",
                CreatedOn = "2019-05-16T06:48:50Z",
                Property = "Any",
                Value = "Any"
            };
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            Optional<string> status = new Optional<string>("Open");
            Optional<string> operant = new Optional<string>("Operant");
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();

            var command = new UpdateWorkOrderCommand(id: Guid.NewGuid(),
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveWorkOrderNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            Optional<string> status = new Optional<string>("Open");
            Optional<string> operant = new Optional<string>("Operant");
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();

            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.WorkOrderNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        #region Status

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void ShouldHaveStatusCannotBeEmptyValidationFailureWhenIsNullOrWhitespace(string statusValue)
        {
            var id = Guid.NewGuid();
            Optional<string> status = new Optional<string>(statusValue);
            Optional<string> operant = new Optional<string>("Operant");
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;

            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Status") && a.ErrorMessage.Contains(ValidationFailures.StatusCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveStatusAllowedValuesValidationFailureWhenStatusIsWrong()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>("Operant");
            var statusValue = "wrongStatus";
            Optional<string> status = new Optional<string>(statusValue);
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Status") && a.ErrorMessage.Contains(ValidationFailures.StatusAllowedValues.Substring(0, ValidationFailures.StatusAllowedValues.IndexOf("{0}"))));

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnits

        [TestMethod]
        public void ShouldHaveHandledUnitsCannotBeEmptyValidationFailureWhenHandledUnitsHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> status = new Optional<string>();
            Optional<string> operant = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(null);
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;

            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("HandledUnits") && a.ErrorMessage.Contains(ValidationFailures.HandledUnitsCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsCannotBeEmptyValidationFailureWhenHandledUnitsIsEmpty()
        {
            var id = Guid.NewGuid();
            Optional<string> status = new Optional<string>();
            Optional<string> operant = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;

            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("HandledUnits") && a.ErrorMessage.Contains(ValidationFailures.HandledUnitsCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnits.Elements

        [TestMethod]
        public void ShouldHaveHandledUnitsElemCannotBeNullValidationFailureWhenHandlingUnitsElemHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = new HandledUnitDto
            {
                Id = Guid.NewGuid().ToString(),
                SourceUnitId = Guid.NewGuid().ToString(),
                OperantId = Guid.NewGuid().ToString(),
                OperantLogin = "OperantLogin",
                HandledOn = "2019-05-16T06:48:50Z",

                Warehouse = "Warehouse",
                Gate = "Gate",
                Row = "Row",
                Position = "Position",

                Units = "1",
                IsPartial = "false",
                IsMixed = "true",
                Quantity = "55",

                WeightNet = "12.00",
                WeightGross = "14.00",
                PalletNumber = "PalletNumber",
                SsccNumber = "SsccNumber"
            };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                .Errors
                .Any(a => a.PropertyName.Equals("HandledUnits") &&
                     a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemCannotBeNull
                                                               .Replace("{Index}", "1")
                                    )
                );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.Id

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemIdHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Id = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Id")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemIdHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Id = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Id")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemIdHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Id = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Id")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidIdValidationFailureWhenHandlingUnitsElemIdHasNonGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Id = "some non guid value";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Id")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidIdValidationFailureWhenHandlingUnitsElemIdHasDefaultGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Id = default(Guid).ToString();

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Id")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.SourceUnitId

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemSourceUnitIdHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.SourceUnitId = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "SourceUnitId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemSourceUnitIdHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.SourceUnitId = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "SourceUnitId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemSourceUnitIdHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.SourceUnitId = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "SourceUnitId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidIdValidationFailureWhenHandlingUnitsElemSourceUnitIdHasNonGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.SourceUnitId = "some non guid value";


            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "SourceUnitId")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidIdValidationFailureWhenHandlingUnitsElemSourceUnitIdHasDefaultGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.SourceUnitId = default(Guid).ToString();

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "SourceUnitId")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.OperantId

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemOperantIdHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.OperantId = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemOperantIdHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.OperantId = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemOperantIdHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.OperantId = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidIdValidationFailureWhenHandlingUnitsElemOperantIdHasNonGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.OperantId = "some non guid value";


            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidIdValidationFailureWhenHandlingUnitsElemOperantIdHasDefaultGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.OperantId = default(Guid).ToString();

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.OperantLogin

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemOperantLoginHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.OperantLogin = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantLogin")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemOperantLoginHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.OperantLogin = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantLogin")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemOperantLoginHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.OperantLogin = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantLogin")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.Warehouse

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemWarehouseHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Warehouse = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Warehouse")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemWarehouseHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Warehouse = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Warehouse")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemWarehouseHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Warehouse = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Warehouse")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.Gate

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemGateHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Gate = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Gate")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemGateHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Gate = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Gate")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemGateHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Gate = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Gate")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.Row

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemRowHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Row = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Row")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemRowHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Row = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Row")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemRowHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Row = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Row")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.Position

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemPositionHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Position = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Position")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemPositionHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Position = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Position")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemPositionHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Position = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Position")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.Units

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsUnitsHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Units = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Units")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemUnitsHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Units = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Units")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemUnitsHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Units = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Units")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("some invalid value")]
        [DataRow("-1")]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidUnitsValidationFailureWhenHandlingUnitsElemUnitsHasInvalidValue(string invalidUnits)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Units = invalidUnits;


            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Units")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.IsPartial

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemIsPartialHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.IsPartial = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "IsPartial")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemIsPartialHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.IsPartial = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "IsPartial")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemIsPartialHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.IsPartial = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "IsPartial")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("some invalid value non convertible to bool")]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidValidationFailureWhenHandlingUnitsElemIsPartialHasInvalidValue(string invalidIsPartial)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.IsPartial = invalidIsPartial;


            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "IsPartial")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.IsMixed

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemIsMixedHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.IsMixed = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "IsMixed")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemIsMixedHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.IsMixed = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "IsMixed")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemIsMixedHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.IsMixed = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "IsMixed")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("some invalid value non convertible to bool")]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidValidationFailureWhenHandlingUnitsElemIsMixedHasInvalidValue(string invalidIsMixed)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.IsMixed = invalidIsMixed;


            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "IsMixed")
                                                )
                            );

            exists.Should().BeTrue();
        }
        #endregion

        #region HandledUnit.Quantity

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemQuantityHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Quantity = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Quantity")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemQuantityHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Quantity = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Quantity")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemQuantityHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Quantity = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Quantity")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("some invalid value")]
        [DataRow("-1")]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidUnitsValidationFailureWhenHandlingUnitsElemQuantityHasInvalidValue(string invalidQuantity)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Quantity = invalidQuantity;


            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Quantity")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.WeightNet

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemWeightNetHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.WeightNet = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "WeightNet")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemWeightNetHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.WeightNet = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "WeightNet")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemWeightNetHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.WeightNet = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "WeightNet")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("some invalid value")]
        [DataRow("-1.10")]
        public void ShouldHaveHandledUnitsElemKeyValueInvalidUnitsValidationFailureWhenHandlingUnitsElemWeightNetHasInvalidValue(string invalidWeightNet)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.WeightNet = invalidWeightNet;


            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "WeightNet")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.WeightGross

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemWeightGrossHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.WeightGross = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "WeightGross")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemWeightGrossHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.WeightGross = string.Empty;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "WeightGross")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemWeightGrossHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.WeightGross = "   ";

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "WeightGross")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnit.Products

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Products = null;

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Products")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsIsEmpty()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Products = new List<GoodDto>();

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Products")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnits.Products.Elements

        [TestMethod]
        public void ShouldHaveHandledUnitProductsElemCannotBeNullValidationFailureWhenHandlingUnitProductsHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            handledUnitDto.Products = new List<GoodDto>() {
                handledUnitDto.Products.First(),
                null
            };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                .Errors
                .Any(a => a.PropertyName.Equals("HandledUnits") &&
                     a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemCannotBeNull
                                                               .Replace("{Index}", "0")
                                                               .Replace("{IndexProduct}", "1")
                                    )
                );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnits.Product.BestBeforeDate

        [DataTestMethod]
        [DataRow("some invalid BestBeforeDate value")]
        [DataRow("-1")]
        [DataRow("")]
        [DataRow("   ")]
        public void ShouldHaveHandledUnitsProductsElemKeyValueInvalidValidationFailureWhenHandlingUnitsElemProductsElemBestBeforeDateHasInvalidValue(string invalidBestBeforeDate)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.BestBeforeDate = invalidBestBeforeDate;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "BestBeforeDate")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("2019-01-30T06:48:50Z")]
        [DataRow(null)]
        public void ShouldNotHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemBestBeforeDateHasValidValue(string validBestBeforeDate)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.BestBeforeDate = validBestBeforeDate;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "BestBeforeDate")
                                                )
                            );

            exists.Should().BeFalse();
        }

        #endregion

        #region HandledUnits.Product.BestBeforeDate

        [DataTestMethod]
        [DataRow("some invalid DateFifo value")]
        [DataRow("-1")]
        [DataRow("")]
        [DataRow("   ")]
        public void ShouldHaveHandledUnitsProductsElemKeyValueInvalidValidationFailureWhenHandlingUnitsElemProductsElemDateFifoHasInvalidValue(string invalidDateFifo)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.DateFifo = invalidDateFifo;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "DateFifo")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("2019-01-30T06:48:50Z")]
        [DataRow(null)]
        public void ShouldNotHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemDateFifoHasValidValue(string validDateFifo)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.DateFifo = validDateFifo;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "DateFifo")
                                                )
                            );

            exists.Should().BeFalse();
        }

        #endregion

        #region HandledUnits.Product.Quantity

        [TestMethod]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemQuantityHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.Quantity = null;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "Quantity")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemQuantityHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.Quantity = string.Empty;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "Quantity")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemQuantityHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.Quantity = "   ";
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "Quantity")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("some invalid value")]
        [DataRow("-1")]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemQuantityHasInvalidValue(string invalidBestBeforeDate)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.Quantity = invalidBestBeforeDate;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "Quantity")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnits.Product.WeightNet

        [TestMethod]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemWeightNetHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.WeightNet = null;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "WeightNet")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemWeightNetHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.WeightNet = string.Empty;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "WeightNet")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemWeightNetHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.WeightNet = "   ";
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "WeightNet")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("some invalid value")]
        [DataRow("-12.09")]
        [DataRow("-1")]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemWeightNetHasInvalidValue(string invalidWeightNet)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.WeightNet = invalidWeightNet;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "WeightNet")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region HandledUnits.Product.WeightGross

        [TestMethod]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemWeightGrossHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.WeightGross = null;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "WeightGross")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemWeightGrossHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.WeightGross = string.Empty;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "WeightGross")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemWeightGrossHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.WeightGross = "   ";
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "WeightGross")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("some invalid value")]
        [DataRow("-12.09")]
        [DataRow("-1")]
        public void ShouldHaveHandledUnitsProductsElemKeyValueCannotBeEmptyValidationFailureWhenHandlingUnitsElemProductsElemWeightGrossHasInvalidValue(string invalidWeightGross)
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();

            var handledUnitDto = _validHandledUnitDto;
            var goodDto = handledUnitDto.Products.First();
            goodDto.WeightGross = invalidWeightGross;
            handledUnitDto.Products = new List<GoodDto>() { goodDto };

            IList<HandledUnitDto> handledUnitDtos = new List<HandledUnitDto> {
                handledUnitDto,
                null
            };
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>(handledUnitDtos);

            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("HandledUnits") &&
                                 a.ErrorMessage.Contains(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{IndexProduct}", "0")
                                                                           .Replace("{Key}", "WeightGross")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region Remarks

        [TestMethod]
        public void ShouldHaveRemarksCannotBeEmptyValidationFailureWhenRemarksHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> status = new Optional<string>();
            Optional<string> operant = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(null);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;

            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Remarks") && a.ErrorMessage.Contains(ValidationFailures.RemarksCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksCannotBeEmptyValidationFailureWhenRemarksIsEmpty()
        {
            var id = Guid.NewGuid();
            Optional<string> status = new Optional<string>();
            Optional<string> operant = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            IList<RemarkDto> remarkDtos = new List<RemarkDto>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;

            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Remarks") && a.ErrorMessage.Contains(ValidationFailures.RemarksCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        #endregion

        #region Remarks.Elements

        [TestMethod]
        public void ShouldHaveRemarksElemCannotBeNullValidationFailureWhenRemarksElemHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = new RemarkDto
            {
                OperantId = Guid.NewGuid().ToString(),
                Operant = "OperantLogin",
                CreatedOn = "2019-05-16T06:48:50Z",
                Text = "Text"
            };

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto,
                null
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                .Errors
                .Any(a => a.PropertyName.Equals("Remarks") &&
                     a.ErrorMessage.Contains(ValidationFailures.RemarksElemCannotBeNull
                                                               .Replace("{Index}", "1")
                                    )
                );

            exists.Should().BeTrue();
        }

        #endregion

        #region Remarks.OperantId

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemOperantIdHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.OperantId = null;

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemOperantIdHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.OperantId = string.Empty;

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemOperantIdHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.OperantId = "   ";

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueInvalidOperantIdValidationFailureWhenRemarksElemOperantIdHasNonGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.OperantId = "some non guid value";

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueInvalidOperantIdValidationFailureWhenRemarksElemIdHasDefaultGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.OperantId = default(Guid).ToString();

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region Remarks.Operant

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemOperantHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.Operant = null;

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Operant")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemOperantHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.Operant = string.Empty;

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Operant")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemOperantHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.Operant = "   ";

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Operant")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region Remarks.CreatedOn

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemCreatedOnHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.CreatedOn = null;

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemCreatedOnHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.CreatedOn = string.Empty;

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemCreatedOnHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.CreatedOn = "   ";

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueInvalidCreatedOnValidationFailureWhenRemarksElemCreatedOnHasInvalidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.CreatedOn = "some non createdOn value";

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();

            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldNotHaveRemarksElemKeyValueInvalidCreatedOnValidationFailureWhenRemarksElemCreatedOnHasValidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.CreatedOn = "2019-01-30T06:48:50Z";

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );

            exists.Should().BeFalse();
        }

        #endregion

        #region Remarks.Text

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemTextHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.Text = null;

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Text")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemTextHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.Text = string.Empty;

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Text")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveRemarksElemKeyValueCannotBeEmptyValidationFailureWhenRemarksElemTextHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();

            var remarkDto = _validRemarkDto;
            remarkDto.Text = "   ";

            IList<RemarkDto> remarkDtos = new List<RemarkDto> {
                remarkDto
            };
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>(remarkDtos);
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Remarks") &&
                                 a.ErrorMessage.Contains(ValidationFailures.RemarksElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Text")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region Pictures

        [TestMethod]
        public void ShouldHavePicturesCannotBeEmptyValidationFailureWhenPicturesHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> status = new Optional<string>();
            Optional<string> operant = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(null);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;

            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Pictures") && a.ErrorMessage.Contains(ValidationFailures.PicturesCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesCannotBeEmptyValidationFailureWhenPicturesIsEmpty()
        {
            var id = Guid.NewGuid();
            Optional<string> status = new Optional<string>();
            Optional<string> operant = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            IList<PictureDto> pictureDtos = new List<PictureDto>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;

            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Pictures") && a.ErrorMessage.Contains(ValidationFailures.PicturesCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        #endregion

        #region Pictures.Elements

        [TestMethod]
        public void ShouldHavePicturesElemCannotBeNullValidationFailureWhenPicturesElemHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = new PictureDto
            {
                OperantId = Guid.NewGuid().ToString(),
                Operant = "OperantLogin",
                CreatedOn = "2019-05-16T06:48:50Z",
                Name = "Picture.jpg"
            };
            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto,
                null
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                .Errors
                .Any(a => a.PropertyName.Equals("Pictures") &&
                     a.ErrorMessage.Contains(ValidationFailures.PicturesElemCannotBeNull
                                                               .Replace("{Index}", "1")
                                    )
                );

            exists.Should().BeTrue();
        }

        #endregion

        #region Pictures.OperantId

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemOperantIdHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.OperantId = null;

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemOperantIdHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.OperantId = string.Empty;

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemOperantIdHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            var pictureDto = _validPictureDto;
            pictureDto.OperantId = "   ";

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueInvalidOperantIdValidationFailureWhenPicturesElemOperantIdHasNonGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.OperantId = "some non guid value";

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueInvalidOperantIdValidationFailureWhenPicturesElemOperantIdHasDefaultGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.OperantId = default(Guid).ToString();

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region Pictures.Operant

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemOperantHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.Operant = null;

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Operant")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemOperantHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.Operant = string.Empty;

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Operant")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemOperantHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.Operant = "   ";

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Operant")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region Pictures.CreatedOn

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemCreatedOnHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.CreatedOn = null;

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemCreatedOnHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.CreatedOn = string.Empty;

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemCreatedOnHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.CreatedOn = "   ";

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueInvalidCreatedOnValidationFailureWhenPicturesElemCreatedOnHasInvalidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.CreatedOn = "some non createdOn value";

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();

            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldNotHavePicturesElemKeyValueInvalidCreatedOnValidationFailureWhenPicturesElemCreatedOnHasValidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.CreatedOn = "2019-01-30T06:48:50Z";

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );

            exists.Should().BeFalse();
        }

        #endregion

        #region Pictures.Name

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemNameHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.Name = null;

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Name")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemNameHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.Name = string.Empty;

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Name")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHavePicturesElemKeyValueCannotBeEmptyValidationFailureWhenPicturesElemNameHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var pictureDto = _validPictureDto;
            pictureDto.Name = "   ";

            IList<PictureDto> pictureDtos = new List<PictureDto> {
                pictureDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>(pictureDtos);
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>();
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Pictures") &&
                                 a.ErrorMessage.Contains(ValidationFailures.PicturesElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Name")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region Inputs
        [TestMethod]
        public void ShouldHaveInputsCannotBeEmptyValidationFailureWhenInputsHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> status = new Optional<string>();
            Optional<string> operant = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(null);
            var version = 1;

            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Inputs") && a.ErrorMessage.Contains(ValidationFailures.InputsCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsCannotBeEmptyValidationFailureWhenInputsIsEmpty()
        {
            var id = Guid.NewGuid();
            Optional<string> status = new Optional<string>();
            Optional<string> operant = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            IList<InputDto> inputDtos = new List<InputDto>();
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;

            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Inputs") && a.ErrorMessage.Contains(ValidationFailures.InputsCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        #endregion

        #region Inputs.Elements

        [TestMethod]
        public void ShouldHaveInputsElemCannotBeNullValidationFailureWhenInputsElemHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = new InputDto
            {
                OperantId = Guid.NewGuid().ToString(),
                Operant = "OperantLogin",
                CreatedOn = "2019-05-16T06:48:50Z",
                Property = "Any",
                Value = "Any"
            };
            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto,
                null
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                .Errors
                .Any(a => a.PropertyName.Equals("Inputs") &&
                     a.ErrorMessage.Contains(ValidationFailures.InputsElemCannotBeNull
                                                               .Replace("{Index}", "1")
                                    )
                );

            exists.Should().BeTrue();
        }

        #endregion

        #region Inputs.OperantId

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemOperantIdHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.OperantId = null;

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemOperantIdHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.OperantId = string.Empty;

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemOperantIdHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();
            var inputDto = _validInputDto;
            inputDto.OperantId = "   ";

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueInvalidOperantIdValidationFailureWhenInputsElemOperantIdHasNonGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.OperantId = "some non guid value";

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueInvalidOperantIdValidationFailureWhenInputsElemOperantIdHasDefaultGuidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.OperantId = default(Guid).ToString();

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);


            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "OperantId")
                                                )
                            );

            exists.Should().BeTrue();
        }

        #endregion

        #region Inputs.Operant

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemOperantHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.Operant = null;

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Operant")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemOperantHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.Operant = string.Empty;

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Operant")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemOperantHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.Operant = "   ";

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Operant")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region Inputs.CreatedOn

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemCreatedOnHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.CreatedOn = null;

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemCreatedOnHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.CreatedOn = string.Empty;

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemCreatedOnHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.CreatedOn = "   ";

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueInvalidCreatedOnValidationFailureWhenInputsElemCreatedOnHasInvalidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.CreatedOn = "some non createdOn value";

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);

            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );

            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldNotHaveInputsElemKeyValueInvalidCreatedOnValidationFailureWhenInputsElemCreatedOnHasValidValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.CreatedOn = "2019-01-30T06:48:50Z";

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueInvalid
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "CreatedOn")
                                                )
                            );

            exists.Should().BeFalse();
        }

        #endregion

        #region Inputs.Property

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemPropertyHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.Property = null;

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Property")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemPropertyHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.Property = string.Empty;

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Property")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemPropertyHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.Property = "   ";

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Property")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

        #region Inputs.Value

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemValueHasNullValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.Value = null;

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Value")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemValueHasEmptyValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.Value = string.Empty;

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Value")
                                                )
                            );


            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputsElemKeyValueCannotBeEmptyValidationFailureWhenInputsElemValueHasWhitespaceValue()
        {
            var id = Guid.NewGuid();
            Optional<string> operant = new Optional<string>();
            Optional<string> status = new Optional<string>();
            Optional<string> startedOn = new Optional<string>();
            Optional<IEnumerable<HandledUnitDto>> handledUnits = new Optional<IEnumerable<HandledUnitDto>>();
            Optional<IEnumerable<RemarkDto>> remarks = new Optional<IEnumerable<RemarkDto>>();

            var inputDto = _validInputDto;
            inputDto.Value = "   ";

            IList<InputDto> inputDtos = new List<InputDto> {
                inputDto
            };
            Optional<IEnumerable<PictureDto>> pictures = new Optional<IEnumerable<PictureDto>>();
            Optional<IEnumerable<InputDto>> inputs = new Optional<IEnumerable<InputDto>>(inputDtos);
            var version = 1;
            var command = new UpdateWorkOrderCommand(id: id,
                                                     operant: operant,
                                                     status: status,
                                                     startedOn: startedOn,
                                                     handledUnits: handledUnits,
                                                     remarks: remarks,
                                                     pictures: pictures,
                                                     inputs: inputs,
                                                     version: version);

            var validationResult = _validator.Validate(command);

            var exists = validationResult
                            .Errors
                            .Any(a => a.PropertyName.Equals("Inputs") &&
                                 a.ErrorMessage.Contains(ValidationFailures.InputsElemKeyValueCannotBeEmpty
                                                                           .Replace("{Index}", "0")
                                                                           .Replace("{Key}", "Value")
                                                )
                            );


            exists.Should().BeTrue();
        }

        #endregion

    }
}
