using ABI.API.Structure.Application.DTO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.DTO
{
    [TestClass()]
    public class UserDTOTests
    {
        [TestMethod()]
        public void UserDTOTest()
        {
            var result = new UserDTO();
            result.Name = "TEST";
            result.Id = new System.Guid("3e8fa66e-3619-48f0-9f0b-60500005d7ef");

            result.Should().NotBeNull();
            result.Name.Should().Be("TEST");
            result.Id.Should().Be("3e8fa66e-3619-48f0-9f0b-60500005d7ef");
        }
    }
}
