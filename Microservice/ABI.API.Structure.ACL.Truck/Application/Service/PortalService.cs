using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Extension;
using ABI.API.Structure.ACL.Truck.Application.Exceptions;
using ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service
{
    public class PortalService : IPortalService
    {

        private readonly IStructureRepository _structureRepository;
        private readonly IStructureNodeRepository _structureNodeRepository;
        private readonly IMediator _mediator;


        public PortalService(IStructureRepository structureRepository, IStructureNodeRepository structureNodeRepository, IMediator mediator)
        {
            _structureRepository = structureRepository ?? throw new ArgumentNullException(nameof(structureRepository));
            _structureNodeRepository = structureNodeRepository ?? throw new ArgumentNullException(nameof(structureNodeRepository));
            _mediator = mediator;
        }


        /// <summary>
        /// Gets all compare by structure identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="active">The active.</param>
        /// <returns></returns>
        /// <exception cref="SerciceException"></exception>
        public async Task<StructureNodeDTO> GetAllCompareByStructureId(string structureName, bool? active)
        {

            var structureNode = new StructureNodeDTO();

            try
            {
                var structure = await _structureRepository.GetStructureDataCompleteByNameAsync(structureName);

                var result = await _mediator.Send(new GetStructureComparePortalQuery { StructureId = structure.Id, ValidityFrom = DateTimeOffset.UtcNow.Date, Active = active });

                if (result.ToList().Count == 0)
                    return structureNode;

                return StructureNodeExtensions.ToStructureNodeDTO(result.ToList(), null, structure, active, false);

            }
            catch (Exception ex)
            {
                throw new GenericException(ex.Message, ex);
            }

        }

        public async Task<StructureDomain> GetStrucureByName(string structureName)
        {

            var structureNode = new StructureNodeDTO();

            try
            {
                return await _structureRepository.GetStructureDataCompleteByNameAsync(structureName);

            }
            catch (Exception ex)
            {
                throw new GenericException(ex.Message, ex);
            }

        }

    }
}
