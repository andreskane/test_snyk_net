using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;

namespace ABI.API.Structure.Application.Validations
{

    public class EditNodeCheckVacantEmployeeCommandValidator : AbstractValidator<EditNodeCommand>
    {
        private readonly IDBUHResourceRepository _dBUHResourceRepository;
        private readonly IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private readonly IMediator _mediator;
        private readonly IStructureNodeRepository _repositoryStructureNode;

        public EditNodeCheckVacantEmployeeCommandValidator(ILogger<EditNodeCheckVacantEmployeeCommandValidator> logger
                                                            , IDBUHResourceRepository dBUHResourceRepository
                                                            , IMapeoTableTruckPortal mapeoTableTruckPortal
                                                            , IMediator mediator
                                                            , IStructureNodeRepository repositoryStructureNode)
        {

            _dBUHResourceRepository = dBUHResourceRepository ?? throw new ArgumentNullException(nameof(dBUHResourceRepository));
            _mapeoTableTruckPortal = mapeoTableTruckPortal ?? throw new ArgumentNullException(nameof(mapeoTableTruckPortal));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _repositoryStructureNode = repositoryStructureNode ?? throw new ArgumentNullException(nameof(repositoryStructureNode));

            RuleFor(command => command).Must(ValidCheckVacant)
            .OnAnyFailure(x =>
            {
                throw new CheckVacantEmployeeException();
            });

        }

        private bool ValidCheckVacant(EditNodeCommand request)
        {
            var value = true;

            if (request.RoleId.HasValue && !request.EmployeeId.HasValue)
            {
                var structure = _mediator.Send(new GetOneStructureIdByNodeIdQuery { NodeId = request.Id }).GetAwaiter().GetResult();

                var truck = _mapeoTableTruckPortal.GetOneBusinessTruckPortalByName(structure.Name).GetAwaiter().GetResult();

                if (structure.StructureModel.CanBeExportedToTruck && truck != null)
                {
                    var node = _repositoryStructureNode.GetAsync(request.Id).GetAwaiter().GetResult();

                    var truckLevel = _mapeoTableTruckPortal.GetOneLevelTruckPortalByLevelId(node.LevelId).GetAwaiter().GetResult();

                    value = _dBUHResourceRepository.CheckVacantCategory(truck.BusinessCode, truckLevel.TypeEmployeeTruck).GetAwaiter().GetResult();
                }

            }
            return value;
        }
    }

}
