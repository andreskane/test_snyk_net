﻿using MediatR;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    [DataContract]
    public class ValidateNodeCodeCommand : IRequest
    {
        public int StructureId { get; set; }
        public int LevelId { get; set; }
        public string Code { get; set; }
        public int? NodeId { get; set; }
    }

}
