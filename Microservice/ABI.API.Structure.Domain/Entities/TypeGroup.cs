using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class TypeGroup : BaseEntity<int>
    {

        public virtual ICollection<Type> Types { get; set; }


        public TypeGroup()
        {
            Types = new HashSet<Type>();
        }
    }
}
