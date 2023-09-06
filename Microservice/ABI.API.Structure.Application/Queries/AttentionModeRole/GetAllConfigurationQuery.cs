using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.Service.Models;
using ABI.API.Structure.Application.DTO;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.AttentionModeRole
{
    public class GetAllConfigurationQuery : IRequest<IList<AttentionModeRoleConfigurationDTO>>
    {
    }

    public class GetAllConfigurationQueryHandler : IRequestHandler<GetAllConfigurationQuery, IList<AttentionModeRoleConfigurationDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ITypeVendorTruckService _service;

        public GetAllConfigurationQueryHandler(IMapper mapper, ITypeVendorTruckService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<IList<AttentionModeRoleConfigurationDTO>> Handle(GetAllConfigurationQuery request, CancellationToken cancellationToken)
        {
            var results = await _service.GetAllConfiguration();
            return _mapper.Map<IList<AttentionModelRolTypeVender>, IList<AttentionModeRoleConfigurationDTO>>(results);
        }
    }
}
