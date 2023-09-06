using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.Extensions.Tests
{
    [TestClass()]
    public class DatesExtensionsTests
    {
        [TestMethod()]
        public void ToOffsetTest()
        {
            var date = new DateTimeOffset(new DateTime(2021, 1, 1));
            var result = date.ToOffset(-3);

            result.Offset.Should().Be(TimeSpan.FromHours(-3));
        }

        [TestMethod()]
        public void ToDateOffsetTest()
        {
            var date = "2021-01-01";
            var result = date.ToDateOffset(-3, "yyyy-MM-dd");

            result.Offset.Should().Be(TimeSpan.FromHours(-3));
        }

        [TestMethod()]
        public void ToDateOffsetTestDateTime()
        {
            var date = new DateTime(2021, 1, 1);
            var result = date.ToDateOffset(-3);

            result.Offset.Should().Be(TimeSpan.FromHours(-3));
        }

        [TestMethod()]
        public void ToDateOffsetUtcTest()
        {
            var date = "2021-01-01";
            var result = date.ToDateOffsetUtc(-3, "yyyy-MM-dd");

            result.Offset.Should().Be(TimeSpan.FromHours(0));
        }

        [TestMethod()]
        public void ToDateOffsetUtcTestDatetime()
        {
            var date = new DateTime(2021, 1, 1);
            var result = date.ToDateOffsetUtc(-3);

            result.Offset.Should().Be(TimeSpan.FromHours(0));
        }

        [TestMethod()]
        public void TodayTest()
        {
            var result = DateTimeOffset.UtcNow.Today();

            result.ToString("dd/MM/yyyy zzz").Should().Be(DateTimeOffset.UtcNow.ToString("dd/MM/yyyy zzz"));
        }
    }
}