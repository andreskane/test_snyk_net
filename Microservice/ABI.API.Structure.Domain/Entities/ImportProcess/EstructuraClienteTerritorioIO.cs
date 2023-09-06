using System;
using System.Collections.Generic;
using System.Text;

namespace ABI.API.Structure.Domain.Entities
{
 public   class EstructuraClienteTerritorioIO
    {
        public int Id { get; set; }
        public int ImportProcessId { get; set; }
        public string GerenciaID { get; set; }//": "100"
        public string CliId { get; set; }//: "047905",
       public string        CliNom { get; set; }//: "INC S.A.",
        public string         CliSts { get; set; }//: "1",
        public string         CliTrrId { get; set; }//: "29001",
        public string         EmpId { get; set; }//: "001"
        
    }
}
