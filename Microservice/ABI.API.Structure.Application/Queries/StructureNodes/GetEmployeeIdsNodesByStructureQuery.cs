using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetEmployeeIdsNodesByStructureQuery : IRequest<List<EmployeeIdNodesDTO>>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetEmployeeIdsNodesByStructureQueryHandler : IRequestHandler<GetEmployeeIdsNodesByStructureQuery, List<EmployeeIdNodesDTO>>
    {
        private readonly IMediator _mediator;
        private readonly ILevelRepository _levelRepository;

        public GetEmployeeIdsNodesByStructureQueryHandler(IMediator mediator, ILevelRepository levelRepository)
        {
            _mediator = mediator;
            _levelRepository = levelRepository;
        }

        public async Task<List<EmployeeIdNodesDTO>> Handle(GetEmployeeIdsNodesByStructureQuery request, CancellationToken cancellationToken)
        {

            var list = await _mediator.Send(new GetAllNodeQuery { StructureId = request.StructureId });

            list = list.Where(l =>
                l.NodeValidityFrom <= request.ValidityFrom &&
                l.NodeValidityTo >= request.ValidityFrom &&
                l.NodeMotiveStateId == (int)MotiveStateNode.Confirmed
            )
            .ToList();

            var listPending = await _mediator.Send(new GetStructureNodesPendingQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });

            var listNode = list.Union(listPending).ToList();

            var levels = await _levelRepository.GetAll();

            var employees = (from l in listNode.AsQueryable()
                             where l.NodeEmployeeId != null
                             group l by l.NodeEmployeeId into g

                             select new EmployeeIdNodesDTO
                             {
                                 EmployeeId = g.Key,
                                 Nodes = g.Select(a => new EmployeeIdNodesItemDTO
                                 {
                                     Id = a.NodeId,
                                     Code = a.NodeCode,
                                     Name = a.NodeName,
                                     Level = levels.Where(x => x.Id == a.NodeLevelId).Select(x => x.Name).FirstOrDefault()
                                 }).ToList()
                             }).ToList();

            if (employees.Count == 0)
                return new List<EmployeeIdNodesDTO>();

            return employees;
        }
    }
}