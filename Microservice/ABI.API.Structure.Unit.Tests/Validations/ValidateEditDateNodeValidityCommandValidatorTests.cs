using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Extensions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class ValidateEditDateNodeValidityCommandValidatorTests
    {
        private static IStructureNodeRepository _nodeRepo;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void ValidateEditDateNodeValidityCommandValidator()
        {
            var result = new ValidateEditDateNodeValidityCommandValidator(null, null);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ValidateEditDateNodeValidityCommandValidatorTrue()
        {

            var mockLogger = new Mock<ILogger<ValidateEditDateNodeValidityCommandValidator>>();
            var model = new EditNodeCommand(100044, 100039, 13, "TEST", null, false, null, null, null, null, 1, "2021-05-21".ToDateOffset(-3), DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
            var validator = new ValidateEditDateNodeValidityCommandValidator(mockLogger.Object, _nodeRepo);

            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public async Task ValidateEditDateNodeValidityCommandValidatorFalseError()
        {
            var mockLogger = new Mock<ILogger<ValidateEditDateNodeValidityCommandValidator>>();
            var model = new EditNodeCommand(100044, 100039, 13, "TEST", null, true, null, null, null, null, 1, "2021-05-20".ToDateOffset(-3), DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
            var validator = new ValidateEditDateNodeValidityCommandValidator(mockLogger.Object, _nodeRepo);

            await Assert.ThrowsExceptionAsync<NodeEditSameDateException>(async () =>
                await validator.TestValidateAsync(model)

            ) ;
        }

     
    }
}