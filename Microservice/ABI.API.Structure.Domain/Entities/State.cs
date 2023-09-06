using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class State : BaseEntity<int>
    {
        public int StateGroupId { get; set; }

        public virtual StateGroup StateGroup { get; set; }

        public virtual ICollection<MotiveState> MotiveStates { get; set; }

        public virtual ICollection<ChangeTrackingStatus> ChangeStatus { get; set; }

        public State()
        {
            MotiveStates = new HashSet<MotiveState>();
            ChangeStatus = new HashSet<ChangeTrackingStatus>();
        }
    }
}

