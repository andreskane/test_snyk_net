using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structure
{
    public class GetAllAristaQuery : IRequest<IList<StructureArista>>
    {
        public int StructureId { get; set; }

        public int NodeId { get; set; }
        
        public int? LevelId { get; set; }

        public bool? OnlyConfirmedAndCurrent { get; set; }

        public DateTimeOffset ValidityFrom { get; set; }

        public List<StructureArista> Aristas  {get; set; }

    }

    public class GetAllAristaQueryHandler : IRequestHandler<GetAllAristaQuery, IList<StructureArista>>
    {
        private readonly IStructureNodeRepository _structureNodeRepository;
        private readonly IMediator _mediator;


        public GetAllAristaQueryHandler(IStructureNodeRepository structureNodeRepository, IMediator mediator)
        {
            _structureNodeRepository = structureNodeRepository;
            _mediator = mediator;
        }

        public async Task<IList<StructureArista>> Handle(GetAllAristaQuery request, CancellationToken cancellationToken)
        {
            if(request.Aristas == null)
                request.Aristas = new List<StructureArista>();

            var query = _structureNodeRepository.GetAristaByNodeFromIQueryable(request.StructureId, request.NodeId, request.ValidityFrom);

            List<StructureArista> aristas = new List<StructureArista>();
            if (request.OnlyConfirmedAndCurrent.HasValue && request.OnlyConfirmedAndCurrent.Value)
            {
                aristas = await query
                            .Include(n => n.NodeTo.StructureNodoDefinitions)
                            .Where(a => 
                                a.NodeTo.StructureNodoDefinitions.Any(
                                    x => x.Active
                                    && x.MotiveStateId == (int)MotiveStateNode.Confirmed
                                    && x.ValidityFrom <= request.ValidityFrom
                                    && x.ValidityTo >= request.ValidityFrom)
                                )
                            .ToListAsync(cancellationToken);
            }
            else
                aristas = await query.ToListAsync(cancellationToken);

            request.Aristas.AddRange(aristas);

            foreach (var item in aristas)
            {
                var queryArista = new GetAllAristaQuery { StructureId = request.StructureId, NodeId = item.NodeIdTo, ValidityFrom = request.ValidityFrom, Aristas = request.Aristas };
                if (request.LevelId.HasValue && item.NodeTo.LevelId <= request.LevelId)
                    queryArista.LevelId = request.LevelId;
                if (request.OnlyConfirmedAndCurrent.HasValue)
                    queryArista.OnlyConfirmedAndCurrent = request.OnlyConfirmedAndCurrent;
                await _mediator.Send(queryArista, cancellationToken);
            }

            return request.Aristas;
        }
    }
}
