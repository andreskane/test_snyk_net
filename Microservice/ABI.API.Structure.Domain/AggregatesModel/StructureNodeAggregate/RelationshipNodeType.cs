using ABI.Framework.MS.Domain.Common;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate
{
    public class RelationshipNodeType : Enumeration
    {
        public static RelationshipNodeType Contains = new RelationshipNodeType(1, "Contiene a");

        public RelationshipNodeType(int id, string name)
          : base(id, name)
        {
        }
    }
}
