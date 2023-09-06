using ABI.Framework.MS.Helpers.Message;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.Structures
{
    [DataContract]
    public abstract class CreateStructureBaseCommand
    {
        [Required(ErrorMessage = ErrorMessageText.Required)]
        [StringLength(50, ErrorMessage = ErrorMessageText.StringLengthMax)]
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public virtual int StructureModelId { get; private set; }
        [DataMember]
        public virtual int? RootNodeId { get; private set; }
        [DataMember]
        public virtual DateTimeOffset? ValidityFrom { get; private set; }
        [DataMember]
        public virtual string Code { get; private set; }

        public CreateStructureBaseCommand() { }
        
        public void AddName(string name)
        {
            Name = name;
        }
        public void AddStructureModelId(int structureModelId)
        {
            StructureModelId = structureModelId;
        }
        public void AddValidity(DateTimeOffset? validity)
        {
            ValidityFrom = validity;
        }
        public void AddRootNodeId(int? rootNodeId)
        {
            RootNodeId = rootNodeId;
        }
        public void AddCode(string code)
        {
            Code = code;
        }
    }
}
