using ABI.API.Structure.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABI.API.Structure.Domain.Entities
{
    public class PeriodicityDays
    {
        public Periodicity Id { get; set; }
        public int DaysCount { get; set; }
    }
}
