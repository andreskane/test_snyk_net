using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Domain.Common;
using System;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureAggregate
{
    public class StructureArista : EntityDomain
    {
        public int StructureIdFrom { get; private set; }
        public int NodeIdFrom { get; private set; }
        public int StructureIdTo { get; private set; }
        public int NodeIdTo { get; private set; }
        public int TypeRelationshipId { get; private set; }
        public DateTimeOffset ValidityFrom { get; private set; }
        public DateTimeOffset ValidityTo { get; private set; }

        public virtual StructureDomain StructureTo { get; set; }

        public virtual StructureDomain StructureFrom { get; set; }

        public virtual StructureNode NodeTo { get; set; }

        public virtual StructureNode NodeFrom { get; set; }

        public virtual Domain.Entities.Type TypeRelationship { get; set; }

        public int MotiveStateId { get; private set; }
        public MotiveState MotiveState { get; set; }

        public StructureArista() { }

        public StructureArista(int structureIdFrom, int nodeIdFrom, int structureIdTo, int nodeIdTo, int typeRelationshipId, DateTimeOffset validityFrom)
        {
            StructureIdFrom = structureIdFrom;
            NodeIdFrom = nodeIdFrom;
            StructureIdTo = structureIdTo;
            NodeIdTo = nodeIdTo;
            TypeRelationshipId = typeRelationshipId;
            ValidityFrom = validityFrom;
            ValidityTo = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)); //HACER: Ojo multipaís
        }

        public StructureArista(StructureDomain structureIdFrom, StructureNode nodeIdFrom, StructureDomain structureIdTo, StructureNode nodeIdTo, int typeRelationshipId, DateTimeOffset validityFrom, DateTimeOffset validityTo)
        {
            this.StructureFrom = structureIdFrom;
            this.NodeFrom = nodeIdFrom;
            this.StructureTo = structureIdTo;
            this.NodeTo = nodeIdTo;
            TypeRelationshipId = typeRelationshipId;
            ValidityFrom = validityFrom;
            ValidityTo = validityTo;
        }

        public StructureArista(int structureIdFrom, StructureNode nodeIdFrom, int structureIdTo, StructureNode nodeIdTo, int typeRelationshipId, DateTimeOffset validityFrom, DateTimeOffset validityTo)
        {
            this.StructureIdFrom = structureIdFrom;
            this.NodeFrom = nodeIdFrom;
            this.StructureIdTo = structureIdTo;
            this.NodeTo = nodeIdTo;
            TypeRelationshipId = typeRelationshipId;
            ValidityFrom = validityFrom;
            ValidityTo = validityTo;
        }

        public StructureArista(int structureIdFrom, int nodeIdFrom, int structureIdTo, StructureNode nodeIdTo, int typeRelationshipId, DateTimeOffset validityFrom, DateTimeOffset validityTo)
        {
            this.StructureIdFrom = structureIdFrom;
            this.NodeIdFrom = nodeIdFrom;
            this.StructureIdTo = structureIdTo;
            this.NodeTo = nodeIdTo;
            TypeRelationshipId = typeRelationshipId;
            ValidityFrom = validityFrom;
            ValidityTo = validityTo;
        }

        public StructureArista(int structureIdFrom, int nodeIdFrom, int structureIdTo, int nodeIdTo, int typeRelationshipId, DateTimeOffset validityFrom, DateTimeOffset validityTo)
        {
            this.StructureIdFrom = structureIdFrom;
            this.NodeIdFrom = nodeIdFrom;
            this.StructureIdTo = structureIdTo;
            this.NodeIdTo = nodeIdTo;
            TypeRelationshipId = typeRelationshipId;
            ValidityFrom = validityFrom;
            ValidityTo = validityTo;
        }

        public void SetNodeParent(int id)
        {
            NodeIdFrom = id;
        }

        public void EditValidityFrom(DateTimeOffset validityFrom)
        {
            ValidityFrom = validityFrom;
        }

        public void EditValidityTo(DateTimeOffset validityTo)
        {
            ValidityTo = validityTo;
        }

        public void EditMotiveStateId(int motiveStateId)
        {
            MotiveStateId = motiveStateId;
        }
    }
}
