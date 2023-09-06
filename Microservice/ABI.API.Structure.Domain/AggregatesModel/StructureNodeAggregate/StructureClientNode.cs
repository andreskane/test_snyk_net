using ABI.Framework.MS.Domain.Common;
using System;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureAggregate
{
    public class StructureClientNode : EntityDomain
    {
        public int NodeId { get; private set; }
        public string Name { get; private set; }
        public string ClientId { get; private set; }
        public string ClientState { get; private set; }
        public DateTimeOffset ValidityFrom { get; private set; }
        public DateTimeOffset ValidityTo { get; private set; }
        public StructureNode Node { get; set; }

        public StructureClientNode()
        {
        
        }

        public StructureClientNode(int nodeId, string name, string clienteId, string clienteState, DateTimeOffset validityFrom)
        {
            NodeId = nodeId;
            Name = name.Length > 50 ? name.Substring(0, 50): name;
            ClientId = clienteId;
            ClientState = clienteState;
            ValidityFrom = validityFrom;
            ValidityTo = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)); //HACER: Ojo multipaís
        }

        public void EditValidityTo(DateTimeOffset validityTo)
        {
            ValidityTo = validityTo;
        }

        public void EditValidityFrom(DateTimeOffset validityFrom)
        {
            ValidityFrom = validityFrom;
        }

        public void EditCientState(string state)
        {
            ClientState = state;
        }

        public void EditName(string name)
        {
            Name = name.Length > 50 ? name.Substring(0, 50) : name; 
        }

        public void EditNodeId(int nodeId)
        {
            NodeId = nodeId;
        }
    }
}
