using ABI.API.Structure.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABI.API.Structure.Domain.Entities
{
  public  class ImportProcess: AuditableEntity
    {
        public ImportProcess(DateTime processDate, string condition, Periodicity periodicity, ImportProcessSource source)
        {
            ProcessDate = processDate;
            Condition = condition.ToUpper();
            Periodicity = periodicity;
            Source = source;
          //  SaleSIs = new List<SaleSI>();
        }

        public int Id { get; set; }
        public DateTime ProcessDate { get; set; }
        public string Condition { get; set; }
        public Periodicity Periodicity { get; set; }
        public ImportProcessState ProcessState { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ProcessedRows { get; set; }
        public decimal? AverageDailySale { get; set; }
        public bool IsDeleted { get; set; }
        public ImportProcessSource Source { get; set; }

        
        

        public ImportProcess SetProcessedRows(int count)
        {
            ProcessedRows = count;
            return this;
        }

        public int From =>
            Periodicity != Periodicity.OneTime ? int.Parse(Condition.Substring(57, 8)) : 0;

        public int To =>
            Periodicity != Periodicity.OneTime ? int.Parse(Condition.Substring(93, 8)) : 0;
    }
}
 
