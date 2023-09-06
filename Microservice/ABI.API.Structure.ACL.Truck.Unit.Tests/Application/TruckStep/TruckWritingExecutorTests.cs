using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.TruckStep.Tests
{
    [TestClass()]
    public class TruckWritingExecutorTests
    {
        [TestMethod()]
        public void TruckWritingExecutorTest()
        {
            var result = new TruckWritingExecutor(null, null, null);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void InvokeTest()
        {
            //var loggerMock = new Mock<ILogger<TruckWritingExecutor>>();
            //var dispatcherMock = new Mock<IDispatcher>();
            //var medietorMock = new Mock<IMediator>();

            //var executor = new TruckWritingExecutor(loggerMock.Object, dispatcherMock.Object, medietorMock.Object);

            //executor.Payload = new TruckWritingPayload
            //{
            //    StructureId = 1,
            //    StructureName = "TEST",
            //    Date = DateTimeOffset.MinValue,
            //    Username = "TEST"
            //};

            //executor.Invoking(async i => await i.Invoke())
            //    .Should().NotThrowAsync();

            //dispatcherMock.Verify(v => v.Broadcast(It.IsAny<TruckWritingEventDone>()));
            Assert.Inconclusive();
        }

        [TestMethod()]
        public async Task InvokeThrowsInternalTest()
        {
            //var loggerMock = new Mock<ILogger<TruckWritingExecutor>>();
            //var structureAdapterMock = new Mock<IStructureAdapter>();
            //var dispatcherMock = new Mock<IDispatcher>();
            //var medietorMock = new Mock<IMediator>();

            //structureAdapterMock
            //    .Setup(s => s.StructurePortalToTruckChanges(It.IsAny<int>(), It.IsAny<DateTime>()))
            //    .ThrowsAsync(new Exception());

            //var executor = new TruckWritingExecutor(loggerMock.Object, dispatcherMock.Object, medietorMock.Object);

            //executor.Payload = new TruckWritingPayload
            //{
            //    StructureId = 1,
            //    StructureName = "TEST",
            //    Date = DateTimeOffset.MinValue,
            //    Username = "TEST"
            //};

            //await executor.Invoke();

            //dispatcherMock.Verify(v => v.Broadcast(It.IsAny<TruckWritingEventError>()));
            Assert.Inconclusive();
        }
    }
}