using ABI.Framework.MS.Helpers.Message;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class NodeCommand : IRequest<int>
    {
        [DataMember]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public int StructureId { get; set; }

        [DataMember]
        public int? NodeIdParent { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [StringLength(50, ErrorMessage = ErrorMessageText.StringLengthMax)]
        [DataMember]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public string Code { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public int LevelId { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public int? AttentionModeId { get; set; }

        [DataMember]
        public int? RoleId { get; set; }

        [DataMember]
        public int? SaleChannelId { get; set; }

        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DateTimeOffset ValidityFrom { get; set; }

        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DateTimeOffset ValidityTo { get; set; }
    }
}
