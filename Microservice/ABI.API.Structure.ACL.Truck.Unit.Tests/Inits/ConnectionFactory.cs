using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Infrastructure.Publishers;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace ABI.API.Structure.ACL.TruckTests.Inits
{
    public class ConnectionFactory : IDisposable
    {
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public StructureContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<StructureContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .EnableSensitiveDataLogging(true)
                .Options;

            var context = new StructureContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public StructureContext CreateContextForSQLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<StructureContext>()

                .UseSqlite(connection)

                .Options;

            var context = new StructureContext(option);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public TruckACLContext CreateTruckACLContextForSQLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<TruckACLContext>()

                .UseSqlite(connection)

                .Options;

            var mockMediator = new Mock<IMediator>();
            var mockTruckAclPublisher = new Mock<ITruckAclPublisher>();
            var context = new TruckACLContext(option, mockMediator.Object, mockTruckAclPublisher.Object);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }

}

