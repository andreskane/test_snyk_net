using System;

namespace ABI.API.Structure.Application.DTO
{
    public class StructureV2DTO
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int StructureModelId { get; set; }

        public virtual DateTimeOffset? ValidityFrom { get; set; }

        public virtual bool? Erasable { get; set; }

        public virtual string FirstNodeName { get; set; }

        public virtual bool ThereAreChangesWithoutSaving { get; set; }

        public virtual bool ThereAreScheduledChanges { get; set; }

        public virtual bool Processing { get; set; }

        public StructureV2DTO()
        {
            //Devuelve true si se puede eliminar, o false si no se puede eliminar
            Erasable = true;
            ThereAreChangesWithoutSaving = false;
            ThereAreScheduledChanges = false;
        }
    }
}
