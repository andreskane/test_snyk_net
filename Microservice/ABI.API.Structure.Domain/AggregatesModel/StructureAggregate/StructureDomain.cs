using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Domain.Common;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureAggregate
{
    public class StructureDomain : EntityDomain, IAggregateRoot
    {
        public virtual string Name { get; set; }

        public int StructureModelId { get; private set; }

        public int? RootNodeId { get; private set; }

        public string Code { get; private set; }

        public DateTimeOffset? ValidityFrom { get; private set; }

        public virtual StructureModel StructureModel { get; set; }

        public virtual StructureNode Node { get; set; }

        public virtual ICollection<StructureArista> AristaFrom { get; set; }

        public virtual ICollection<StructureArista> AristaTo { get; set; }

        private readonly List<ChangeTracking> _ChangeTrackingListItem;
        public IReadOnlyCollection<ChangeTracking> ChangeTrackingListItems => _ChangeTrackingListItem;
        public StructureDomain()
        {
            _ChangeTrackingListItem = new List<ChangeTracking>();


        }

        public StructureDomain(int structureModelId, int? rootNodeId, DateTimeOffset? validityFrom) : this()
        {
            Name = "-";
            StructureModelId = structureModelId;
            RootNodeId = rootNodeId;
            ValidityFrom = validityFrom;
            Code = "-";

            AristaTo = new HashSet<StructureArista>();
            AristaFrom = new HashSet<StructureArista>();
        }

        public StructureDomain(string name, int structureModelId, int? rootNodeId, DateTimeOffset? validityFrom) : this()
        {
            Name = name;
            StructureModelId = structureModelId;
            RootNodeId = rootNodeId;
            ValidityFrom = validityFrom;
            Code = "-";

            AristaTo = new HashSet<StructureArista>();
            AristaFrom = new HashSet<StructureArista>();
        }

        public StructureDomain(string name, int structureModelId, int? rootNodeId, DateTimeOffset? validityFrom, string code):this()
        {
            Name = name;
            StructureModelId = structureModelId;
            RootNodeId = rootNodeId;
            ValidityFrom = validityFrom;
            Code = code;

            AristaTo = new HashSet<StructureArista>();
            AristaFrom = new HashSet<StructureArista>();
        }


        public StructureDomain(int structureId) : this()
        {
            Id = structureId;
            StructureModelId = 0;
            Name = "-";
            Code = "-";
            AristaTo = new HashSet<StructureArista>();
            AristaFrom = new HashSet<StructureArista>();
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetRootNodeId(int id)
        {
            RootNodeId = id;
        }

        public void SetRootNodeIdNull()
        {
            RootNodeId = null;
        }

        public void SetValidityFrom(DateTimeOffset? validityFrom)
        {
            ValidityFrom = validityFrom;
        }

        public void AddStructureAristaItems(StructureArista oStructureArista)
        {
            AristaFrom.Add(oStructureArista);

        }

        public void AddCode(string code)
        {
            Code = code;

        }

        public void AddId(int id)
        {
            Id = id;

        }
    }
}
