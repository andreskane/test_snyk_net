using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Country
{
    public class GetAllOrderQuery : IRequest<IList<CountryDTO>>
    {
    }
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IList<CountryDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _repo;

        public GetAllOrderQueryHandler(IMapper mapper, ICountryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IList<CountryDTO>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var results = await _repo.GetAll();
            return _mapper.Map<IList<Domain.Entities.Country>, IList<CountryDTO>>(results);
        }
    }
}
