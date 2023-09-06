using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class EditNodeCommandValidActiveValidatorTests
    {
        private static IStructureNodeRepository _nodeRepo;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void EditNodeCommandValidActiveValidatorTest()
        {
            var mockLogger = new Mock<ILogger<EditNodeCommandValidActiveValidator>>();
            var model = new EditNodeCommand(100016, 100016, 10, "TEST", null, true, null, null, null, null, 1, new DateTime(2020, 1, 1), DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
            var validator = new EditNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public async Task EditNodeCommandValidActiveValidatorThrowsChildNodesActiveExceptionTest()
        {
            var mockLogger = new Mock<ILogger<EditNodeCommandValidActiveValidator>>();
            var model = new EditNodeCommand(100015, 100014, 10, "TEST", null, false, null, null, null, null, 1, new DateTime(2020, 2, 2), DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
            var validator = new EditNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);

            await Assert.ThrowsExceptionAsync<ChildNodesActiveException>(async () =>
                await validator.TestValidateAsync(model)
            );
        }

        [TestMethod()]
        public async Task EditNodeCommandValidActiveValidatorThrowsParentNodesActiveExceptionTest()
        {
            var mockLogger = new Mock<ILogger<EditNodeCommandValidActiveValidator>>();
            var model = new EditNodeCommand(100016, 100015, 10, "TEST", null, true, null, null, null, null, 1, new DateTime(2020, 2, 2), DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
            var validator = new EditNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);

            await Assert.ThrowsExceptionAsync<ParentNodesActiveException>(async () =>
                await validator.TestValidateAsync(model)
            );
        }

        [TestMethod()]
        public async Task EditNodeCommandValidActiveValidatorValidTerritoryClientExceptionTest()
        {
            var mockLogger = new Mock<ILogger<EditNodeCommandValidActiveValidator>>();
            var model = new EditNodeCommand(100035, null, 7, "TEST", null, false, null, null, null, null, 8, new DateTime(2021, 6, 4), DateTime.MaxValue.Date);
            var validator = new EditNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);

            await Assert.ThrowsExceptionAsync<TerritoryClientActiveException>(async () =>
                await validator.TestValidateAsync(model)
            );
        }

        [TestMethod()]
        public void EditNodeCommandValidActiveValidatorValidTerritoryClientNoExceptionTest()
        {
            var mockLogger = new Mock<ILogger<EditNodeCommandValidActiveValidator>>();
            var model = new EditNodeCommand(100035, null, 7, "TEST", null, true, null, null, null, null, 8, new DateTime(2021, 6, 4), DateTime.MaxValue.Date);
            var validator = new EditNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void EditNodeCommandValidActiveValidatorValidTerritoryClientTest()
        {
            var mockLogger = new Mock<ILogger<EditNodeCommandValidActiveValidator>>();
            var model = new EditNodeCommand(100036, null, 7, "TEST", null, false, null, null, null, null,8, new DateTime(2021, 6, 4), DateTime.MaxValue.Date);
            var validator = new EditNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}