using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Extension;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.StructureModel
{
    public class GetStructureModelByIdQuery : IRequest<StructureModelDTO>
    {
        public int StructureModelId { get; set; }
    }

    public class GetStructureModelByIdQueryHandler : IRequestHandler<GetStructureModelByIdQuery, StructureModelDTO>
    {
        private readonly StructureContext _context;

        public GetStructureModelByIdQueryHandler()
        {
        }

        public GetStructureModelByIdQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<StructureModelDTO> Handle(GetStructureModelByIdQuery request, CancellationToken cancellationToken)
        {
            var structureModel = (from s in _context.StructureModels.AsNoTracking().Include(s => s.Country)
                             where s.Id == request.StructureModelId
                             select new StructureModelDTO
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 ShortName = s.ShortName,
                                 Description = s.Description,
                                 Code = s.Code,
                                 Active = s.Active,
                                 CanBeExportedToTruck = s.CanBeExportedToTruck,
                                 CountryId = s.CountryId,
                                 Country = s.Country.ToCountryDTO()
                             }).FirstOrDefault();

            return await Task.Run(() => structureModel);
        }
    }
}
