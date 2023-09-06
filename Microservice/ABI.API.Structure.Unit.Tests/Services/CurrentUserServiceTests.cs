using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;

namespace ABI.API.Structure.Application.Services.Tests
{
    [TestClass()]
    public class CurrentUserServiceTests
    {
        [TestMethod()]
        public void CurrentUserServiceTest()
        {
            var principalMock = new Mock<ClaimsPrincipal>();
            principalMock
                .Setup(s => s.FindFirst(It.Is<string>(p => p.Equals("name"))))
                .Returns(new Claim("name", "TEST"));
            principalMock
                .Setup(s => s.FindFirst(It.Is<string>(p => p.Equals("http://schemas.microsoft.com/identity/claims/objectidentifier"))))
                .Returns(new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", Guid.Empty.ToString()));

            var contextMock = new Mock<HttpContext>();
            contextMock
                .Setup(s => s.User)
                .Returns(principalMock.Object);

            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock
                .Setup(s => s.HttpContext)
                .Returns(contextMock.Object);

            var result = new CurrentUserService(accessorMock.Object);
            result.Should().NotBeNull();
            result.UserName.Should().Be("TEST");
            result.UserId.Should().Be(Guid.Empty.ToString());
        }

        [TestMethod()]
        public void CurrentUserServiceNullTest()
        {
            var principalMock = new Mock<ClaimsPrincipal>();
            var contextMock = new Mock<HttpContext>();
            contextMock
                .Setup(s => s.User)
                .Returns(principalMock.Object);

            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock
                .Setup(s => s.HttpContext)
                .Returns(contextMock.Object);

            var result = new CurrentUserService(accessorMock.Object);
            result.Should().NotBeNull();
            result.UserName.Should().BeNull();
            result.UserId.Should().Be(Guid.Empty.ToString());
        }
    }
}