using ABI.Framework.MS.Entity;
using System.Collections.Generic;

namespace ABI.API.Structure.Domain.Entities
{
    public class Country : BaseEntity<int>
    {
        public string Code { get; set; }

        public virtual ICollection<StructureModel> StructureModels { get; set; }

        public Country()
        {
            StructureModels = new HashSet<StructureModel>();
        }
}
}
