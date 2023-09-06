using ABI.API.Structure.ACL.Truck.Domain.Enums;
using System;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public  class ImportProcess: AuditableEntity
    {
        public ImportProcess() { }
        public ImportProcess(DateTime processDate, string condition, Periodicity periodicity, ImportProcessSource source,String companyId)
        {
            ProcessDate = processDate;
            Condition = condition.ToUpper();
            Periodicity = periodicity;
            Source = source;
            CompanyId = companyId;
        }

        public ImportProcess(
            DateTime processDate,
            DateTime from,
            DateTime to, string condition,
            Periodicity periodicity,
            ImportProcessSource source , string companyId)
        {
            ProcessDate = processDate;
            Condition = condition.ToUpper();
            Periodicity = periodicity;
            Source = source;
            From = from.Year * 10000 + from.Month * 100 + from.Day;
            To = to.Year * 10000 + to.Month * 100 + to.Day;
            CompanyId = companyId;

        }
        public int Id { get; set; }
        public DateTime ProcessDate { get; set; }
        public string Condition { get; set; }
        public Periodicity Periodicity { get; set; }
        public ImportProcessState ProcessState { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ProcessedRows { get; set; }
       
        public ImportProcessSource Source { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }
        public bool IsDeleted { get; set; }





        
        public ImportProcess SetProcessedRows(int count)
        {
            ProcessedRows = count;
            return this;
        }
        
        

    }
}
 
