using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Infrastructure.ApiUris.Tests
{
    [TestClass()]
    public class DBUHResourceTests
    {
        [TestMethod()]
        public void GetAllDBUHResourceTest()
        {
            var result = ApiUri.DBUHResource.GetAllDBUHResource(string.Empty);
            result.Should().NotBeNullOrEmpty();
        }
    }
}