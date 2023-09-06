using ABI.API.Structure.Application.DTO;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.Structures
{
    [DataContract]
    public class CreateStructureCommandV2 : CreateStructureBaseCommand, IRequest<StructureDTO>
    {
        public CreateStructureCommandV2(string name, int structureModelId, int? rootNodeId, DateTimeOffset? validity, string code)
        {
            AddName(name);
            AddStructureModelId(structureModelId);
            AddValidity(validity);
            AddRootNodeId(rootNodeId);
            AddCode(code);
        }
    }
}
