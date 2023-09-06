using ABI.Framework.MS.Entity;
using System;

namespace ABI.API.Structure.Domain.Entities
{
    public class ChangeTrackingStatus : IEntity<int>
    {
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public int IdChangeTracking { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public ChangeTracking ChangeTracking { get; set; }

        public virtual State Status { get; set; }
    }
}
