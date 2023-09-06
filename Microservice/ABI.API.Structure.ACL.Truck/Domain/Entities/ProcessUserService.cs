using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using Moq;
using System;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public static class ProcessUserService
    {
        public static ICurrentUserService GetDefaultUserService()
        {
            var userServiceMock = new Mock<ICurrentUserService>();
            userServiceMock.Setup(x => x.UserId).Returns(Guid.Empty);
            userServiceMock.Setup(x => x.UserName).Returns("TRUCK_USER");

            return userServiceMock.Object;
        }
    }
}
