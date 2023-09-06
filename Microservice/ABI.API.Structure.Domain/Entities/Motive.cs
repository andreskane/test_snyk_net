using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class Motive : BaseEntity<int>
    {

        public virtual ICollection<MotiveState> MotiveStates { get; set; }

        public Motive()
        {
            MotiveStates = new HashSet<MotiveState>();
        }
    }
}
