using ABI.API.Structure.ACL.Truck.Domain.Enums;

namespace ABI.API.Structure.ACL.Truck.Domain.Entities
{
    public class PeriodicityDays
    {
        public Periodicity Id { get; set; }
        public int DaysCount { get; set; }
    }
}
