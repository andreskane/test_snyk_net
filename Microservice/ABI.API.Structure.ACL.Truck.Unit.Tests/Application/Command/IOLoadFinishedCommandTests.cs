using ABI.API.Structure.ACL.Truck.Application.Commands.ImportProcess;
using Coravel.Queuing.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Command
{
    [TestClass()]
    public class IOLoadFinishedCommandTests
    {
        private readonly IQueue _queue;

        [TestMethod()]
        public void IOLoadFinishedCommandInitTest()
        {
            var result = new IOLoadFinishedCommand { 
                ProccessId = 1,
                RowsCount = 0,
                State = Domain.Enums.ImportProcessState.Pending
            };
            result.Should().NotBeNull();
            result.ProccessId.Should().Be(1);
            result.RowsCount.Should().Be(0);
            result.State.Should().Be(Domain.Enums.ImportProcessState.Pending);
        }

        [TestMethod()]
        public void IOLoadFinishedCommandHandlerTest()
        {
            var result = new IOLoadFinishedCommandHandler(_queue);
            result.Should().NotBeNull();
        }
    }
}
