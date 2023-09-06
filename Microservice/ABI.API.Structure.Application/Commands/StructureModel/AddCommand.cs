using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.Application.Commands.StructureModel
{
    public class AddCommand : StructureModelDTO, IRequest<int>
    {
    }

    public class AddCommandHandler : IRequestHandler<AddCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelRepository _structureModelRepo;
        private readonly IStructureModelDefinitionRepository _structureModelDefinitionRepo;

        public AddCommandHandler(IStructureModelRepository repo, IMapper mapper, IStructureModelDefinitionRepository structureModelDefinitionRepo)
        {
            _structureModelRepo = repo;
            _mapper = mapper;
            _structureModelDefinitionRepo = structureModelDefinitionRepo;
        }

        public async Task<int> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<E.StructureModel>(request);  
            await _structureModelRepo.Create(entity);

            if (request.StructureModelSourceId.HasValue)
            {
                int? idnew = null;

                var structureModel = await _structureModelDefinitionRepo.GetAllByStructureModel(request.StructureModelSourceId.Value);

                foreach (var item in structureModel)
                {

                    var itemNew = new E.StructureModelDefinition
                    {
                        Id = 0,
                        StructureModelId = entity.Id,
                        LevelId = item.LevelId,
                        IsAttentionModeRequired = item.IsAttentionModeRequired,
                        IsSaleChannelRequired = item.IsSaleChannelRequired
                    };

                    await _structureModelDefinitionRepo.Create(itemNew);
                    itemNew.ParentLevelId = idnew;
                    await _structureModelDefinitionRepo.Update(itemNew);
                    idnew = itemNew.LevelId;

                }
            }

            return entity.Id;
        }
    }
}
