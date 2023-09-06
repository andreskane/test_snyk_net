using ABI.Framework.MS.Helpers.Message;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ABI.API.Structure.Application.DTO.FiltresDto
{
    public   class RequestTrayFilterDto  
    {
        public RequestTrayFilterDto()
        {

            sId = new int[] { };

        }


        public int[] sId { get; set; }
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DateTime PeriodFrom { get; set; }

        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DateTime PeriodTo { get; set; }
        public PaginatedSearchDTO Pagination { get; set; }
    }
}
