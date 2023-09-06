using ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Extension
{
    public static class StructureModelExtensions
    {
        public static StructureModelDTO ToStructureModelDTO(this StructureModel structureModel)
        {
            var modelDTO = new StructureModelDTO
            {
                Active = structureModel.Active,
                CanBeExportedToTruck = structureModel.CanBeExportedToTruck,
                Description = structureModel.Description,
                Id = structureModel.Id,
                Name = structureModel.Name,
                ShortName = structureModel.ShortName
            };


            return modelDTO;

        }
    }
}
