using MediatR;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    [DataContract]
    public class CreateNodeLinkCommand : IRequest<int>
    {
        [DataMember]
        public string StructureId { get; private set; }
        [DataMember]
        public int? TypeRelationId { get; private set; }
        [DataMember]
        public int? NodeRelation { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Code { get; private set; }
        [DataMember]
        public int LevelId { get; private set; }
        [DataMember]
        public int? AttentionModeId { get; private set; }
        [DataMember]
        public int? RoleId { get; private set; }
        [DataMember]
        public int? SaleChannelId { get; private set; }

        public CreateNodeLinkCommand(string structureId, int? nodeRelation, string name, string code, int levelId)
        {
            StructureId = structureId;
            TypeRelationId = 2; //Link entre nodos
            NodeRelation = nodeRelation;
            Name = name;
            Code = code;
            LevelId = levelId;
        }

        public CreateNodeLinkCommand(string structureId, int? nodeRelation, string name, string code, int levelId, int? attentionModeId, int? roleId, int? saleChannelId)
        {
            StructureId = structureId;
            TypeRelationId = 2; //Link entre nodos
            NodeRelation = nodeRelation;
            Name = name;
            Code = code;
            LevelId = levelId;
            AttentionModeId = attentionModeId;
            RoleId = roleId;
            SaleChannelId = saleChannelId;
        }
    }
}
