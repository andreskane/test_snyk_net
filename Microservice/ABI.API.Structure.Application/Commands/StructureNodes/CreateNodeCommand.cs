using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    [DataContract]
    public class CreateNodeCommand : NodeCommand
    {
        public RelationshipNodeType TypeRelation { get; private set; }

        [DataMember]
        public bool? IsRootNode { get; private set; }

        public CreateNodeCommand(int structureId, int? nodeIdParent, string name, string code, int levelId, bool active,
            int? attentionModeId, int? roleId, int? saleChannelId, int? employeeId, DateTimeOffset validityFrom, bool isRootNode = false, DateTimeOffset? validityTo = null)
        {
            StructureId = structureId;
            TypeRelation = RelationshipNodeType.Contains;
            NodeIdParent = nodeIdParent;
            Name = name.Trim();
            Code = code;
            LevelId = levelId;
            AttentionModeId = attentionModeId;
            RoleId = roleId;
            SaleChannelId = saleChannelId;
            EmployeeId = employeeId;
            IsRootNode = isRootNode;
            ValidityFrom = validityFrom;
            ValidityTo = validityTo ?? DateTimeOffset.MaxValue.ToOffset(-3); //HACER: Ojo multipaís
            Active = active;
        }
    }
}
