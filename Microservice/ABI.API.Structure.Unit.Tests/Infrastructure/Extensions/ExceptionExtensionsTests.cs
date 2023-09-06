using ABI.API.Structure.Application.Exceptions;
using ABI.Framework.MS.Helpers.Response;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Extensions.Tests
{
    [TestClass()]
    public class ExceptionExtensionsTests
    {
        [TestMethod()]
        public async Task CatchNodeValidationTrhowsNodeEmployeeResponsableZonesExceptionTest()
        {
            Func<Task<int?>> action = () =>
            {
                throw new NodeEmployeeResponsableZonesException();
            };

            var result = await action.CatchNodeValidation(StatusGenericResponse.Created, 7);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task CatchNodeValidationTrhowsNodeEmployeeNoResponsableZonesExceptionTest()
        {
            Func<Task<int?>> action = () =>
            {
                throw new NodeEmployeeNoResponsableZonesException();
            };

            var result = await action.CatchNodeValidation(StatusGenericResponse.Created, 7);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task CatchNodeValidationTrhowsNodeEmployeeResponsableZonesExceptionTerritoryTest()
        {
            Func<Task<int?>> action = () =>
            {
                throw new NodeEmployeeResponsableZonesException();
            };

            var result = await action.CatchNodeValidation(StatusGenericResponse.Created, 8);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task CatchNodeValidationTrhowsNodeEmployeeNoResponsableZonesExceptionTerritoryTest()
        {
            Func<Task<int?>> action = () =>
            {
                throw new NodeEmployeeNoResponsableZonesException();
            };

            var result = await action.CatchNodeValidation(StatusGenericResponse.Created, 8);

            result.Should().NotBeNull();
        }


        [TestMethod()]
        public async Task NodeEditSameDateExceptionTest()
        {
            Func<Task<int?>> action = () =>
            {
                throw new NodeEditSameDateException();
            };

            var result = await action.CatchNodeValidation(StatusGenericResponse.BadRequest, 8);

            result.Should().NotBeNull();
        }
    }
}