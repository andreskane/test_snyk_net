using ABI.API.Structure.Application.Validations.ABMValidation;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Service.Exceptions;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations.AMBValidations
{
    [TestClass()]
    public class CreateUpdateAbmNameValidatorTests
    {
        [TestMethod()]
        public void CreateUpdateAbmNameValidatorRoleTest()
        {
            var model = new Application.Commands.Role.AddCommand();
            model.Id = 0;
            model.Name = "TEST";
            var validator = new CreateRoleCommandValidator(AddDataContext._context);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void CreateUpdateAbmNameValidatorRoleExceptionTest()
        {
            var model = new Application.Commands.Role.AddCommand();
            model.Id = 0;
            model.Name = "ACTIVADOR";
            var validator = new CreateRoleCommandValidator(AddDataContext._context);
            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NameExistsException>();
        }
        [TestMethod()]
        public void CreateUpdateAbmNameValidatorAttentionModeTest()
        {
            var model = new Application.Commands.AttentionMode.AddCommand();
            model.Id = 0;
            model.Name = "TEST-PRUEBA";
            var validator = new CreateAttentionModeCommandValidator(AddDataContext._context);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void CreateUpdateAbmNameValidatorAttentionModeExceptionTest()
        {
            var model = new Application.Commands.AttentionMode.AddCommand();
            model.Id = 0;
            model.Name = "ATENCIÓN RI MAYORISTA";
            var validator = new CreateAttentionModeCommandValidator(AddDataContext._context);
            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NameExistsException>();
        }
        [TestMethod()]
        public void CreateUpdateAbmNameValidatorLevelTest()
        {
            var model = new Application.Commands.Level.AddCommand();
            model.Id = 0;
            model.Name = "TEST-01";
            var validator = new CreateLevelCommandValidator(AddDataContext._context);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void CreateUpdateAbmNameValidatorLevelExceptionTest()
        {
            var model = new Application.Commands.Level.AddCommand();
            model.Id = 0;
            model.Name = "JEFATURA";
            var validator = new CreateLevelCommandValidator(AddDataContext._context);
            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NameExistsException>();
        }
        [TestMethod()]
        public void CreateUpdateAbmNameValidatorSaleChannelTest()
        {
            var model = new Application.Commands.SaleChannel.AddCommand();
            model.Id = 0;
            model.Name = "TEST-PRUEBA";
            var validator = new CreateSaleChannelCommandValidator(AddDataContext._context);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void CreateUpdateAbmNameSaleChannelModeExceptionTest()
        {
            var model = new Application.Commands.SaleChannel.AddCommand();
            model.Id = 0;
            model.Name = "DIRECTA";
            var validator = new CreateSaleChannelCommandValidator(AddDataContext._context);
            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NameExistsException>();
        }
        [TestMethod()]
        public void UpdateUpdateAbmNameValidatorRoleTest()
        {
            var model = new Application.Commands.Role.UpdateCommand();
            model.Id = 1;
            model.Name = "ACTIVADOR";
            var validator = new UpdateRoleCommandValidator(AddDataContext._context);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void UpdateUpdateAbmNameValidatorRoleExceptionTest()
        {
            var model = new Application.Commands.Role.UpdateCommand();
            model.Id = 2;
            model.Name = "ACTIVADOR";
            var validator = new UpdateRoleCommandValidator(AddDataContext._context);
            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NameExistsException>();
        }
        [TestMethod()]
        public void UpdateUpdateAbmNameValidatorAttentionModeTest()
        {
            var model = new Application.Commands.AttentionMode.UpdateCommand();
            model.Id = 3;
            model.Name = "BDR";
            var validator = new UpdateAttentionModeCommandValidator(AddDataContext._context);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void UpdateUpdateAbmNameValidatorAttentionModeExceptionTest()
        {
            var model = new Application.Commands.AttentionMode.UpdateCommand();
            model.Id = 1;
            model.Name = "BDR";
            var validator = new UpdateAttentionModeCommandValidator(AddDataContext._context);
            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NameExistsException>();
        }
        [TestMethod()]
        public void UpdateUpdateAbmNameValidatorLevelTest()
        {
            var model = new Application.Commands.Level.UpdateCommand();
            model.Id = 7;
            model.Name = "ZONA";
            var validator = new UpdateLevelCommandValidator(AddDataContext._context);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void UpdateUpdateAbmNameValidatorLevelExceptionTest()
        {
            var model = new Application.Commands.Level.UpdateCommand();
            model.Id = 1;
            model.Name = "ZONA";
            var validator = new UpdateLevelCommandValidator(AddDataContext._context);
            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NameExistsException>();
        }
        [TestMethod()]
        public void UpdateUpdateAbmNameValidatorSaleChannelTest()
        {
            var model = new Application.Commands.SaleChannel.UpdateCommand();
            model.Id = 1;
            model.Name = "DIRECTA";
            var validator = new UpdateSaleChannelCommandValidator(AddDataContext._context);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void UpdateUpdateAbmNameSaleChannelModeExceptionTest()
        {
            var model = new Application.Commands.SaleChannel.UpdateCommand();
            model.Id = 2;
            model.Name = "DIRECTA";
            var validator = new UpdateSaleChannelCommandValidator(AddDataContext._context);
            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NameExistsException>();
        }
    }
}
