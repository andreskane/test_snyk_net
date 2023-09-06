using ABI.API.Structure.Domain.Entities;
using ABI.Framework.MS.Domain.Common;
using Newtonsoft.Json;
using System;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureAggregate
{
    public class StructureNodeDefinition : EntityDomain
    {
        public int NodeId { get; private set; }
        public DateTimeOffset ValidityFrom { get; private set; }
        public DateTimeOffset ValidityTo { get; private set; }
        public string Name { get; private set; }
        public bool Active { get; private set; }
        public int? AttentionModeId { get; private set; }
        public int? RoleId { get; private set; }
        public int? SaleChannelId { get; private set; }
        public int? EmployeeId { get; private set; }
        public StructureNode Node { get; set; }
        public AttentionMode AttentionMode { get; set; }
        public Role Role { get; private set; }
        public SaleChannel SaleChannel { get; set; }
        public int MotiveStateId { get; private set; }
        public MotiveState MotiveState { get; private set; }
        public bool? VacantPerson { get; private set; }

        public StructureNodeDefinition() { }

        [JsonConstructor]
        public StructureNodeDefinition(int nodeId, int? attentionModeId, int? roleId, int? saleChannelId, int? employeeId, DateTimeOffset validityFrom, string name, bool active)
        {
            NodeId = nodeId;
            ValidityFrom = validityFrom;
            ValidityTo = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)); //HACER: Ojo multipaís
            AttentionModeId = attentionModeId == 0 ? null : attentionModeId;
            RoleId = roleId == 0 ? null : roleId;
            SaleChannelId = saleChannelId == 0 ? null : saleChannelId;
            EmployeeId = employeeId == 0 ? null : employeeId;
            Name = name;
            Active = active;
        }

        public StructureNodeDefinition(int? attentionModeId, int? roleId, int? saleChannelId, int? employeeId, DateTimeOffset validityFrom, string name, bool active)
        {
            ValidityFrom = validityFrom;
            ValidityTo = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)); //HACER: Ojo multipaís
            AttentionModeId = attentionModeId == 0 ? null : attentionModeId;
            RoleId = roleId == 0 ? null : roleId;
            SaleChannelId = saleChannelId == 0 ? null : saleChannelId;
            EmployeeId = employeeId == 0 ? null : employeeId;
            Name = name;
            Active = active;
        }

        public StructureNodeDefinition(StructureNode node, int? attentionModeId, int? roleId, int? saleChannelId, int? employeeId, DateTimeOffset validityFrom, string name, bool active)
        {
            Node = node;
            ValidityFrom = validityFrom;
            ValidityTo = DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)); //HACER: Ojo multipaís
            AttentionModeId = attentionModeId == 0 ? null : attentionModeId;
            RoleId = roleId == 0 ? null : roleId;
            SaleChannelId = saleChannelId == 0 ? null : saleChannelId;
            EmployeeId = employeeId == 0 ? null : employeeId;
            Name = name;
            Active = active;
        }

        public StructureNodeDefinition(int nodeId)
        {
            NodeId = nodeId;
        }

        public StructureNodeDefinition(AttentionMode attentionMode, Role role, SaleChannel saleChannel)
        {
            AttentionMode = attentionMode;
            Role = role;
            SaleChannel = saleChannel;
        }

        public StructureNodeDefinition(AttentionMode attentionMode, Role role, SaleChannel saleChannel, MotiveState state)
        {
            AttentionMode = attentionMode;
            Role = role;
            SaleChannel = saleChannel;
            MotiveState = state;
        }

        public void EditRoleId(int? roleId)
        {
            RoleId = roleId;
        }
        public void EditAttentionModeId(int? attentionModeId)
        {
            AttentionModeId = attentionModeId;
        }
        public void EditSaleChannelId(int? saleChannelId)
        {
            SaleChannelId = saleChannelId;
        }
        public void EditEmployeeId(int? employeeId)
        {
            EmployeeId = employeeId;
        }
        public void EditValidityFrom(DateTimeOffset validityFrom)
        {
            ValidityFrom = validityFrom;
        }
        public void EditValidityTo(DateTimeOffset validityTo)
        {
            ValidityTo = validityTo;
        }
        public void EditName(string name)
        {
            Name = name;
        }
        public void EditActive(bool active)
        {
            Active = active;
        }

        public void EditMotiveStateId(int motiveStateId)
        {
            MotiveStateId = motiveStateId;
        }

        public void EditVacantPerson(bool? vacant)
        {
            VacantPerson = vacant;
        }
    }
}
