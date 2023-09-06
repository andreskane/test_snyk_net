using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class StateGroup : BaseEntity<int>
    {
        public virtual ICollection<State> States { get; set; }

        public StateGroup()
        {
            States = new HashSet<State>();
        }
    }
}
