using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using Moq;
using System;

namespace ABI.API.Structure.ACL.TruckTests.Inits
{
    public static class InitCurrentUserService
    {
        public static ICurrentUserService GetDefaultUserService()
        {
            var userServiceMock = new Mock<ICurrentUserService>();
            userServiceMock.Setup(x => x.UserId).Returns(Guid.Empty);
            userServiceMock.Setup(x => x.UserName).Returns("TEST_USER");

            return userServiceMock.Object;
        }
    }
}
