using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureClient
{
    public class GetAllClientQuery : IRequest<IList<DTO.StructureClientDTO>>
    {
        public string StructureCode { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }

    }

    public class GetAllClientQueryHandler : IRequestHandler<GetAllClientQuery, IList<DTO.StructureClientDTO>>
    {
        private readonly IStructureClientRepository _structureClientRepository;
        private readonly IStructureRepository _structureRepository;
        private readonly IMapper _mapper;

        public GetAllClientQueryHandler(IStructureClientRepository structureClientRepository, IStructureRepository structureRepository, IMapper mapper)
        {
            _structureClientRepository = structureClientRepository;
            _structureRepository = structureRepository;
            _mapper = mapper;


        }

        public async Task<IList<DTO.StructureClientDTO>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
        {

            var structure = await _structureRepository.GetStructureByCodeAsync(request.StructureCode);

            if (structure != null)
            {
                var clients = await _structureClientRepository.GetAllByStructureId(structure.Id, request.ValidityFrom, cancellationToken);


                return  _mapper.Map<IList<DTO.StructureClientDTO>>(clients);
            }

            return new List<DTO.StructureClientDTO>();
        }
    }
}
