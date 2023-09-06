using ABI.API.Structure.Application.Commands.Entities;
using ABI.API.Structure.Application.DTO;
using ABI.Framework.MS.Helpers.Message;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    [DataContract]
    public class ValidateStructureCommand : IRequest<ValidateStructure>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual int StructureId { get; private set; }
        public virtual DateTimeOffset ValidityFrom { get; private set; }
        public List<StructureNodeDTO> Nodes { get; private set; }

        public ValidateStructureCommand(int structureId, DateTimeOffset validityFrom)
        {
            StructureId = structureId;
            ValidityFrom = validityFrom;
            Nodes = new List<StructureNodeDTO>();
        }



        public void SetNodes(List<StructureNodeDTO> nodes)
        {
            Nodes = nodes;
        }
    }

}
