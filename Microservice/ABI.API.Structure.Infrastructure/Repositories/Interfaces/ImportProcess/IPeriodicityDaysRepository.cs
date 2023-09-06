using ABI.API.Structure.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IPeriodicityDaysRepository
    {
        Task<int> GetDaysCount(Periodicity periodicity);
    }
}
