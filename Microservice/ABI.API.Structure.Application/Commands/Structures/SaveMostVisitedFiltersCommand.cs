using System.Threading;
using System.Threading.Tasks;

using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;

using AutoMapper;

using MediatR;

namespace ABI.API.Structure.Application.Commands.Structures
{
    public class SaveMostVisitedFiltersCommand : MostVisitedFilterDto, IRequest
    {
        public string StructureCode { get; set; }
    }
    public class SaveMostVisitedFiltersCommandHandler : IRequestHandler<SaveMostVisitedFiltersCommand>
    {
        private readonly IMapper _mapper;
        private readonly IMostVisitedFilterRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStructureRepository _structureRepository;

        public SaveMostVisitedFiltersCommandHandler(IMostVisitedFilterRepository repo, IMapper mapper, IStructureRepository structureRepository, ICurrentUserService currentUserService)
        {
            _repository = repo;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _structureRepository = structureRepository;
        }

        public async Task<Unit> Handle(SaveMostVisitedFiltersCommand request, CancellationToken cancellationToken)
        {
            var structure = await _structureRepository.GetStructureByCodeAsync(request.StructureCode);

            if (structure == null)
                return Unit.Value;

            var entity = _mapper.Map<MostVisitedFilter>(request);
            entity.EditUser(_currentUserService.UserId);
            entity.EditStructureId(structure.Id);
            entity.AddQuantity();

            var visitedFilter = await _repository.GetByUserStructureAndValue(request.Name, request.FilterType, entity.UserId, entity.StructureId, cancellationToken);
            if (visitedFilter != null)
            {
                visitedFilter.AddQuantity();
                await _repository.Update(visitedFilter, cancellationToken);
            }
            else
                await _repository.Create(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
