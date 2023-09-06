using ABI.API.Structure.ACL.Truck.Domain.Enums;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO
{
    public  class ImportProcessDTO
    {
        public int Id { get; set; }
        public DateTime ProcessDate { get; set; }
        public string Condition { get; set; }
        public Periodicity Periodicity { get; set; }
        public ImportProcessState ProcessState { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ProcessedRows { get; set; }
    
      
    }
}
