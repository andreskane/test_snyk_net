using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO
{
    public class EmployeeIdNodesDTO
    {
        public int? EmployeeId { get; set; }
        public List<EmployeeIdNodesItemDTO> Nodes { get; set; }


        public EmployeeIdNodesDTO()
        {
            Nodes = new List<EmployeeIdNodesItemDTO>();
        }
    }
}
