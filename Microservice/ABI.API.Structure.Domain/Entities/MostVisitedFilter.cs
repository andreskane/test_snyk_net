using System;

using ABI.Framework.MS.Entity;

namespace ABI.API.Structure.Domain.Entities
{
    public class MostVisitedFilter : IEntity<int>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int IdValue { get; set; }
        public int FilterType { get; set; }//Area = 1; Region =2; Subregion = 3; Persona =4; ModoAtencion = 5; Rol = 6; Canal = 7
        public int Quantity { get; set; }
        public int StructureId { get; private set; }
        public Guid UserId { get; private set; }

        public MostVisitedFilter()
        {

        }

        public void EditUser(Guid userId)
        {
            UserId = userId;
        }
        public void EditStructureId(int structureId)
        {
            StructureId = structureId;
        }
        public void AddQuantity()
        {
            Quantity++;
        }
    }
}
