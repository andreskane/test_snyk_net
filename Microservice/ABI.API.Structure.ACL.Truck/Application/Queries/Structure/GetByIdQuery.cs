using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Extension;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.Structure
{
    public class GetByIdQuery : IRequest<StructureDTO>
    {
        public int StructureId { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, StructureDTO>
    {
        private readonly StructureContext _context;

        public GetByIdQueryHandler()
        {
        }

        public GetByIdQueryHandler(StructureContext context)
        {
            _context = context;
        }

        public async Task<StructureDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var structure = (from s in _context.Structures.AsNoTracking().Include(s => s.StructureModel)
                             where s.Id == request.StructureId
                             select new StructureDTO
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 StructureModelId = s.StructureModelId,
                                 Validity = s.ValidityFrom,
                                 StructureModel = s.StructureModel.ToStructureModelDTO()
                             }).FirstOrDefault();

            return await Task.Run(() => structure);
        }
    }
}
