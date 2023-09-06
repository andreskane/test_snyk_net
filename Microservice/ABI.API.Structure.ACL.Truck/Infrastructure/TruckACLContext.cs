using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations;
using ABI.API.Structure.ACL.Truck.Infrastructure.Publishers;
using ABI.Framework.MS.Infrastructure;
using ABI.Framework.MS.Messages.EventBus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public partial class TruckACLContext : DbContextBase<TruckACLContext>
    {
        public const string ACL_SCHEMA = "acl";

        private readonly ITruckAclPublisher _truckAclPublisher;


        public TruckACLContext(DbContextOptions<TruckACLContext> options) : base(options) { }

        public TruckACLContext(DbContextOptions<TruckACLContext> options, IMediator mediator, ITruckAclPublisher truckAclPublisher) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _truckAclPublisher = truckAclPublisher;
        }


        #region Schema ACL
        public virtual DbSet<LevelTruckPortal> LevelTruckPortals { get; set; }
        public virtual DbSet<BusinessTruckPortal> BusinessTruckPortals { get; set; }
        public virtual DbSet<TypeVendorTruck> TypeVendorsTruck { get; set; }
        public virtual DbSet<TypeVendorTruckPortal> TypeVendorsTruckPortal { get; set; }

        public virtual DbSet<Versioned> Versioneds { get; set; }
        public virtual DbSet<VersionedArista> VersionedsArista { get; set; }
        public virtual DbSet<VersionedNode> VersionedsNode { get; set; }
        public virtual DbSet<VersionedLog> VersionedsLog { get; set; }
        public virtual DbSet<VersionedLogStatus> VersionedsLogStatus { get; set; }
        public virtual DbSet<VersionedStatus> VersionedsStatus { get; set; }
        public virtual DbSet<ResourceResponsable> ResourcesResponsable { get; set; }



        public virtual DbSet<ImportProcess> ImportProcessDBSet { get; set; }
        public virtual DbSet<PeriodicityDays> PeriodicityDaysDBSet { get; set; }
        public virtual DbSet<EstructuraClienteTerritorioIO> EstructuraClienteTerritorioIODBSet { get; set; }

        public virtual DbSet<SyncLog> SyncLogs { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var handleHResourcesChanges = HandleHResourceChanges();

            await Task.WhenAll(handleHResourcesChanges);

            var result = await base.SaveChangesAsync(cancellationToken);

            await Task.WhenAll(
                BroadcastNewHResources(handleHResourcesChanges.Result)
            );

            return result;
        }


        //private void AddAuditData()
        //{
        //    foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedBy = _currentUserService.UserId;
        //                entry.Entity.CreatedByName = _currentUserService.UserName;
        //                entry.Entity.CreatedDate = DateTime.UtcNow;
        //                break;
        //            case EntityState.Modified:
        //                entry.Entity.LastModifiedBy = _currentUserService.UserId;
        //                entry.Entity.LastModifiedByName = _currentUserService.UserName;
        //                entry.Entity.LastModifiedDate = DateTime.UtcNow;
        //                break;
        //        }
        //    }
        //}

        #endregion

        #region SQLite DateTimeOffset Fix

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Schema ACL
            modelBuilder.ApplyConfiguration(new LevelTruckPortalEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessTruckPortalEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TypeVendorTruckEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TypeVendorTruckPortalEntityTypeConfiguration());



            modelBuilder.ApplyConfiguration(new EstructuraClienteTerritorioIOConfiguration());
            modelBuilder.ApplyConfiguration(new PeriodicityDaysConfiguration());
            modelBuilder.ApplyConfiguration(new ImportProcessConfiguration());


            modelBuilder.ApplyConfiguration(new VersionedEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VersionedAristaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VersionedLogEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VersionedLogStateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VersionedNodeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VersionedStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SyncLogEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceResponsableEntityTypeConfiguration());
            #endregion

            //
            // https://blog.dangl.me/archive/handling-datetimeoffset-in-sqlite-with-entity-framework-core/
            //
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
                // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
                // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
                // use the DateTimeOffsetToBinaryConverter
                // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
                // This only supports millisecond precision, but should be sufficient for most use cases.
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset)
                                                                                || p.PropertyType == typeof(DateTimeOffset?));
                    foreach (var property in properties)
                    {
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }

        #endregion

        #region EventBus

        private async Task<IList<SyncLog>> HandleHResourceChanges()
        {
            var newEntities = ChangeTracker.Entries<SyncLog>()
                .Where(x => x.State == EntityState.Added)
                .Select(s => s.Entity)
                .ToList();

            return await Task.FromResult(newEntities);
        }

        private async Task BroadcastNewHResources(IList<SyncLog> entities)
        {
            if (!entities.Any() || _truckAclPublisher == null)
                return;

            await _truckAclPublisher.Publish(entities, ActionTriggered.Create);
        }

        #endregion
    }
}
