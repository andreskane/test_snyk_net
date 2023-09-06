using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace ABI.API.Structure.Unit.Tests.Inits
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

        public void PersistInMemorySQLite(StructureContext context)
        {
            var inMemoryDb = (SqliteConnection)context.Database.GetDbConnection();
            inMemoryDb.Open();

            using (var fs = new SqliteConnection("Filename=physical.db"))
            {
                fs.Open();
                inMemoryDb.BackupDatabase(fs);
            }
        }

        public TruckACLContext CreateTruckACLContextForSQLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<TruckACLContext>()

                .UseSqlite(connection)

                .Options;

            var context = new TruckACLContext(option);

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

