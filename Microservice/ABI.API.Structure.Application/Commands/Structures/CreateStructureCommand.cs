using MediatR;
using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.Structures
{
    [DataContract]
    public class CreateStructureCommand : CreateStructureBaseCommand, IRequest<int>
    {
        public CreateStructureCommand(string name, int structureModelId, int? rootNodeId, DateTimeOffset? validity, string code)
        {
            AddName(name);
            AddStructureModelId(structureModelId);
            AddValidity(validity);
            AddRootNodeId(rootNodeId);
            AddCode(code);
        }
    }

}
