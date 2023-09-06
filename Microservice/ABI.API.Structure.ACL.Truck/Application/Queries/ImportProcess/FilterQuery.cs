
using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Domain.Enums;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Queries.ImportProcess
{
    public class FilterQuery : IRequest<List<ImportProcessDTO>>
    {
        public ImportProcessState? ProcessState { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

    }

    public class FilterQueryHandler : IRequestHandler<FilterQuery, List<ImportProcessDTO>>
    {
      

        public FilterQueryHandler(IMapper mapper)
        {
           
        }

        public Task<List<ImportProcessDTO>> Handle(FilterQuery request, CancellationToken cancellationToken)
        {

            var dtos = new List<ImportProcessDTO>();//todo; hay que dar vuelta las interfaces y armar bienla relacion
            return Task.FromResult(dtos);
        }
    }
}