using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;

namespace ABI.API.Structure.Application.Validations
{

    public class CreateNodeCheckVacantEmployeeCommandValidator : AbstractValidator<CreateNodeCommand>
    {
        private readonly IDBUHResourceRepository _dBUHResourceRepository;
        private readonly IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private readonly IMediator _mediator;

        public CreateNodeCheckVacantEmployeeCommandValidator(ILogger<CreateNodeCheckVacantEmployeeCommandValidator> logger
                                                            , IDBUHResourceRepository dBUHResourceRepository
                                                            , IMapeoTableTruckPortal mapeoTableTruckPortal
                                                            , IMediator mediator
                                                            )
        {

            _dBUHResourceRepository = dBUHResourceRepository ?? throw new ArgumentNullException(nameof(dBUHResourceRepository));
            _mapeoTableTruckPortal = mapeoTableTruckPortal ?? throw new ArgumentNullException(nameof(mapeoTableTruckPortal));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            RuleFor(command => command).Must(ValidCheckVacant)
            .OnAnyFailure(x =>
            {
                throw new CheckVacantEmployeeException();
            });

        }

        private bool ValidCheckVacant(CreateNodeCommand request)
        {
            var value = true;

            if (request.RoleId.HasValue && !request.EmployeeId.HasValue)
            {
                var structure = _mediator.Send(new GetStructureDomainQuery { StructureId = request.StructureId }).GetAwaiter().GetResult();

                var truck = _mapeoTableTruckPortal.GetOneBusinessTruckPortalByName(structure.Name).GetAwaiter().GetResult();

                if (truck != null)
                {
                    var truckLevel = _mapeoTableTruckPortal.GetOneLevelTruckPortalByLevelId(request.LevelId).GetAwaiter().GetResult();

                    value = _dBUHResourceRepository.CheckVacantCategory(truck.BusinessCode, truckLevel.TypeEmployeeTruck).GetAwaiter().GetResult();
                }

            }
            return value;
        }
    }

}
