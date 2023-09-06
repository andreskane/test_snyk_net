
using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries
{
    public class GetAllImportProcessQuery : IRequest<IList<ImportProcessDTO>>
    {
    }

    public class GetAllImportProcessQueryHandler : IRequestHandler<GetAllImportProcessQuery, IList<ImportProcessDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IImportProcessRepository _repository;
        private readonly ILogger<GetAllImportProcessQueryHandler> _logger;
        public GetAllImportProcessQueryHandler(IMapper mapper, IImportProcessRepository repository, ILogger<GetAllImportProcessQueryHandler> logger)
       {
            _mapper = mapper;
           _repository = repository;
            _logger = logger;
       }
      
        public async Task<IList<ImportProcessDTO>> Handle(GetAllImportProcessQuery request, CancellationToken cancellationToken)=>
             _mapper.Map<IList<ImportProcessDTO>>(await _repository.GetAllAsync(cancellationToken));

           
        }
        
    
}
