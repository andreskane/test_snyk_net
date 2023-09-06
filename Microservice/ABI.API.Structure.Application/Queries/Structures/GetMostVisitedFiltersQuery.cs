using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;

using AutoMapper;

using MediatR;

namespace ABI.API.Structure.Application.Queries.Structures
{
    public class GetMostVisitedFiltersQuery : IRequest<IList<MostVisitedFilterDto>>
    {
        public string StructureCode { get; set; }
    }
    public class GetMostVisitedFiltersQueryHandler : IRequestHandler<GetMostVisitedFiltersQuery, IList<MostVisitedFilterDto>>
    {
        private readonly IMostVisitedFilterRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStructureRepository _structureRepository;
        private readonly IMapper _mapper;

        public GetMostVisitedFiltersQueryHandler(
            IMostVisitedFilterRepository repository,
            ICurrentUserService currentUserService,
            IStructureRepository structureRepository,
            IMapper mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _structureRepository = structureRepository;
            _mapper = mapper;
        }

        public async Task<IList<MostVisitedFilterDto>> Handle(GetMostVisitedFiltersQuery request, CancellationToken cancellationToken)
        {
            var structure = await _structureRepository.GetStructureByCodeAsync(request.StructureCode);

            if (structure == null)
                return new List<MostVisitedFilterDto>();

            var listMostVisited = await _repository.GetByUserAndStructureOrder(_currentUserService.UserId, structure.Id, 5, cancellationToken);

            return _mapper.Map<IList<Domain.Entities.MostVisitedFilter>, IList<MostVisitedFilterDto>>(listMostVisited);
        }
    }
}
