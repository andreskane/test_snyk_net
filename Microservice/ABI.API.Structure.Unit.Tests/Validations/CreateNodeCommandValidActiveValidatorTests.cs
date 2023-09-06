using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Validations;
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

namespace ABI.API.Structure.Unit.Tests.Validations
{
    [TestClass()]
    public class CreateNodeCommandValidActiveValidatorTests
    {
        private static IStructureNodeRepository _nodeRepo;
        
        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
        }

        [TestMethod()]
        public void CreateNodeCommandValidActiveValidatorTest()
        {
            var mockLogger = new Mock<ILogger<CreateNodeCommandValidActiveValidator>>();
            var model = new CreateNodeCommand(1, null, "TEST", null, 1, true, null, null, null, null, DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue.Date);
            var validator = new CreateNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public async Task CreateNodeCommandValidActiveValidatorThrowsParentNodesActiveExceptionTest()
        {
            var mockLogger = new Mock<ILogger<CreateNodeCommandValidActiveValidator>>();
            var model = new CreateNodeCommand(1, 100030, "TEST", null, 4, true, null, null, null, null, DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue.Date);
            var validator = new CreateNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);

            await Assert.ThrowsExceptionAsync<ParentNodesActiveException>(async () =>
                await validator.TestValidateAsync(model)
            );
        }

        [TestMethod()]
        public async Task CreateNodeCommandValidActiveValidatorThrowsParentNodesActiveExceptionDraftTest()
        {
            var mockLogger = new Mock<ILogger<CreateNodeCommandValidActiveValidator>>();
            var model = new CreateNodeCommand(1, 100031, "TEST", null, 4, true, null, null, null, null, DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue.Date);
            var validator = new CreateNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);

            await Assert.ThrowsExceptionAsync<ParentNodesActiveException>(async () =>
                await validator.TestValidateAsync(model)
            );
        }

        [TestMethod()]
        public void CreateNodeCommandValidActiveValidatorThrowsParentNodesActiveException2Test()
        {
            var mockLogger = new Mock<ILogger<CreateNodeCommandValidActiveValidator>>();
            var model = new CreateNodeCommand(1, 999999, "TEST", null, 4, true, null, null, null, null, DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue.Date);
            var validator = new CreateNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);

            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void CreateNodeCommandValidActiveValidatorThrowsParentNodesActiveException3Test()
        {
            var mockLogger = new Mock<ILogger<CreateNodeCommandValidActiveValidator>>();
            var model = new CreateNodeCommand(1, 100032, "TEST", null, 4, true, null, null, null, null, DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue.Date);
            var validator = new CreateNodeCommandValidActiveValidator(mockLogger.Object, _nodeRepo);

            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new CreateNodeCommandValidActiveValidator(null, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }
    }
}
