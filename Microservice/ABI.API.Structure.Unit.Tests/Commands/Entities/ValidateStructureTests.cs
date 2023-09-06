using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.Commands.Entities.Tests
{
    [TestClass()]
    public class ValidateStructureTests
    {
        [TestMethod()]
        public void ValidateStructureTest()
        {
            var result = new ValidateStructure();
            result.Valid = true;
            result.Errors = new List<ValidateError>();
            result.AddValidateError(1, "TEST", "TEST", 1, "TEST", "TEST", "TEST", 1, null);

            result.Should().NotBeNull();
            result.Valid.Should().BeTrue();
            result.Errors.Should().HaveCount(1);
        }
    }
}