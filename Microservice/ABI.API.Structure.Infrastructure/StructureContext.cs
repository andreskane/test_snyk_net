using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;

using ABI.API.Structure.Infrastructure.EntityConfigurations;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure
{
    public partial class StructureContext : DbContextBase<StructureContext>
    {
        private readonly ICurrentUserService _currentUserService;
        public const string DEFAULT_SCHEMA = "dbo";

        public StructureContext(DbContextOptions<StructureContext> options)
            : base(options)
        {
        }

        public StructureContext(DbContextOptions<StructureContext> options, IMediator mediator, ICurrentUserService currentUserService) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _currentUserService = currentUserService;
        }

        #region Schema DBO

        public virtual DbSet<AttentionModeRole> AttentionModeRole { get; set; }
        public virtual DbSet<AttentionMode> AttentionMode { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<StructureModel> StructureModels { get; set; }
        public virtual DbSet<StructureModelDefinition> ModelStructuresDefinitions { get; set; }
        public virtual DbSet<SaleChannel> SalesChannels { get; set; }
        public virtual DbSet<TypeGroup> TypeGroups { get; set; }
        public virtual DbSet<Domain.Entities.Type> Types { get; set; }
        public virtual DbSet<StructureDomain> Structures { get; set; }
        public virtual DbSet<StructureNode> StructureNodes { get; set; }
        public virtual DbSet<StructureArista> StructureAristas { get; set; }
        public virtual DbSet<StructureNodeDefinition> StructureNodeDefinitions { get; set; }
        public virtual DbSet<StructureClientNode> StructureClientNodes { get; set; }
       // public virtual DbSet<StateTruck> StatesTruck { get; set; }
        public virtual DbSet<ChangeTracking> ChangesTracking { get; set; }
        public virtual DbSet<ChangeTrackingStatus> ChangesTrackingStatus { get; set; }
        public virtual DbSet<Motive> Motives { get; set; }
        public virtual DbSet<MotiveState> MotiveStates { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<StateGroup> StateGroups { get; set; }
        public virtual DbSet<Country> Countrys { get; set; }
        public virtual DbSet<MostVisitedFilter> MostVisitedFilters { get; set; }




        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //StringsToUpper();
            AddAuditData();

            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        [ExcludeFromCodeCoverage]
        private void AddAuditData()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedByName = _currentUserService.UserName;
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModifiedByName = _currentUserService.UserName;
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                }
            }
        }


        [ExcludeFromCodeCoverage]
        private void StringsToUpper()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                foreach (var propInfo in entry.Entity.GetType().GetProperties())
                {
                    if (propInfo.PropertyType == typeof(string) &&
                        propInfo.CanWrite &&
                        propInfo.GetSetMethod(true).IsPublic)
                    {
                        var value = propInfo.GetValue(entry.Entity);
                        if (value != null)
                            propInfo.SetValue(entry.Entity, (value as string).ToUpper());
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Schema DBO
            modelBuilder.ApplyConfiguration(new LevelEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttentionModeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StructureModelEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StructureModelDefinitionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SaleChannelEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TypeGroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StructureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StructureNodeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StructureNodeAristaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StructureNodeDefinitionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttentionModeRoleEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new StateTruckEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChangeTrackingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChangeTrackingStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ObjectTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MotiveEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MotiveStateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StateGroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StructureClientNodeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CountryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MostVisitedFilterEntityTypeConfiguration());

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
    }
}
