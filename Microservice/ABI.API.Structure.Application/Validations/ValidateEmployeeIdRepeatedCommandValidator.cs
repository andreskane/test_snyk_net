using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Queries.StructureNodes;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations
{
    public class ValidateEmployeeIdRepeatedCommandValidator : AbstractValidator<ValidateStructureCommand>
    {
        private readonly IMediator _mediator;
        public List<DTO.EmployeeIdNodesDTO> EmployeeError { get; set; }
        public string MessageError { get; set; }

        public ValidateEmployeeIdRepeatedCommandValidator(IMediator mediator)
        {
            EmployeeError = new List<DTO.EmployeeIdNodesDTO>();
            _mediator = mediator;
        }

        public async Task<bool> Validate(int structureId, DateTimeOffset validityFrom)
        {
            MessageError = "Empleado en más de un nodo";

            var employeeNodes = await _mediator.Send(new GetRepeatedEmployeeIdsNodesByStructureQuery
            {
                StructureId = structureId,
                ValidityFrom = validityFrom
            });

            if (employeeNodes != null && employeeNodes.Count > 0)
            {
                EmployeeError = employeeNodes;
                return false;
            }

            return true;
        }
    }
}
