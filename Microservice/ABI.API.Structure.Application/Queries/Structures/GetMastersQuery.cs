using ABI.API.Structure.Application.DTO;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structures
{
    public class GetMastersQuery : IRequest<MasterDTO>
    {
    }
    public class GetMastersQueryHandler : IRequestHandler<GetMastersQuery, MasterDTO>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetMastersQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<MasterDTO> Handle(GetMastersQuery request, CancellationToken cancellationToken)
        {
            MasterDTO result = new MasterDTO();

            var structureModels = await _mediator.Send(new StructureModels.GetAllOrderV2Query());
            if (structureModels != null)
            {
                foreach(var item in structureModels)
                {
                    item.Definitions = (List<StructureModelDefinitionV2DTO>)await _mediator.Send(new StructureModelDefinition.GetAllByStructureModelV2Query { Id = item.Id.Value });
                    result.structureModels.Add(item);
                }
            }
            result.roles = (List<RoleDTO>)await _mediator.Send(new Role.GetAllOrderQuery());

            var saleChannels = await _mediator.Send(new SaleChannel.GetAllOrderQuery());
            if (saleChannels != null)
                result.saleChannels = (List<ItemDTO>)_mapper.Map<IList<SaleChannelDTO>, IList<ItemDTO>>(saleChannels);

            var attentionModes = await _mediator.Send(new AttentionMode.GetAllOrderQuery());
            if (attentionModes != null)
                result.attentionModes = (List<ItemDTO>)_mapper.Map<IList<AttentionModeDTO>, IList<ItemDTO>>(attentionModes);

            var levels = await _mediator.Send(new Level.GetAllOrderQuery());
            if (levels != null)
                result.levels = (List<ItemDTO>)_mapper.Map<IList<LevelDTO>, IList<ItemDTO>>(levels);

            return result;
        }
    }
}
