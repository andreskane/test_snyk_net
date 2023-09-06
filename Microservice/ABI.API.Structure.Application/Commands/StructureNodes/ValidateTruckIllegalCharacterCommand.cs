using MediatR;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    [DataContract]
    public class ValidateTruckIllegalCharacterCommand : IRequest
    {
        public int StructureId { get; set; }
        public string Name { get; set; }
    }

}
