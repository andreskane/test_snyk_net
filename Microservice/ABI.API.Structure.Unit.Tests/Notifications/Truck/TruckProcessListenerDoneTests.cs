using ABI.API.Structure.ACL.Truck.Application.TruckStep;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Services.Interfaces;
using ABI.API.Structure.Unit.Tests.Mock;
using ABI.Framework.MS.Helpers.Response;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Notifications.Truck.Tests
{
    [TestClass()]
    public class TruckProcessListenerDoneTests
    {
        private static Mock<INotificationStatusService> _notificationServiceMock;
        private static  Mock<IVersionedRepository> _VersionedMock;
        private static Mock<IVersionedLogStatusRepository> _versionedLogStatus;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _notificationServiceMock = new Mock<INotificationStatusService>();
            _VersionedMock = new Mock<IVersionedRepository>();
            _versionedLogStatus = new Mock<IVersionedLogStatusRepository>();

            _VersionedMock
                .Setup(s => s.Filter(It.IsAny<Expression<Func<Versioned, bool>>>()))
                .Returns(new List<Versioned> { new Versioned { Id = 1 } }.AsQueryable());

            _versionedLogStatus
                .Setup(s => s.GetAll())
                .ReturnsAsync(new List<VersionedLogStatus> { new VersionedLogStatus { Id = 100, Name = "Error" } });
        }


        [TestMethod()]
        public void TruckProcessListenerDoneTest()
        {
            var result = new TruckProcessListenerDone(null, null, null, null);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task HandleAsyncDoneTest()
        {
            var versionedLogMock = new Mock<IVersionedLogRepository>();

            versionedLogMock
                .Setup(s => s.Filter(It.IsAny<Expression<Func<VersionedLog, bool>>>()))
                .Returns(new List<VersionedLog> { new VersionedLog { LogStatusId = 1 } }.AsQueryable());

            var listener = new TruckProcessListenerDone(
                _notificationServiceMock.Object,
                _VersionedMock.Object,
                versionedLogMock.Object,
                _versionedLogStatus.Object
            );

            var broadcast = new TruckWritingEventDone
            {
                StructureId = 1,
                StructureName = "TEST",
                Date = DateTimeOffset.MinValue,
                Username = "TEST"
            };

            await listener.HandleAsync(broadcast);

            _notificationServiceMock.Verify(v =>
                v.Notify(It.IsAny<string>(), It.Is<GenericResponse>(w => w.Type == "FINALIZADO_SIN_ERRORES"))
            );
        }

        [TestMethod()]
        public async Task HandleAsyncFailTest()
        {

            var versionedLogMock = new Mock<IVersionedLogRepository>();

            versionedLogMock
                .Setup(s => s.Filter(It.IsAny<Expression<Func<VersionedLog, bool>>>()))
                .Returns(new List<VersionedLog> { new VersionedLog { LogStatusId = 100 } }.AsQueryable());


            var listener = new TruckProcessListenerDone(
                _notificationServiceMock.Object,
                _VersionedMock.Object,
                versionedLogMock.Object,
                _versionedLogStatus.Object
            );

            var broadcast = new TruckWritingEventDone
            {
                StructureId = 1,
                StructureName = "TEST",
                Date = DateTimeOffset.MinValue,
                Username = "TEST"
            };

            await listener.HandleAsync(broadcast);

            _notificationServiceMock.Verify(v =>
                v.Notify(It.IsAny<string>(), It.Is<GenericResponse>(w => w.Type == "FINALIZADO_CON_ERRORES"))
            );
        }

        [TestMethod()]
        public async Task HandleAsyncFailDefaultMessageTest()
        {
            var versionedLogMock = new Mock<IVersionedLogRepository>();

            versionedLogMock
                .Setup(s => s.Filter(It.IsAny<Expression<Func<VersionedLog, bool>>>()))
                .Returns(new List<VersionedLog> { new VersionedLog { LogStatusId = 101 } }.AsQueryable());


            var listener = new TruckProcessListenerDone(
                _notificationServiceMock.Object,
                _VersionedMock.Object,
                versionedLogMock.Object,
                _versionedLogStatus.Object
            );

            var broadcast = new TruckWritingEventDone
            {
                StructureId = 1,
                StructureName = "TEST",
                Date = DateTimeOffset.MinValue,
                Username = "TEST"
            };

            await listener.HandleAsync(broadcast);

            _notificationServiceMock.Verify(v =>
                v.Notify(It.IsAny<string>(), It.Is<GenericResponse>(w => w.Type == "FINALIZADO_CON_ERRORES"))
            );
        }

        [TestMethod()]
        public async Task HandleAsyncFailTruckMessagesTest()
        {
            var versionedLogRecords = FactoryMock.GetMockJson<IList<VersionedLog>>(
                Path.Combine("MockFile", "ACL.VersionedLog.json")
            );
            var impactStatusRecord = versionedLogRecords.First(f => f.LogStatusId == 106);
            var logImpacTruckStatusMock = new Mock<IVersionedLogRepository>();

            logImpacTruckStatusMock
                .Setup(s => s.Filter(It.IsAny<Expression<Func<VersionedLog, bool>>>()))
                .Returns(new List<VersionedLog> { impactStatusRecord }.AsQueryable());

            var listener = new TruckProcessListenerDone(
                _notificationServiceMock.Object,
                _VersionedMock.Object,
                logImpacTruckStatusMock.Object,
                _versionedLogStatus.Object
            );

            var broadcast = new TruckWritingEventDone
            {
                StructureId = 1,
                StructureName = "TEST",
                Date = DateTimeOffset.MinValue,
                Username = "TEST"
            };

            await listener.HandleAsync(broadcast);

            _notificationServiceMock.Verify(v =>
                v.Notify(It.IsAny<string>(), It.Is<GenericResponse>(w =>
                    w.Type == "FINALIZADO_CON_ERRORES" &&
                    w.Messages.Any()
                ))
            );
        }
    }
}