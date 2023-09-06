using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public class StructureDTO
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int StructureModelId { get; set; }

        public virtual DateTimeOffset? Validity { get; set; }

        public virtual bool? Erasable { get; set; }

        public virtual string FirstNodeName { get; set; }

        public virtual StructureModelDTO StructureModel { get; set; }

        public virtual bool ThereAreChangesWithoutSaving { get; set; }
        public virtual bool ThereAreScheduledChanges { get; set; }

        public StructureDTO()
        {

            Erasable = true;
            ThereAreChangesWithoutSaving = false;
            ThereAreScheduledChanges = false;
        }
    }
}
