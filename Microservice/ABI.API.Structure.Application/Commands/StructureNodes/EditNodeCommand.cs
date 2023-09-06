using System;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    [DataContract]
    public class EditNodeCommand : NodeCommand
    {
        public EditNodeCommand(int id, int? nodeIdParent, int structureId, string name, string code, bool active,
            int? attentionModeId, int? roleId, int? saleChannelId, int? employeeId, int levelId,
            DateTimeOffset validityFrom, DateTimeOffset validityTo)
        {
            Id = id;
            NodeIdParent = nodeIdParent;
            StructureId = structureId;
            Name = name.Trim();
            Code = code;
            AttentionModeId = attentionModeId;
            RoleId = roleId;
            SaleChannelId = saleChannelId;
            EmployeeId = employeeId;
            LevelId = levelId;
            ValidityFrom = validityFrom;
            ValidityTo = validityTo;
            Active = active;
        }


        /// <summary>
        /// Edits the name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void EditName(string name)
        {
            Name = name;
        }
        /// <summary>
        /// Edits the code.
        /// </summary>
        /// <param name="code">The code.</param>
        public void EditCode(string code)
        {
            Code = code;
        }
        /// <summary>
        /// Edits the role identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        public void EditRoleId(int roleId)
        {
            RoleId = roleId;
        }
        /// <summary>
        /// Edits the attention mode identifier.
        /// </summary>
        /// <param name="attentionModeId">The attention mode identifier.</param>
        public void EditAttentionModeId(int attentionModeId)
        {
            AttentionModeId = attentionModeId;
        }
        /// <summary>
        /// Edits the sale channel identifier.
        /// </summary>
        /// <param name="saleChannelId">The sale channel identifier.</param>
        public void EditSaleChannelId(int saleChannelId)
        {
            SaleChannelId = saleChannelId;
        }
        /// <summary>
        /// Edits the employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        public void EditEmployeeId(int employeeId)
        {
            EmployeeId = employeeId;
        }

        /// <summary>
        /// Edits the active.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        public void EditActive(bool active)
        {
            Active = active;
        }

        public void EditLevel(int levelId)
        {
            LevelId = levelId;
        }
    }
}
